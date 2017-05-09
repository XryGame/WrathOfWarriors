using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 购买金币
    /// </summary>
    public class Action1850 : BaseAction
    {
        private bool receipt;

        public Action1850(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1850, actionGetter)
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
            //var vip = new ShareCacheStruct<Config_Vip>().FindKey(GetBasis.VipLv);
            var purlist = new ShareCacheStruct<Config_Purchase>().FindAll();
            var pur = purlist.Find(t => t.id == GetPay.BuyGoldTimes + 1);
            if (pur == null)
                pur = purlist[purlist.Count - 1];
            if (/*vip == null || */pur == null)
            {
                return false;
            }

            //int canBuyTimes = vip.BuyStamina;

            //if (GetPay.BuyGoldTimes >= canBuyTimes)
            //{
            //    return true;
            //}
            int needDiamond = pur.SpendDiamond;

            if (GetBasis.DiamondNum < needDiamond)
            {
                return true;
            }

            BigInteger gold = BigInteger.Parse(pur.Gold);
            BigInteger value = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * gold;

            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            UserHelper.RewardsGold(Current.UserId, value);
            GetPay.BuyGoldTimes++;

            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.BuyGold, 1);
            receipt = true;
            return true;
        }

    }
}