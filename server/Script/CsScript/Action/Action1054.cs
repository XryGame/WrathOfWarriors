﻿using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1054_新的好友请求通知接口
    /// </summary>
    public class Action1054 : BaseAction
    {
        private JPFriendApplyData receipt;
        private int Uid;
        public Action1054(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1054, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Uid", ref Uid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            UserBasisCache dest = UserHelper.FindUserBasis(Uid);
            if (dest == null)
                return true;
            FriendApplyData apply = GetFriends.FindFriendApply(Uid);
            if (apply == null)
                return true;

            receipt = new JPFriendApplyData();
            receipt.UserId = Uid;
            receipt.NickName = dest.NickName;
            receipt.Profession = dest.Profession;
            receipt.AvatarUrl = dest.AvatarUrl;
            receipt.UserLv = dest.UserLv;
            //receipt.FightValue = dest.FightingValue;
            receipt.VipLv = dest.VipLv;
            receipt.ApplyTime = apply.ApplyDate;

            GameSession fsession = GameSession.Get(dest.UserID);
            receipt.IsOnline = fsession != null && fsession.Connected;
            return true;
        }
    }
}