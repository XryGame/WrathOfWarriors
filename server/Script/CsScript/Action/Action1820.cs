using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 20200_领取首充奖励
    /// </summary>
    public class Action1820 : BaseAction
    {
        private JPRequestSFOData receipt;
        private int AwardDiamondNum = ConfigEnvSet.GetInt("System.FirstPayAwardDiamondNum");
        private int AwardItemId = ConfigEnvSet.GetInt("System.FirstPayAwardItemID");
        public Action1820(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1820, actionGetter)
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
            UserPayCache usepay = UserHelper.FindUserPay(GetBasis.UserID);
            if (usepay == null || usepay.PayMoney == 0)
                return false;
            receipt = new JPRequestSFOData();
            receipt.Result = EventStatus.Bad;
            if (usepay.IsReceiveFirstPay)
                return true;
            usepay.IsReceiveFirstPay = true;
            receipt.Result = EventStatus.Good;

            UserHelper.RewardsDiamond(GetBasis.UserID, AwardDiamondNum);
            Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(AwardItemId);
            if (item != null)
            {
                GetPackage.AddItem(AwardItemId, 1);
            }

            receipt.AwardDiamondNum = AwardDiamondNum;
            receipt.CurrDiamond = GetBasis.DiamondNum;
            receipt.AwardItemList.Add(AwardItemId);

            return true;
        }
    }
}