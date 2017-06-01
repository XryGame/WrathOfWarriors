//using GameServer.CsScript.JsonProtocol;
//using GameServer.Script.CsScript.Action;
//using GameServer.Script.CsScript.Com;
//using GameServer.Script.Model.Config;
//using GameServer.Script.Model.DataModel;
//using GameServer.Script.Model.Enum;
//using ZyGames.Framework.Game.Contract;
//using ZyGames.Framework.Game.Lang;
//using ZyGames.Framework.Game.Service;
//using ZyGames.Framework.Net;
//using ZyGames.Framework.RPC.Sockets;

//namespace GameServer.CsScript.Action
//{

//    /// <summary>
//    /// 1502_添加好友回执
//    /// </summary>
//    public class Action1502 : BaseAction
//    {
//        private RequestFriendResult receipt;
//        private int destuid;
//        private EventStatus result;

//        public Action1502(ActionGetter actionGetter)
//            : base(ActionIDDefine.Cst_Action1502, actionGetter)
//        {

//        }

//        protected override string BuildJsonPack()
//        {

//            body = receipt;
//            return base.BuildJsonPack();
//        }

//        public override bool GetUrlElement()
//        {
//            if (httpGet.GetInt("DestUid", ref destuid)
//                && httpGet.GetEnum("Result", ref result))
//            {
//                return true;
//            }
//            return false;
//        }

//        public override bool TakeAction()
//        {

//            UserBasisCache dest = UserHelper.FindUserBasis(destuid);
//            UserFriendsCache destFriends = UserHelper.FindUserFriends(destuid);
            
//            FriendApplyData apply = GetFriends.FindFriendApply(destuid);
//            if (apply == null)
//            {
//                receipt = RequestFriendResult.NoApply;
//                return true;
//            }
            
//            if (result == EventStatus.Good)
//            {
//                if (GetFriends.IsFriendNumFull())
//                {
//                    receipt = RequestFriendResult.FriendNumFull;
//                }
//                else if (GetFriends.IsHaveFriend(destuid))
//                {
//                    receipt = RequestFriendResult.HadFriend;
//                }
//                else if (destFriends.IsFriendNumFull())
//                {
//                    receipt = RequestFriendResult.DestFriendNumFull;
//                }
//                else
//                {
//                    receipt = RequestFriendResult.OK;

//                    GetFriends.AddFriend(destuid);

//                    destFriends.AddFriend(Current.UserId);

//                    PushMessageHelper.NewFriendNotification(GameSession.Get(destuid), Current.UserId);
//                    PushMessageHelper.NewFriendNotification(Current, destuid);
//                }
//            }
//            GetFriends.ApplyList.Remove(apply);
            

//            return true;
//        }

//    }
//}