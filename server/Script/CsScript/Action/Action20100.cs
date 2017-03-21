using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 20100_请求抽奖
    /// </summary>
    public class Action20100 : BaseAction
    {
        private JPLotteryData receipt;
        
        public Action20100(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action20100, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action20100;
            }
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            receipt = new JPLotteryData();
            
            Config_Lottery lott = new ShareCacheStruct<Config_Lottery>().FindKey(GetBasis.RandomLotteryId);

            if (lott == null)
                return false;

            if (GetBasis.IsTodayLottery)
            {
                int needDiamond = ConfigEnvSet.GetInt("User.LotteryNeedDiamondNum");
                if (GetBasis.DiamondNum < needDiamond)
                {
                    receipt.Result = RequestLotteryResult.NoDiamond;
                    return true;
                }
                UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            }


            //receipt.Type = lott.Type;
            //switch (lott.Type)
            //{
            //    case LotteryAwardType.Diamond:
            //        {
            //            UserHelper.RewardsDiamond(GetBasis.UserID, lott.Content);
            //            receipt.AwardNum = lott.Content;
            //        }
            //        break;
            //    case LotteryAwardType.Item:
            //        {
            //            Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(lott.Content);
            //            if (item != null)
            //            {
            //                if (item.Type == ItemType.Item)
            //                {
            //                    if (!GetPackage.AddItem(lott.Content, 1))
            //                    {
            //                        receipt.Result = RequestLotteryResult.Full;
            //                        return true;
            //                    }
            //                }
            //                receipt.AwardItemId = lott.Content;
            //                receipt.AwardNum = 1;
            //            }
            //        }
            //        break;
            //}

            //GetBasis.IsTodayLottery = true;
            ////GetBasis.RandomLotteryId = 0;
            //GetBasis.LastLotteryId = GetBasis.RandomLotteryId;

            //var lottery = UserHelper.RandomLottery(GetBasis.UserID, GetBasis.UserLv);
            //if (lottery != null)
            //{
            //    GetBasis.RandomLotteryId = lottery.ID;
            //    receipt.LotteryAwardType = lottery.Type;
            //    receipt.LotteryId = lottery.Content;
            //}

            receipt.Result = RequestLotteryResult.OK;

            return true;
        }
    }
}