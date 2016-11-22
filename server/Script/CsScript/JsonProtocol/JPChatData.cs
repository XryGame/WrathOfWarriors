using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPChatData
    {
        public int Id { get; set; }

        public ChatType Type { get; set; }

        public ChatChildType ChildType { get; set; }

        public string Message { get; set; }

        public int UserId { get; set; }

        public string Sender { get; set; }

        public string SendTime { get; set; }

        public long SendTimestamp { get; set; }

        public int LooksId { get; set; }

        
    }
}
