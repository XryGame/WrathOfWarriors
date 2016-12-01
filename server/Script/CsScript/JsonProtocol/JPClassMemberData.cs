using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPClassMemberData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int UserLv { get; set; }

        public int LooksId { get; set; }

        public int FightValue { get; set; }

        public CacheList<int> SkillCarryList { get; set; }
    }
}
