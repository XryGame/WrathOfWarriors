using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1125_宝石合成
    /// </summary>
    public class Action1125 : BaseAction
    {
        private bool receipt;
        private int gemID, gemNum;

        private Random random = new Random();

        public Action1125(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1125, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("GemID", ref gemID)
                && httpGet.GetInt("GemNum", ref gemNum))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            ItemData gemData = GetPackage.FindItem(gemID);
            if (gemData == null || gemData.Num < gemNum)
            {
                return false;
            }

            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(gemID);
            var gemcfg = new ShareCacheStruct<Config_Gem>().Find(t => (t.ItemID == gemID));
            if (itemcfg == null || gemcfg == null)
            {
                return false;
            }

            float fprob = gemNum / gemcfg.Number;
            if (random.Next(1000) <= fprob * 1000)
            {
                GetPackage.RemoveItem(gemID, gemNum);
                GetPackage.AddItem(gemcfg.GemID, 1);
            }

            receipt = true;
            return true;
        }
    }
}