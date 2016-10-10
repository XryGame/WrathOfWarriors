using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCombatMatchUserData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int LooksId { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public int FightingValue { get; set; }

        public CacheList<int> SkillCarryList { get; set; }
    }
}
