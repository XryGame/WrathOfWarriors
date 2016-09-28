using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1505_领取好友馈赠
    /// </summary>
    public class Action1505 : BaseAction
    {
        private JPReceiveFriendGiveAwayData receipt;
        private int destuid;

        public Action1505(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1505, actionGetter)
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
            FriendData fd = ContextUser.FindFriend(destuid);
            if (!ContextUser.IsHaveFriend(destuid)
                || !ContextUser.IsHaveFriendGiveAway(destuid)
                || fd == null
                || fd.IsReceiveGiveAway)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }
            
            ContextUser.Vit = MathUtils.Addition(ContextUser.Vit, DataHelper.FriendGiveAwayVitValue, int.MaxValue);
            fd.IsReceiveGiveAway = true;

            receipt = new JPReceiveFriendGiveAwayData()
            {
                UserId = destuid,
                AwayVit = DataHelper.FriendGiveAwayVitValue,
                CurrVit = ContextUser.Vit,
                NickName = dest.NickName
            };
            

            return true;
        }

    }
}