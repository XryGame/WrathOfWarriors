﻿using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 请求抽奖数据
    /// </summary>
    public class Action1811 : BaseAction
    {
        private JPRequestLotteryData receipt;
        
        public Action1811(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1811, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            return true;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestLotteryData();

            receipt.IsTodayLottery = GetBasis.IsTodayLottery;


            var lottery = UserHelper.RandomLottery(Current.UserId, GetBasis.UserLv);
            if (lottery != null)
            {
                GetBasis.RandomLotteryId = lottery.ID;
                receipt.LotteryAwardType = lottery.Type;
                receipt.LotteryId = lottery.Content;
            }

            var lastlottery = new ShareCacheStruct<Config_Lottery>().FindKey(GetBasis.LastLotteryId);
            if (lastlottery != null)
            {
                receipt.LastLotteryAwardType = lastlottery.Type;
                receipt.LastLotteryId = lastlottery.Content;
            }


            return true;
        }
    }
}