using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class LotteryData
    {
        public LotteryData()
        {
            AwardItemList = new List<int>();
        }
        public RequestLotteryResult Result { get; set; }

        public int ID { get; set; }

        public LotteryAwardType Type { get; set; }

        public List<int> AwardItemList { get; set; }

        public string AwardNum { get; set; }



    }
    /// <summary>
    /// 请求抽奖
    /// </summary>
    public class Action1810 : BaseAction
    {
        private LotteryData receipt;
        private Random random = new Random();
        
        public Action1810(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1810, actionGetter)
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
            receipt = new LotteryData();

            var itemSet = new ShareCacheStruct<Config_Item>();

            if (GetBasis.RandomLotteryId == 0)
            {
                var randlottery = UserHelper.RandomLottery(GetBasis.UserLv);
                if (randlottery != null)
                {
                    GetBasis.RandomLotteryId = randlottery.ID;
                }
            }

            Config_Lottery lott = new ShareCacheStruct<Config_Lottery>().FindKey(GetBasis.RandomLotteryId);
            if (lott == null)
            {
                return false;
            }

            if (GetBasis.LotteryTimes <= 0)
            {
                //int needDiamond = ConfigEnvSet.GetInt("User.LotteryNeedDiamondNum");
                //if (GetBasis.DiamondNum < needDiamond)
                //{
                //    receipt.Result = RequestLotteryResult.NoDiamond;
                //    return true;
                //}
                //UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
                receipt.Result = RequestLotteryResult.NoTimes;
                return true;
            }

            receipt.ID = GetBasis.RandomLotteryId;
            receipt.Type = lott.Type;
            switch (lott.Type)
            {
                case LotteryAwardType.Gold:
                    {
                        BigInteger resourceNum = BigInteger.Parse(lott.Content);
                        BigInteger value = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * resourceNum;
                        
                        UserHelper.RewardsGold(Current.UserId, value);
                        receipt.AwardNum = value.ToString();
                    }
                    break;
                case LotteryAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, lott.Content.ToInt());
                        receipt.AwardNum = lott.Content;
                    }
                    break;
                case LotteryAwardType.Gem:
                    {
                        //var items = itemSet.FindAll(t => (t.ItemType == ItemType.Gem
                        //                            && t.Quality == ItemQuality.Normal));
                        //int maxCount = lott.Content.ToInt();
                        //for (int i = 0; i < maxCount; ++i)
                        //{
                        //    int index = random.Next(items.Count);
                        //    int awardId = items[index].ItemID;
                        //    UserHelper.RewardsItem(Current.UserId, awardId, 1);
                        //    receipt.AwardItemList.Add(awardId);

                        //}
                        //receipt.AwardNum = receipt.AwardItemList.Count.ToString();
                        int maxCount = lott.Content.ToInt();
                        for (int i = 0; i < maxCount; ++i)
                        {
                            var lotteryGem = UserHelper.RandomLotteryGem();
                            UserHelper.RewardsItem(Current.UserId, lotteryGem.ID, 1);
                            receipt.AwardItemList.Add(lotteryGem.ID);

                        }
                    }
                    break;
                case LotteryAwardType.Debris:
                    {
                        var debris = itemSet.FindAll(t => (t.ItemType == ItemType.Debris));
                        foreach (var v in GetElf.ElfList)
                        {
                            var elfCard = itemSet.Find(t => t.ResourceNum.ToInt() == v.ID);
                            if (elfCard == null)
                                continue;

                            debris.RemoveAll(t => (t.ResourceNum.ToInt() == elfCard.ItemID));
                        }
                        if (debris.Count == 0)
                        {
                            debris = itemSet.FindAll(t => (t.ItemType == ItemType.Debris));
                        }

                        int maxCount = lott.Content.ToInt();
                        for (int i = 0; i < maxCount; ++i)
                        {
                            int index = random.Next(debris.Count);
                            int awardId = debris[index].ItemID;
                            UserHelper.RewardsItem(Current.UserId, awardId, 1);
                            receipt.AwardItemList.Add(awardId);

                        }
                        receipt.AwardNum = receipt.AwardItemList.Count.ToString();
                    }
                    break;
            }

            if (GetBasis.LotteryTimes > 0)
                GetBasis.LotteryTimes --;
            //GetBasis.RandomLotteryId = 0;
            GetBasis.LastLotteryId = GetBasis.RandomLotteryId;

            var lottery = UserHelper.RandomLottery(GetBasis.UserLv);
            if (lottery != null)
            {
                GetBasis.RandomLotteryId = lottery.ID;
            }

            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.Lottery, 1);

            receipt.Result = RequestLotteryResult.OK;

            return true;
        }
    }
}