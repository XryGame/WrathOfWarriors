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
    /// 技能卸载
    /// </summary>
    public class Action1142 : BaseAction
    {
        private bool receipt;
        private int skillId;

        public Action1142(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1142, actionGetter)
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

            if (GetSkill.CarryList.Find(t => t == skillId) == 0)
            {
                return false;
            }
            GetSkill.CarryList.Remove(skillId);
            receipt = true;
            return true;
        }
    }
}