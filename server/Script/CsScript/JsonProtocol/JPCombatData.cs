using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{

    public class CombatMatchUserData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public string AvatarUrl { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public int VipLv { get; set; }

        public int FightingValue { get; set; }

    }
    public class CombatMatchData
    {
        public CombatMatchData()
        {
            RivalList = new CacheList<CombatMatchUserData>();
            //LogList = new CacheList<JPCombatLogData>();
        }
        public int RankId { get; set; }

        public int CombatTimes { get; set; }

        public long LastFailedTime { get; set; }

        public CacheList<CombatMatchUserData> RivalList { get; set; }

        // public CacheList<JPCombatLogData> LogList { get; set; }


    }

    public class CombatRivalData
    {
        public CombatRivalData()
        {

        }
        public CombatReqRivalResult Result { get; set; }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int RankId { get; set; }

        public int UserLv { get; set; }

        public string AvatarUrl { get; set; }

        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserSkillCache Skill { get; set; }

    }


    public class CombatFightEndData
    {
        public EventStatus Result { get; set; }

        public int CurrRankId { get; set; }

        public int RankRise { get; set; }

        public long LastFailedTime { get; set; }

        public string AwardGold { get; set; }
        
    }

    public class MatchRivalData
    {
        public MatchRivalData()
        {

        }
        public MatchRivalResult Result { get; set; }

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int CombatRankID { get; set; }

        public int LevelRankID { get; set; }

        public int UserLv { get; set; }

        public string AvatarUrl { get; set; }

        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserSkillCache Skill { get; set; }

    }

}
