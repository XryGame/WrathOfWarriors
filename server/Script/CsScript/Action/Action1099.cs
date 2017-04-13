using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1099_充值成功通知
    /// </summary>
    public class Action1099 : BaseAction
    {
        public JPPaySucceedData receipt;

        public Action1099(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1099, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1099;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPPaySucceedData();
            receipt.VipLv = GetBasis.VipLv;
            receipt.Diamond = GetBasis.DiamondNum;
            receipt.BuyDiamond = GetBasis.BuyDiamond;
            receipt.PayMoney = GetPay.PayMoney;
            return true;
        }
    }
}