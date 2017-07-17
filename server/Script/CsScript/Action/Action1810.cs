using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.CsScript.Remote;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class LottItem
    {
        public int ID { get; set; }

        public LotteryAwardType Type { get; set; }

        public int ItemID { get; set; }

        public string Num { get; set; }

    }
    public class LotteryData
    {
        public LotteryData()
        {
            AwardList = new List<LottItem>();
        }
        public RequestLotteryResult Result { get; set; }


        public List<LottItem> AwardList { get; set; }

    }
    /// <summary>
    /// 请求抽奖
    /// </summary>
    public class Action1810 : BaseAction
    {
        private LotteryData receipt;
        private Random random = new Random();
        private bool isFiveTimes = false;
        public Action1810(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1810, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetBool("IsTenTimes", ref isFiveTimes))
            {
                return true;
            }
            return false;
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

            int times = isFiveTimes ? 5 : 1;
            
            if (GetLottery.LotteryTimes <= times)
            {
                int needDiamond = ConfigEnvSet.GetInt("User.BuyLotteryTimesNeedDiamond") * (times - GetLottery.LotteryTimes);
                if (GetBasis.DiamondNum < needDiamond)
                {
                    receipt.Result = RequestLotteryResult.NoDiamond;
                    return true;
                }
                UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
                //receipt.Result = RequestLotteryResult.NoTimes;
                //return true;
            }

            BigInteger awardGold = 0;
            int awardDiamond = 0;
            List<ItemData> awardItem = new List<ItemData>();

            int lotteryId = 0;
            LotteryAwardType awardType = LotteryAwardType.Gold;
            GetLottery.StealTimes = 0;
            GetLottery.RobTimes = 0;
            for (int i = 0; i < times; ++i)
            {
                if (GetLottery.TotalCount == 0)
                {
                    lotteryId = 7;
                    awardType = LotteryAwardType.Steal;
                }
                else if (GetLottery.TotalCount == 1)
                {
                    lotteryId = 8;
                    awardType = LotteryAwardType.Rob;
                }
                else
                {
                    var randlottery = UserHelper.RandomLottery(GetBasis.UserLv);
                    if (randlottery != null)
                    {
                        lotteryId = randlottery.ID;
                        awardType = randlottery.Type;
                    }
                }

                Config_Lottery lott = new ShareCacheStruct<Config_Lottery>().FindKey(lotteryId);
                if (lott == null)
                {
                    return false;
                }

                LottItem item = new LottItem();
                item.ID = lotteryId;
                item.Type = awardType;
                switch (lott.Type)
                {
                    case LotteryAwardType.Gold:
                        {
                            BigInteger resourceNum = BigInteger.Parse(lott.Content);
                            BigInteger value = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * resourceNum;
                            awardGold += value;

                            item.ItemID = 0;
                            item.Num = value.ToString();
                        }
                        break;
                    case LotteryAwardType.Diamond:
                        {
                            awardDiamond += lott.Content.ToInt();
                            item.ItemID = 0;
                            item.Num = lott.Content;
                        }
                        break;
                    case LotteryAwardType.Gem:
                        {
                            int maxCount = lott.Content.ToInt();
                            if (maxCount > 0)
                            {
                                var lotteryGem = UserHelper.RandomLotteryGem();

                                item.ItemID = lotteryGem.ID;
                                item.Num = maxCount.ToString();

                                awardItem.Add(new ItemData() { ID = lotteryGem.ID, Num = maxCount });
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
                            if (maxCount > 0)
                            {
                                int index = random.Next(debris.Count);
                                int awardId = debris[index].ItemID;

                                item.ItemID = awardId;
                                item.Num = maxCount.ToString();

                                awardItem.Add(new ItemData() { ID = awardId, Num = maxCount });
                            }
                        }
                        break;
                    case LotteryAwardType.Elf:
                        {
                            item.ItemID = lott.Content.ToInt();
                            item.Num = "1";

                            awardItem.Add(new ItemData() { ID = lott.Content.ToInt(), Num = 1 });

                            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(item.ItemID);
                            if (itemcfg != null)
                            {
                                string context = string.Format("恭喜 {0} 在幸运大夺宝中获得精灵 {1}",
                                    GetBasis.NickName, itemcfg.ItemName);
                                GlobalRemoteService.SendNotice(NoticeMode.World, context);
                            }
                        }
                        break;
                    case LotteryAwardType.Steal:
                        {
                            GetLottery.StealTimes++;
                            UserHelper.RandomStealTarget(Current.UserId);
                        }
                        break;
                    case LotteryAwardType.Rob:
                        {
                            GetLottery.RobTimes++;
                            UserHelper.RandomRobTarget(Current.UserId);
                        }
                        break;
                    case LotteryAwardType.Vit:
                        {
                            GetBasis.Vit += lott.Content.ToInt();
                            PushMessageHelper.UserVitChangedNotification(Current);
                        }
                        break;
                }
                receipt.AwardList.Add(item);

                GetLottery.TotalCount++;
            }

            UserHelper.RewardsGold(Current.UserId, awardGold, UpdateCoinOperate.NormalReward, true);
            UserHelper.RewardsDiamond(Current.UserId, awardDiamond);
            UserHelper.RewardsItems(Current.UserId, awardItem);

            if (GetLottery.LotteryTimes == ConfigEnvSet.GetInt("User.LotteryTimesMax"))
            {
                GetLottery.StartRestoreLotteryTimesDate = DateTime.Now;
            }
            if (GetLottery.LotteryTimes > 0)
                GetLottery.LotteryTimes = MathUtils.Subtraction(GetLottery.LotteryTimes, times, 0);
            
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.Lottery, times);

            receipt.Result = RequestLotteryResult.OK;

            return true;
        }
    }
}