using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
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
        private int receipt;
        private int destuid;

        public Action1503(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1503, actionGetter)
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
            UserFriendsCache destFriends = UserHelper.FindUserFriends(destuid);

            if (GetFriends.IsHaveFriend(destuid))
            {
                GetFriends.RemoveFriend(destuid);
            }
            if (destFriends.IsHaveFriend(Current.UserId))
            {
                destFriends.RemoveFriend(Current.UserId);
            }

            PushMessageHelper.FriendRemoveNotification(GameSession.Get(destuid), Current.UserId);
            receipt = destuid;

            return true;
        }

    }
}