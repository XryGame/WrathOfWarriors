using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1503_删除好友
    /// </summary>
    public class Action1503 : BaseAction
    {
        private object receipt;
        private int destuid;

        public Action1503(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1503, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1503;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUid", ref destuid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            GameUser dest = UserHelper.FindUser(destuid);
            if (dest == null)
            {
                ErrorInfo = Language.Instance.NoFoundUser;
                return true;
            }

            if (ContextUser.IsHaveFriend(destuid))
            {
                ContextUser.RemoveFriend(destuid);
            }
            if (dest.IsHaveFriend(ContextUser.UserID))
            {
                dest.RemoveFriend(ContextUser.UserID);
            }

            if (dest.IsOnline)
            {
                var parameters = new Parameters();
                parameters["Uid"] = ContextUser.UserID;
                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1056, GameSession.Get(destuid), parameters, OpCode.Text, null);
                ActionFactory.SendAction(GameSession.Get(destuid), ActionIDDefine.Cst_Action1056, packet, (session, asyncResult) => { }, 0);
            }
            
            receipt = destuid;

            return true;
        }

    }
}