using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCombatRivalData
    {
        public JPCombatRivalData()
        {
            SkillList = new List<SkillData>();
        }
        public CombatReqRivalResult Result { get; set; }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public int FightingValue { get; set; }
        
        public int Attack { get; set; }

        public int Defense { get; set; }

        public int HP { get; set; }
        
        public List<SkillData> SkillList { get; set; }

        public bool IsUnlockSelectMap { get; set; }

    }
}
