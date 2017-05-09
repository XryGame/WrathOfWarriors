using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1111_通关关卡
    /// </summary>
    public class Action1111 : BaseAction
    {
        private UserAttributeCache receipt = null;

        private int _ID;
        public Action1111(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1111, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref _ID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var roleInitialSet = new ShareCacheStruct<Config_RoleInitial>();
            if (roleInitialSet.FindKey(_ID) == null)
                return false;
            var transcriptSet = new ShareCacheStruct<Config_TeneralTranscript>();
            var transcriptCfg = transcriptSet.FindKey(_ID);
            if (transcriptCfg == null)
                return false;

            if (_ID == GetBasis.UserLv)
            {
                if (roleInitialSet.FindKey(_ID + 1) != null
                    && transcriptSet.FindKey(_ID + 1) != null)
                {
                    GetBasis.UserLv = _ID + 1;
                    UserHelper.UserLevelUp(Current.UserId);

                    // 技能
                    if (GetBasis.UserLv % 10 == 0 && (GetBasis.UserLv / 10) % 2 == 0)
                    {
                        var skillcfg = new ShareCacheStruct<Config_Skill>().Find(t => (
                            t.SkillGroup == GetBasis.Profession && t.SkillID % 10000 == GetBasis.UserLv / 10)
                        );
                        if (GetSkill.AddSkill(skillcfg.SkillID))
                        {
                            PushMessageHelper.NewSkillNotification(Current, skillcfg.SkillID);
                        }
                    }
                }

                // 每日
                if (transcriptCfg.limitTime > 0)
                {
                    UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.PassStageBoss, 1);
                }
                else
                {
                    UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.PassStage, 1);
                }

                // 成就
                UserHelper.AchievementProcess(Current.UserId, AchievementType.LevelCount);
            }

            receipt = GetAttribute;
            return true;
        }
    }
}