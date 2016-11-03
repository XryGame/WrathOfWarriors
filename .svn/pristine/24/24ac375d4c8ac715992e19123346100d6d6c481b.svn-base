using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 8000_请求切磋
    /// </summary>
    public class Action8000 : BaseAction
    {
        private RequestInviteFightResult receipt;
        private int destuid;
       
        public Action8000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action8000, actionGetter)
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
            receipt = RequestInviteFightResult.OK;
            GameSession session = GameSession.Get(destuid);
            GameUser dest = UserHelper.FindUser(destuid);
            if (session == null || dest == null || !dest.IsOnline)
            {
                receipt = RequestInviteFightResult.Offine;
                return true;
            }
            else if (dest.UserStatus == UserStatus.Inviteing)
            {
                receipt = RequestInviteFightResult.HadInvite;
                return true;
            }
            else if (dest.UserStatus == UserStatus.Fighting)
            {
                receipt = RequestInviteFightResult.Fighting;
                return true;
            }

            ContextUser.UserStatus = UserStatus.Inviteing;
            ContextUser.InviteFightDestUid = destuid;

            // 发送切磋邀请
            dest.UserStatus = UserStatus.Inviteing;
            PushMessageHelper.InviteFightNotification(session, ContextUser.UserID);

            return true;
        }
    }
}