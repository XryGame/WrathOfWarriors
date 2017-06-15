using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 20200_领取首充奖励
    /// </summary>
    public class Action1820 : BaseAction
    {
        private bool receipt;

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
            UserPayCache usepay = UserHelper.FindUserPay(Current.UserId);
            if (usepay == null || usepay.PayMoney == 0)
                return false;
            //receipt = new JPRequestSFOData();
            //receipt.Result = EventStatus.Bad;
            if (usepay.IsReceiveFirstPay)
                return true;
            usepay.IsReceiveFirstPay = true;


            UserHelper.RewardsGold(Current.UserId, 100000);
            UserHelper.RewardsDiamond(Current.UserId, 20);
            UserHelper.RewardsItem(Current.UserId, 20024, 1);
            usepay.WeekCardDays += 7;

            PushMessageHelper.UserPaySucceedNotification(Current);
            receipt = true;
            return true;
        }
    }
}