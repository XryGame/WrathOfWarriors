using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1053_整点刷新通知接口
    /// </summary>
    public class Action1053 : BaseAction
    {
        private JPRestoreUserData receipt;
        public Action1053(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1053, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1053;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {


            receipt = new JPRestoreUserData();

            //receipt.Vit = GetBasis.Vit;
            receipt.CombatTimes = GetCombat.CombatTimes;
            receipt.GiveAwayCount = GetFriends.GiveAwayCount;

            receipt.Task = GetTask;

            receipt.IsTodayLottery = false;
            var lottery = new ShareCacheStruct<Config_Lottery>().FindKey(GetBasis.RandomLotteryId);
            if (lottery != null)
            {
                receipt.LotteryAwardType = lottery.Type;
                receipt.LotteryId = lottery.Content;
            }

            UserPayCache userpay = UserHelper.FindUserPay(Current.UserId);
            if (userpay != null)
            {
                receipt.WeekCardDays = userpay.WeekCardDays;
                receipt.MonthCardDays = userpay.MonthCardDays;
            }
            
            return true;
        }
    }
}