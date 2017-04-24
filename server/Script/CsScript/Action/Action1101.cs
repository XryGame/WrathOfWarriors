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
    /// 1101_出售背包物品
    /// </summary>
    public class Action1101 : BaseAction
    {
        private UsedItemResult receipt;

        private int itemId;
        private int useNum;

        public Action1101(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1101, actionGetter)
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

            if (!itemconfig.IfSell)
            {
                receipt = UsedItemResult.Unavailable;
                return true;
            }
            

            BigInteger resourceNum = Util.ConvertGameCoin(itemconfig.SellGold);
            UserHelper.RewardsGold(Current.UserId, resourceNum * useNum, UpdateGoldType.UserItemReward);
            //receipt.GainGold = resourceNum;

            GetPackage.RemoveItem(itemId, useNum);
            return true;
        }
    }
}