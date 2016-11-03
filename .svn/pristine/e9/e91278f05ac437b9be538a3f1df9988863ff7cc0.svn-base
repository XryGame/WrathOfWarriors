using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCombatMatchData
    {
        public JPCombatMatchData()
        {
            RivalList = new CacheList<JPCombatMatchUserData>();
            LogList = new CacheList<JPCombatLogData>();
        }
        public int RankId { get; set; }

        public int CombatTimes { get; set; }

        public long LastFailedTime { get; set; }

        public CacheList<JPCombatMatchUserData> RivalList { get; set; }

        public CacheList<JPCombatLogData> LogList { get; set; }


    }
}
