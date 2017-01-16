using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPQueryUserData
    {
        public JPQueryUserData()
        {
            CombatItemList = new List<ItemData>();
        }
        public int UserId { get; set; }
        
        public string NickName { get; set; }

        public int LooksId { get; set; }

        public string ClassName { get; set; }

        public bool IsOnline { get; set; }

        public int FightValue { get; set; }

        public int VipLv { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Hp { get; set; }

        public SubjectStage UserStage { get; set; }

        public int CombatRankId { get; set; }

        public List<ItemData> CombatItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }

        public CacheList<int> SkillCarryList { get; set; }
    }
}
