using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class RestoreUserData
    {
        public RestoreUserData()
        {

        }
        public UserCombatCache Combat { get; set; }
        
        public UserTaskCache Task { get; set; }

        public UserEventAwardCache EventAward { get; set; }

        public int LotteryTimes { get; set; }

        public int SignStartID { get; set; }

        public int QuarterCardDays { get; set; }

        public int MonthCardDays { get; set; }
    }

    /// <summary>
    /// 1053_整点刷新通知接口
    /// </summary>
    public class Action1053 : BaseAction
    {
        private RestoreUserData receipt;
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


            receipt = new RestoreUserData();

            //receipt.Vit = GetBasis.Vit;
            //receipt.CombatTimes = GetCombat.CombatTimes;
            //receipt.GiveAwayCount = GetFriends.GiveAwayCount;

            receipt.Combat = GetCombat;

            receipt.Task = GetTask;

            receipt.LotteryTimes = GetBasis.LotteryTimes;

            receipt.SignStartID = DataHelper.SignStartID;

            receipt.EventAward = GetEventAward;
            //receipt.IsTodayLottery = false;
            //var lottery = new ShareCacheStruct<Config_Lottery>().FindKey(GetBasis.RandomLotteryId);
            //if (lottery != null)
            //{
            //    receipt.LotteryAwardType = lottery.Type;
            //    receipt.LotteryId = lottery.Content.ToInt();
            //}

            UserPayCache userpay = UserHelper.FindUserPay(Current.UserId);
            if (userpay != null)
            {
                receipt.QuarterCardDays = userpay.QuarterCardDays;
                receipt.MonthCardDays = userpay.MonthCardDays;
            }
            
            return true;
        }
    }
}