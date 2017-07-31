using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 领取充值返利
    /// </summary>
    public class Action1950 : BaseAction
    {
        private bool receipt;
        private int id;
        private PayID fundId;
        public Action1950(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1950, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref id)
                && httpGet.GetEnum("FundID", ref fundId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var fundcfg = new ShareCacheStruct<Config_Fund>().FindKey(id);
            if (fundcfg == null)
                return false;

            FundStageData funddata = new FundStageData();
            switch (fundId)
            {
                case PayID.Fund50:
                    funddata = GetPay.Fund50.List.Find(t => t.ID == id);
                    break;
                case PayID.Fund98:
                    funddata = GetPay.Fund98.List.Find(t => t.ID == id);
                    break;
                case PayID.Fund298:
                    funddata = GetPay.Fund298.List.Find(t => t.ID == id);
                    break;
            }

            if (funddata == null)
            {
                receipt = false;
                return true;
            }

            if (funddata.Status != FundStatus.Permit)
            {
                receipt = false;
                return true;
            }
            funddata.Status = FundStatus.Received;

            switch (fundId)
            {
                case PayID.Fund50:
                    UserHelper.RewardsDiamond(Current.UserId, fundcfg.fund50);
                    break;
                case PayID.Fund98:
                    UserHelper.RewardsDiamond(Current.UserId, fundcfg.fund98);
                    break;
                case PayID.Fund298:
                    UserHelper.RewardsDiamond(Current.UserId, fundcfg.fund298);
                    break;
            }

            PushMessageHelper.FundChangeNotification(Current);
            receipt = true;

            return true;
        }
    }
}