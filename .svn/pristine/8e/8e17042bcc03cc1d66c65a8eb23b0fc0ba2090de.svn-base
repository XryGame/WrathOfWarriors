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
        private object receipt;
        private int destuid;

        public Action1504(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1504, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1504;
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

           
            if (!ContextUser.IsHaveFriend(destuid))
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }

            if (ContextUser.FriendsData.GiveAwayCount >= DataHelper.FriendGiveAwayCountMax)
            {
                ErrorInfo = Language.Instance.NoValidTimes;
                return true;
            }

            FriendData fd = ContextUser.FindFriend(destuid);
            FriendData byfd = dest.FindFriend(ContextUser.UserID);
            if (fd.IsGiveAway || byfd == null)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }

            ContextUser.FriendsData.GiveAwayCount++;
            fd.IsGiveAway = true;
            byfd.IsByGiveAway = true;
            byfd.IsReceiveGiveAway = false;

            var session = GameSession.Get(destuid);
            if (session != null && session.Connected)
            {
                var parameters = new Parameters();
                parameters["Uid"] = ContextUser.UserID;
                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1057, session, parameters, OpCode.Text, null);
                ActionFactory.SendAction(session, ActionIDDefine.Cst_Action1057, packet, (sessions, asyncResult) => { }, 0);
            }
            
            receipt = destuid;

            // 每日
            UserHelper.EveryDayTaskProcess(ContextUser.UserID, TaskType.GiveAwayFriend, 1);

            return true;
        }

    }
}