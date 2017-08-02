using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1125_宝石合成
    /// </summary>
    public class Action1126 : BaseAction
    {
        private UsedItemResult receipt;
        private int gemID;
        

        public Action1126(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1126, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("GemID", ref gemID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            ItemData gemData = GetPackage.FindItem(gemID);
            if (gemData == null)
            {
                receipt = UsedItemResult.NoItem;
                return true;
            }

            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(gemID);
            var gemcfg = new ShareCacheStruct<Config_Gem>().Find(t => (t.ItemID == gemID));
            if (itemcfg == null || gemcfg == null || itemcfg.ItemType != ItemType.Gem)
            {
                return false;
            }
            var nextItemcfg = new ShareCacheStruct<Config_Item>().Find(t => (
                t.ItemType == ItemType.Gem && t.Species == itemcfg.Species && (t.ItemGrade == itemcfg.ItemGrade + 1))
                );
            if (nextItemcfg == null)
            {
                return false;
            }
            int compoundNum = gemData.Num / gemcfg.Number;
            int needNum = compoundNum * gemcfg.Number;
            if (gemData.Num < needNum)
            {
                receipt = UsedItemResult.ItemNumError;
                return true;
            }

            UserHelper.RewardsItem(Current.UserId, nextItemcfg.ItemID, compoundNum);
            receipt = UsedItemResult.Successfully;
            
            GetPackage.RemoveItem(gemID, needNum);

            return true;
        }
    }
}