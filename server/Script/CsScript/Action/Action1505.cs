//using GameServer.CsScript.JsonProtocol;
//using GameServer.Script.CsScript.Action;
//using GameServer.Script.Model.Config;
//using GameServer.Script.Model.DataModel;
//using ZyGames.Framework.Common;
//using ZyGames.Framework.Game.Lang;
//using ZyGames.Framework.Game.Service;

//namespace GameServer.CsScript.Action
//{

//    /// <summary>
//    /// 1505_领取好友馈赠
//    /// </summary>
//    public class Action1505 : BaseAction
//    {
//        private bool receipt;
//        private int destuid;

//        public Action1505(ActionGetter actionGetter)
//            : base(ActionIDDefine.Cst_Action1505, actionGetter)
//        {

//        }

//        protected override string BuildJsonPack()
//        {

//            body = receipt;
//            return base.BuildJsonPack();
//        }

//        public override bool GetUrlElement()
//        {
//            if (httpGet.GetInt("DestUid", ref destuid))
//            {
//                return true;
//            }
//            return false;
//        }

//        public override bool TakeAction()
//        {
//            UserBasisCache dest = UserHelper.FindUserBasis(destuid);
//            if (dest == null)
//            {
//                ErrorInfo = Language.Instance.NoFoundUser;
//                return true;
//            }
//            FriendData fd = GetFriends.FindFriend(destuid);
//            if (!GetFriends.IsHaveFriend(destuid)
//                || !GetFriends.IsHaveFriendGiveAway(destuid)
//                || fd == null
//                || fd.IsReceiveGiveAway)
//            {
//                ErrorInfo = Language.Instance.RequestIDError;
//                return true;
//            }
            
//            fd.IsReceiveGiveAway = true;

//            receipt = true;
            

//            return true;
//        }

//    }
//}