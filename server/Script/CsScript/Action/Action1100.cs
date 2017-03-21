using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1100_使用背包物品
    /// </summary>
    public class Action1100 : BaseAction
    {
        private JPUsedItemReceipt receipt;

        private int itemId;
        private int useNum;

        public Action1100(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1100, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1401;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ItemID", ref itemId)
                && httpGet.GetInt("UseNum", ref useNum))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPUsedItemReceipt();
            receipt.Result = UsedItemResult.Successfully;

            var itemconfig = new ShareCacheStruct<Config_Item>().FindKey(itemId);
            if (itemconfig == null)
            {
                new BaseLog().SaveLog(string.Format("No found item config. ID={0}", itemId));
                return false;
            }
            
            var itemdata = GetPackage.FindItem(itemId);
            if (itemdata == null)
            {
                receipt.Result = UsedItemResult.NoItem;
                return true;
                
            }
            if (itemdata.Num < useNum)
            {
                receipt.Result = UsedItemResult.ItemNumError;
                return true;
            }

            BigInteger resourceNum = Util.ConvertGameCoin(itemconfig.ResourceNum);
            switch (itemconfig.ResourceType)
            {
                case ResourceType.Gold:
                    {
                        UserHelper.RewardsGold(Current.UserId, resourceNum * useNum);
                        //receipt.GainGold = resourceNum;
                    }
                    break;
                case ResourceType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, Convert.ToInt32(resourceNum * useNum));
                        //receipt.GainDiamond = resourceNum * useNum;
                    }
                    break;
                case ResourceType.Gift:
                    {
                        var giftconfig = new ShareCacheStruct<Config_Giftbag>().FindKey(itemconfig.ItemID);
                        if (giftconfig == null)
                        {
                            new BaseLog().SaveLog(string.Format("No found gift config. ID={0}", itemconfig.ResourceNum));
                            return false;
                        }

                        //receipt.GainItem = giftconfig.GetRewardsItem();
                        UserHelper.RewardsItems(Current.UserId, giftconfig.GetRewardsItem());
                    }
                    break;
                default:
                    {
                        receipt.Result = UsedItemResult.Cannot;
                        return true;
                    }
            }

            GetPackage.RemoveItem(itemId, useNum);
            return true;
        }
    }
}