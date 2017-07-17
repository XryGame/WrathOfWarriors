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

    public class Action1088 : BaseAction
    {
        /// <summary>
        /// 
        /// </summary>
        private EnemyLogData receipt;

        public Action1088(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1088, actionGetter)
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
            if (GetEnemys.LogList.Count > 0)
            {
                receipt = GetEnemys.LogList[GetEnemys.LogList.Count - 1];
            }
            return true;
        }
    }
}