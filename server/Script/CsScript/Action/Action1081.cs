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
    /// 1081_一个请求入会申请移除
    /// </summary>
    public class Action1081 : BaseAction
    {

        private int receipt;
        private int _ApplyUid;
        public Action1081(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1081, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ApplyUid", ref _ApplyUid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = _ApplyUid;
            return true;
        }
    }
}