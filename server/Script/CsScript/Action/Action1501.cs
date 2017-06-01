using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
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
        private RequestFriendResult receipt;
        private int destuid;

        public Action1501(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1501, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
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
           

            if (GetFriends.IsFriendNumFull())
            {
                receipt = RequestFriendResult.FriendNumFull;
            }
            else if (destFriends.IsFriendNumFull())
            {
                receipt = RequestFriendResult.DestFriendNumFull;
            }
            else if (GetFriends.IsHaveFriend(destuid))
            {
                receipt = RequestFriendResult.HadFriend;
            }
            //else if (destFriends.IsHaveFriendApply(Current.UserId))
            //{
            //    receipt = RequestFriendResult.HadApply;
            //}
            else
            {
                receipt = RequestFriendResult.OK;

                //destFriends.AddFriendApply(Current.UserId);
                //var session = GameSession.Get(destuid);
                //PushMessageHelper.NewFriendRequestNotification(GameSession.Get(destuid), Current.UserId);

                GetFriends.AddFriend(destuid);

                destFriends.AddFriend(Current.UserId);

                PushMessageHelper.NewFriendNotification(GameSession.Get(destuid), Current.UserId);
                PushMessageHelper.NewFriendNotification(Current, destuid);

            }

            return true;
        }

    }
}