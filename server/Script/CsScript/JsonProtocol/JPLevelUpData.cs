using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPLevelUpData
    {
        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Hp { get; set; }

        public int CurrLevel { get; set; }

        public bool IsChangeClass { get; set; }

    }
}
