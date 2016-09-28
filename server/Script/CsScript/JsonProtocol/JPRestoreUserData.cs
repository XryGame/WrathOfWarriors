using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRestoreUserData
    {
        public JPRestoreUserData()
        {
            DailyQuestData = new JPDailyQuestData();
        }
        public JPDailyQuestData DailyQuestData { get; set; }
    }
}
