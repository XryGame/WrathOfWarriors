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
            GameUser user = UserHelper.FindUser(UserId);
            if (user == null)
                return false;

            receipt = new JPRestoreUserData();

            receipt.Vit = ContextUser.Vit;
            receipt.CombatTimes = ContextUser.CombatData.CombatTimes;
            receipt.CampaignTicketNum = ContextUser.CampaignTicketNum;
            receipt.GiveAwayCount = ContextUser.FriendsData.GiveAwayCount;
            receipt.ChallengeMonitorTimes = ContextUser.ChallengeMonitorTimes;

            receipt.DailyQuestData.ID = ContextUser.DailyQuestData.ID;
            receipt.DailyQuestData.IsFinish = ContextUser.DailyQuestData.IsFinish;
            receipt.DailyQuestData.RefreshCount = ContextUser.DailyQuestData.RefreshCount;
            receipt.DailyQuestData.FinishCount = ContextUser.DailyQuestData.FinishCount;
            receipt.DailyQuestData.Count = ContextUser.DailyQuestData.Count;

            receipt.IsTodayLottery = false;
            var lottery = new ShareCacheStruct<Config_Lottery>().FindKey(ContextUser.RandomLotteryId);
            if (lottery != null)
            {
                receipt.LotteryAwardType = lottery.Type;
                receipt.LotteryId = lottery.Content;
            }
            return true;
        }
    }
}