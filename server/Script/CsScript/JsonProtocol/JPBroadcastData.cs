using ZyGames.Framework.Game.Model;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPBroadcastData
    {
        public NoticeType Type { get; set; }

        public string Context { get; set; }

        public string Sender { get; set; }
    }
}
