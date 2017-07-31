using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{

    public class JPRankAwardData
    {
        public int LevelRankID { get; set; }

        public int FightValueRankID { get; set; }

        public int ComboRankID { get; set; }

        public bool IsReceivedLevel { get; set; }

        public bool IsReceivedFightValue { get; set; }

        public bool IsReceivedCombo { get; set; }
    }
}
