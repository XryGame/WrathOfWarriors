using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestDeleteMailData
    {
        public JPRequestDeleteMailData()
        {
            MailList = new CacheList<MailData>();
        }

        public bool IsAll { get; set; }

        public EventStatus Result { get; set; }

        public CacheList<MailData> MailList { get; set; }
    }
}
