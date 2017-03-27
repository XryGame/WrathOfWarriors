using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;

namespace GameServer.CsScript.JsonProtocol
{

    public class JPGuildMemberData
    {
        public int UserID { get; set; }
        
        public int Profession { get; set; }
        
        public string NickName { get; set; }
        
        public int UserLv { get; set; }
        
        public int CombatRankID { get; set; }
        
        public GuildJobTitle JobTitle { get; set; }
        
        public int Liveness { get; set; }

        public bool IsOnline { get; set; }
    }

    public class JPGuildApplyData
    {

        public int UserID { get; set; }

        public int Profession { get; set; }

        public string NickName { get; set; }

        public int UserLv { get; set; }

        public int CombatRankID { get; set; }
        
        public bool IsOnline { get; set; }

        public DateTime ApplyTime { get; set; }

    }

    public class JPGuildLogData
    {
        
        public DateTime LogTime { get; set; }

        public int UserId { get; set; }
        
        public string UserName { get; set; }
        
        public string Content { get; set; }

    }


    public class JPGuildData
    {
        public JPGuildData()
        {
            MemberList = new List<JPGuildMemberData>();
            ApplyList = new List<JPGuildApplyData>();
            LogList = new List<JPGuildLogData>();
        }
        public string GuildID { get; set; }

        public string GuildName { get; set; }

        public int Liveness { get; set; }

        public string Notice { get; set; }

        public int RankID { get; set; }

        public DateTime CreateDate { get; set; }

        public List<JPGuildMemberData> MemberList;

        public List<JPGuildApplyData> ApplyList;

        public List<JPGuildLogData> LogList;

    }
}
