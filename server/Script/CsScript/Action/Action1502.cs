using GameServer.CsScript.JsonProtocol;
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
    /// 1502_添加好友回执
    /// </summary>
    public class Action1502 : BaseAction
    {
        private JPFriendApplyReceiptData receipt;
        private int destuid;
        private EventStatus result;

        public Action1502(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1502, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1502;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUid", ref destuid)
                && httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            UserBasisCache dest = UserHelper.FindUserBasis(destuid);
            UserFriendsCache destFriends = UserHelper.FindUserFriends(destuid);

            receipt = new JPFriendApplyReceiptData();
            receipt.Data.UserId = destuid;
            receipt.Data.NickName = dest.NickName;
            receipt.Data.Profession = dest.Profession;
            FriendApplyData apply = GetFriends.FindFriendApply(destuid);
            if (apply == null)
            {
                receipt.Result = RequestFriendResult.NoApply;
                return true;
            }
            
            if (result == EventStatus.Good)
            {
                if (GetFriends.IsFriendNumFull())
                {
                    receipt.Result = RequestFriendResult.FriendNumFull;
                }
                else if (GetFriends.IsHaveFriend(destuid))
                {
                    receipt.Result = RequestFriendResult.HadFriend;
                }
                else if (destFriends.IsFriendNumFull())
                {
                    receipt.Result = RequestFriendResult.DestFriendNumFull;
                }
                else
                {
                    receipt.Result = RequestFriendResult.OK;
                }

                if (receipt.Result == RequestFriendResult.OK)
                {
                    GetFriends.AddFriend(destuid);
                    receipt.Data.UserLv = dest.UserLv;
                    //receipt.Data.FightValue = dest.FightingValue;
                    receipt.Data.VipLv = dest.VipLv;
                    GameSession fsession = GameSession.Get(dest.UserID);
                    if (fsession != null && fsession.Connected)
                        receipt.Data.IsOnline = true;

                    destFriends.AddFriend(GetBasis.UserID);

                    PushMessageHelper.NewFriendNotification(GameSession.Get(destuid), Current.UserId);
                }
            }
            GetFriends.ApplyList.Remove(apply);
            

            return true;
        }

    }
}