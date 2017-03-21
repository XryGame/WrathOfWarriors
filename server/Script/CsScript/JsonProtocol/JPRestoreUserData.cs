using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRestoreUserData
    {
        public JPRestoreUserData()
        {
            //DailyQuestData = new JPDailyQuestData();
            EventAwardData = new JPEventAwardData();
        }

        public int Vit { get; set; }

        public int CombatTimes { get; set; }

        public int GiveAwayCount { get; set; }
        
        //public JPDailyQuestData DailyQuestData { get; set; }
        public UserTaskCache Task { get; set; }

        public JPEventAwardData EventAwardData { get; set; }

        public bool IsTodayLottery { get; set; }

        public LotteryAwardType LotteryAwardType { get; set; }

        public int LotteryId { get; set; }

        public int InviteFightDiamondNum { get; set; }

        public int WeekCardDays { get; set; }

        public int MonthCardDays { get; set; }
    }
}
