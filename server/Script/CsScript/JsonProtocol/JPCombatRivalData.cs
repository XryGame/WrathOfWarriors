using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCombatRivalData
    {
        public JPCombatRivalData()
        {

        }
        public CombatReqRivalResult Result { get; set; }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }
        
        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserSkillCache Skill { get; set; }
        


    }
}
