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
            UserFriendsCache destFriends = UserHelper.FindUserFriends(destuid);

            if (GetFriends.IsHaveFriend(destuid))
            {
                GetFriends.RemoveFriend(destuid);
            }
            if (destFriends.IsHaveFriend(GetBasis.UserID))
            {
                destFriends.RemoveFriend(GetBasis.UserID);
            }

            var session = GameSession.Get(destuid);
            if (session != null && session.Connected)
            {
                var parameters = new Parameters();
                parameters["Uid"] = GetBasis.UserID;
                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1056, session, parameters, OpCode.Text, null);
                ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1056, packet, (sessions, asyncResult) => { }, 0);
            }
            
            receipt = destuid;

            return true;
        }

    }
}