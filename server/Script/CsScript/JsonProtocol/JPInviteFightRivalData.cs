using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPInviteFightRivalData
    {
        public JPInviteFightRivalData()
        {
            RivalData = new JPRivalUserData();
        }
        
        public EventStatus AppointResult { get; set; }

        public JPRivalUserData RivalData { get; set; }

    }
}
