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
    ///通天塔新日志通知
    /// </summary>
    public class Action1082 : BaseAction
    {

        private CombatLogData receipt;
        public Action1082(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1082, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = GetCombat.LogList[GetCombat.LogList.Count - 1];
            return true;
        }
    }
}