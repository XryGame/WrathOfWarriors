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
using ZyGames.Framework.Common;
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
        private UsedItemResult receipt;

        private int itemId;
        private int useNum;

        public Action1100(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1100, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
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
            receipt = UsedItemResult.Successfully;

            var itemconfig = new ShareCacheStruct<Config_Item>().FindKey(itemId);
            if (itemconfig == null)
            {
                new BaseLog().SaveLog(string.Format("No found item config. ID={0}", itemId));
                return false;
            }
            
            var itemdata = GetPackage.FindItem(itemId);
            if (itemdata == null)
            {
                receipt = UsedItemResult.NoItem;
                return true;
                
            }
            if (itemdata.Num < useNum)
            {
                receipt = UsedItemResult.ItemNumError;
                return true;
            }
            
            switch (itemconfig.ResourceType)
            {
                case ResourceType.Gold:
                    {
                        BigInteger resourceNum = BigInteger.Parse(itemconfig.ResourceNum);
                        BigInteger value = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * resourceNum;
                        UserHelper.RewardsGold(Current.UserId, value * useNum, UpdateCoinOperate.UserItemReward);
                        //receipt.GainGold = resourceNum;
                    }
                    break;
                case ResourceType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, itemconfig.ResourceNum.ToInt() * useNum, UpdateCoinOperate.UseItem);
                        //receipt.GainDiamond = resourceNum * useNum;
                    }
                    break;
                case ResourceType.Gift:
                    {
                        var giftconfig = new ShareCacheStruct<Config_Giftbag>().Find(t => t.ItemID == itemconfig.ItemID);
                        if (giftconfig == null)
                        {
                            new BaseLog().SaveLog(string.Format("No found gift config. ID={0}", itemconfig.ItemID));
                            return false;
                        }

                        //receipt.GainItem = giftconfig.GetRewardsItem();
                        UserHelper.RewardsItems(Current.UserId, giftconfig.GetRewardsItem());
                    }
                    break;
                case ResourceType.Elf:
                    {
                        var elfconfig = new ShareCacheStruct<Config_Elves>().Find(t => t.ElvesID == itemconfig.ResourceNum.ToInt());
                        if (elfconfig == null)
                        {
                            new BaseLog().SaveLog(string.Format("No found elf config. ID={0}", itemconfig.ResourceNum));
                            return false;
                        }

                        //receipt.GainItem = giftconfig.GetRewardsItem();
                        UserHelper.RewardsElf(Current.UserId, elfconfig.ElvesID);
                    }
                    break;
                default:
                    {
                        receipt = UsedItemResult.Cannot;
                        return true;
                    }
            }

            GetPackage.RemoveItem(itemId, useNum);
            return true;
        }
    }
}