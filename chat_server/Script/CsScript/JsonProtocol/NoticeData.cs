using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.JsonProtocol
{

    public class NoticeData
    {
        public NoticeMode Type { get; set; }

        public int Sender { get; set; }

        public int ServerID { get; set; }

        public string Content { get; set; }
    }
  
}