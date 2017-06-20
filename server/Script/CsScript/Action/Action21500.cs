using GameServer.CsScript.Base;
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
    /// 领取邀请奖励
    /// </summary>
    public class Action21500 : BaseAction
    {
        private bool receipt;
        private int id;
        public Action21500(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action21500, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref id))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            var share = new ShareCacheStruct<Config_Share>().FindKey(id);
            if (share == null || share.Type != ShareType.Invite || GetBasis.ReceiveInviteList.Find(t => t == id) != 0)
            {
                return false;
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

            GetBasis.ReceiveInviteList.Add(id);
            receipt = true;
            return true;
        }
    }
}