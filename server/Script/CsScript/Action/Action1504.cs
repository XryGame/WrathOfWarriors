using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
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
    /// 1504_好友馈赠
    /// </summary>
    public class Action1504 : BaseAction
    {
        private int receipt;
        private int destuid;

        public Action1504(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1504, actionGetter)
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
            
           
            if (!GetFriends.IsHaveFriend(destuid))
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }

            if (GetFriends.GiveAwayCount >= DataHelper.FriendGiveAwayCountMax)
            {
                ErrorInfo = Language.Instance.NoValidTimes;
                return true;
            }

            FriendData fd = GetFriends.FindFriend(destuid);
            FriendData byfd = destFriends.FindFriend(Current.UserId);
            if (fd.IsGiveAway || byfd == null)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }

            GetFriends.GiveAwayCount++;
            fd.IsGiveAway = true;
            byfd.IsByGiveAway = true;
            byfd.IsReceiveGiveAway = false;

            PushMessageHelper.FriendGiveAwayNotification(GameSession.Get(destuid), Current.UserId);
            
            receipt = destuid;


            return true;
        }

    }
}