using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10500_购买体力
    /// </summary>
    public class Action10500 : BaseAction
    {
        private JPBuyData receipt;

        public Action10500(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10500, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            var vip = new ShareCacheStruct<Config_Vip>().FindKey(ContextUser.VipLv == 0 ? 1 : ContextUser.VipLv);
            if (vip == null)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "SubjectExp");
                return true;
            }
                
            int canBuyTimes = vip.BuyStamina;
            if (ContextUser.VipLv == 0)
                canBuyTimes -= 1;
            
            var purchase = new ShareCacheStruct<Config_Purchase>().FindKey(ContextUser.BuyVitCount + 1);

            if (ContextUser.BuyVitCount >= canBuyTimes || purchase == null)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            int needDiamond = purchase.SpendDiamond;


            if (ContextUser.DiamondNum < needDiamond)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
                    
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needDiamond);
            ContextUser.Vit = MathUtils.Addition(ContextUser.Vit, purchase.Stamina);
            ContextUser.BuyVitCount++;
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.Extend1 = ContextUser.Vit;
            return true;
        }
    }
}