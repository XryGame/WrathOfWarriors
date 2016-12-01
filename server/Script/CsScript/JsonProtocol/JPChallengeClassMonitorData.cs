using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPChallengeClassMonitorData
    {
        public RequestChallengeClassMonitorResult result { get; set; }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int LooksId { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int HP { get; set; }

        public int FightValue { get; set; }

        public int RankId { get; set; }

        public CacheList<ItemData> ItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }

        public CacheList<int> SkillCarryList { get; set; }

        public bool IsUnlockSelectMap { get; set; }
    }
}
