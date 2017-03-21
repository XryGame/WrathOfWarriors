using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 8002_邀请人取消邀请
    /// </summary>
    public class Action8002 : BaseAction
    {
        private int destuid;
       
        public Action8002(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action8002, actionGetter)
        {
            IsNotRespond = true;
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
            GetBasis.UserStatus = UserStatus.MainUi;
            GetBasis.InviteFightDestUid = 0;
            GameSession session = GameSession.Get(destuid);
            UserBasisCache dest = UserHelper.FindUserBasis(destuid);
            if (session == null || !session.Connected || dest == null
                || dest.UserStatus != UserStatus.Inviteing)
            {
                return true;
            }

            dest.UserStatus = UserStatus.MainUi;
            PushMessageHelper.CancelInviteFightNotification(session, GetBasis.NickName);
            return true;
        }
    }
}