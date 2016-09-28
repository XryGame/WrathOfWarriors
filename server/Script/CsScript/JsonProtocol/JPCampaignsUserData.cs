using GameServer.Script.Model.Enum;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPCampaignsUserData
    {

        public int UserId { get; set; }

        public string NickName { get; set; }

        public string ClassName { get; set; }

        public int VoteCount { get; set; }

        public int LooksId { get; set; }
    }
}
