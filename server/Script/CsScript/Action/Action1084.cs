using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 获得新技能通知
    /// </summary>
    public class Action1084 : BaseAction
    {

        private SkillData receipt;

        private int _skillId;
        public Action1084(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1084, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("SkillID", ref _skillId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = GetSkill.FindSkill(_skillId);
            return true;
        }
    }
}