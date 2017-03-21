using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.JsonProtocol
{

    public class ChatData
    {
        public ChatType Type { get; set; }

        public int Sender { get; set; }

        public string SenderName { get; set; }
        
        public int ServerID { get; set; }

        public long SendDate { get; set; }

        public string Content { get; set; }
    }
  
}