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
    /// 购买体力
    /// </summary>
    public class Action1860 : BaseAction
    {
        private bool receipt;

        public Action1860(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1860, actionGetter)
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
            var purlist = new ShareCacheStruct<Config_Purchase>().FindAll();
            var pur = purlist.Find(t => t.id == GetPay.BuyVitTimes + 1);
            if (pur == null)
                pur = purlist[purlist.Count - 1];
            if (/*vip == null || */pur == null)
            {
                return false;
            }

            //int canBuyTimes = vip.BuyStamina;

            //if (GetPay.BuyVitTimes >= canBuyTimes)
            //{
            //    return true;
            //}
            int needDiamond = pur.SpendDiamond;

            if (GetBasis.DiamondNum < needDiamond)
            {
                return true;
            }
            

            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            UserHelper.RewardsVit(Current.UserId, pur.Stamina);
            GetPay.BuyVitTimes++;
            
            receipt = true;
            return true;
        }

    }
}