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
    /// 10101_请求抽奖数据
    /// </summary>
    public class Action10101 : BaseAction
    {
        private JPRequestLotteryData receipt;
        
        public Action10101(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10101, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            return true;
        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action10101;
            }
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestLotteryData();

            receipt.IsTodayLottery = ContextUser.IsTodayLottery;

            if (!ContextUser.IsTodayLottery)
            {
                var lottery = UserHelper.RandomLottery(ContextUser.UserID, ContextUser.UserLv);
                if (lottery != null)
                {
                    ContextUser.RandomLotteryId = lottery.ID;
                    receipt.LotteryAwardType = lottery.Type;
                    receipt.LotteryId = lottery.Content;
                }
            }
            else
            {
                var lottery = new ShareCacheStruct<Config_Lottery>().FindKey(ContextUser.LastLotteryId);
                if (lottery != null)
                {
                    receipt.LotteryAwardType = lottery.Type;
                    receipt.LotteryId = lottery.Content;
                }
            }


            return true;
        }
    }
}