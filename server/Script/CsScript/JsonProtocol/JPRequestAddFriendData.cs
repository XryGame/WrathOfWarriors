using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestAddFriendData
    {
        public RequestFriendResult Result { get; set; }

        public int DestUid { get; set; }

        public string Nickname { get; set; }

        

    }
}
