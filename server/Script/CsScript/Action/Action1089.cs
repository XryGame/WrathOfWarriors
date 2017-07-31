using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class FundChangeReceipt
    {
        public FundData Fund50;

        public FundData Fund98;

        public FundData Fund298;
    }
    public class Action1089 : BaseAction
    {
        /// <summary>
        /// 返利变更通知
        /// </summary>
        private FundChangeReceipt receipt;

        public Action1089(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1089, actionGetter)
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
            receipt = new FundChangeReceipt();
            receipt.Fund50 = GetPay.Fund50;
            receipt.Fund98 = GetPay.Fund98;
            receipt.Fund298 = GetPay.Fund298;
            return true;
        }
    }
}