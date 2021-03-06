﻿using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 分享成功
    /// </summary>
    public class Action21400 : BaseAction
    {
        private bool receipt;
        public Action21400(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action21400, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            GetBasis.ShareCount++;
            GetBasis.ShareDate = Util.GetTimeStamp();
            var share = new ShareCacheStruct<Config_Share>().Find(t => (t.Type == ShareType.Share && t.Number == GetBasis.ShareCount));
            if (share == null)
            {
                return true;
            }

            switch (share.RewardType)
            {
                case TaskAwardType.Gold:
                    {
                        BigInteger resourceNum = BigInteger.Parse(share.RewardNum);
                        BigInteger value = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * resourceNum;
                        UserHelper.RewardsGold(Current.UserId, value);
                    }
                    break;
                case TaskAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, share.RewardNum.ToInt());
                    }
                    break;
                case TaskAwardType.Item:
                    {
                        UserHelper.RewardsItem(Current.UserId, share.RewardNum.ToInt(), 1);
                    }
                    break;
            }

            UserHelper.RewardsItem(Current.UserId, share.AddRewardItem, share.AddRewardNum.ToInt());

            receipt = true;
            return true;
        }
    }
}