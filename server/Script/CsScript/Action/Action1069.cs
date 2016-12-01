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
    /// 1069_好友上线
    /// </summary>
    public class Action1069 : BaseAction
    {
        private int receipt;
        private int _FriendUid;
        public Action1069(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1069, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("FriendUid", ref _FriendUid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = _FriendUid;
            return true;
        }
    }
}