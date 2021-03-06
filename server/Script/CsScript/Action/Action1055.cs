﻿using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1055_添加新的好友通知接口
    /// </summary>
    public class Action1055 : BaseAction
    {
        private JPFriendData receipt;
        private int Uid;
        public Action1055(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1055, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1055;
            }
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

            receipt = new JPFriendData();
            receipt.UserId = Uid;
            receipt.NickName = dest.NickName;
            receipt.Profession = dest.Profession;
            receipt.AvatarUrl = dest.AvatarUrl;
            receipt.UserLv = dest.UserLv;
            //receipt.FightValue = dest.FightingValue;
            receipt.VipLv = dest.VipLv;
            GameSession fsession = GameSession.Get(dest.UserID);
            if (fsession != null && fsession.Connected)
                receipt.IsOnline = true;

            return true;
        }
    }
}