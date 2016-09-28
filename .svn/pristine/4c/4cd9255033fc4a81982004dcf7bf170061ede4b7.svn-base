using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Com.Rank;
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

            GameUser dest = UserHelper.FindUser(destuid);
            if (dest == null)
            {
                ErrorInfo = Language.Instance.NoFoundUser;
                return true;
            }

            receipt = new JPFriendApplyReceiptData();
            receipt.Data.UserId = destuid;
            receipt.Data.NickName = dest.NickName;

            FriendApplyData apply = ContextUser.FindFriendApply(destuid);
            if (apply == null)
            {
                receipt.Result = RequestFriendResult.NoApply;
                return true;
            }
            
            if (result == EventStatus.Good)
            {
                if (ContextUser.IsFriendNumFull())
                {
                    receipt.Result = RequestFriendResult.FriendNumFull;
                }
                else if (ContextUser.IsHaveFriend(destuid))
                {
                    receipt.Result = RequestFriendResult.HadFriend;
                }
                else if (dest.IsFriendNumFull())
                {
                    receipt.Result = RequestFriendResult.DestFriendNumFull;
                }
                else
                {
                    receipt.Result = RequestFriendResult.OK;
                }

                if (receipt.Result == RequestFriendResult.OK)
                {
                    ContextUser.AddFriend(destuid);
                    receipt.Data.UserLv = dest.UserLv;
                    receipt.Data.FightValue = dest.FightingValue;
                    receipt.Data.VipLv = dest.VipLv;
                    receipt.Data.IsOnline = dest.IsOnline;


                    dest.AddFriend(ContextUser.UserID);
                    if (dest.IsOnline)
                    {
                        var parameters = new Parameters();
                        parameters["Uid"] = ContextUser.UserID;
                        var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action1055, GameSession.Get(destuid), parameters, OpCode.Text, null);
                        ActionFactory.SendAction(GameSession.Get(destuid), ActionIDDefine.Cst_Action1055, packet, (session, asyncResult) => { }, 0);
                    }
                }
            }
            ContextUser.FriendsData.ApplyList.Remove(apply);
            

            return true;
        }

    }
}