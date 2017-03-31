using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1140_升级技能
    /// </summary>
    public class Action1140 : BaseAction
    {
        private bool receipt;
        private int skillId;

        public Action1140(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1140, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("SkillID", ref skillId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var skillSet = new ShareCacheStruct<Config_Skill>();
            var skillGradeSet = new ShareCacheStruct<Config_SkillGrade>();
            var skillcfg = skillSet.FindKey(skillId);
            if (skillcfg == null)
            {
                return false;
            }

            var skill = GetSkill.SkillList.Find(t => t.ID == skillId);
            if (skill == null)
            {
                return false;
            }

            var skillGradeCfg = skillGradeSet.Find(t => (t.SkillId == skillId && t.SkillGrade == skill.Lv + 1));
            if (skillGradeCfg == null)
            {
                return false;
            }

            switch (skillGradeCfg.ConsumeType)
            {
                case ConsumeType.Gold:
                    {
                        BigInteger consumeNumber = Util.ConvertGameCoin(skillGradeCfg.ConsumeNumber);
                        if (GetBasis.GoldNum < consumeNumber)
                        {
                            return false;
                        }
                        else
                        {
                            UserHelper.ConsumeGold(Current.UserId, consumeNumber);
                        }
                    }
                    break;
                case ConsumeType.Diamond:
                    {
                        if (GetBasis.DiamondNum < Convert.ToInt32(skillGradeCfg.ConsumeNumber))
                        {
                            return false;
                        }
                        else
                        {
                            UserHelper.ConsumeDiamond(Current.UserId, Convert.ToInt32(skillGradeCfg.ConsumeNumber));
                        }
                    }
                    break;
            }


            skill.Lv = skillGradeCfg.SkillGrade;

            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.UpgradeSkill, 1);

            // 成就
            UserHelper.AchievementProcess(Current.UserId, AchievementType.UpgradeSkill);

            receipt = true;
            return true;
        }
    }
}