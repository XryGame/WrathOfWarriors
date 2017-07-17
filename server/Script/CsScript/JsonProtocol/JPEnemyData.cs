using GameServer.Script.Model.Config;
using System;
using System.Collections.Generic;

namespace GameServer.CsScript.JsonProtocol
{

    public class JPEnemyData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int UserLv { get; set; }

        public string AvatarUrl { get; set; }

        public string RobGold { get; set; }
        
    }

    public class JPEnemyRobData
    {
        public int UserId { get; set; }

        public string Gold { get; set; }

    }


    public class JPEnemysData
    {
        public JPEnemysData()
        {
            EnemysList = new List<JPEnemyData>();
            LogList = new List<EnemyLogData>();
        }

        public List<JPEnemyData> EnemysList;

        public List<EnemyLogData> LogList;

        public bool IsHaveNewLog { get; set; }

    }
}
