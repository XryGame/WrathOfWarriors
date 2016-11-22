using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPChangeNickNameData
    {
        public EventStatus Result { get; set; }

        public int CurrDiamond { get; set; }

        public string NewNickName { get; set; }
        
    }
}
