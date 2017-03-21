using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1501_请求添加好友
    /// </summary>
    public class Action1501 : BaseAction
    {
        private JPRequestAddFriendData receipt;
        private int destuid;

        public Action1501(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1501, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1501;
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
            UserBasisCache dest = UserHelper.FindUserBasis(destuid);
            UserFriendsCache destFriends = UserHelper.FindUserFriends(destuid);

            receipt = new JPRequestAddFriendData();
            receipt.DestUid = destuid;
            receipt.Nickname = dest.NickName;

            if (GetFriends.IsFriendNumFull())
            {
                receipt.Result = RequestFriendResult.FriendNumFull;
            }
            else if (GetFriends.IsHaveFriend(destuid))
            {
                receipt.Result = RequestFriendResult.HadFriend;
            }
            //else if (dest.IsFriendNumFull())
            //{
            //    receipt.Result = RequestFriendResult.DestFriendNumFull;
            //}
            else if (destFriends.IsHaveFriendApply(GetBasis.UserID))
            {
                receipt.Result = RequestFriendResult.HadApply;
            }
            else
            {
                receipt.Result = RequestFriendResult.OK;
            }
            

            if (receipt.Result == RequestFriendResult.OK)
            {
                destFriends.AddFriendApply(GetBasis.UserID);
                var session = GameSession.Get(destuid);
                if (session != null && session.Connected)
                {
                    var parameters = new Parameters();
                    parameters["Uid"] = GetBasis.UserID;
                    var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1054, session, parameters, OpCode.Text, null);
                    ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1054, packet, (sessions, asyncResult) => {}, 0);
                }

            }

            return true;
        }

    }
}