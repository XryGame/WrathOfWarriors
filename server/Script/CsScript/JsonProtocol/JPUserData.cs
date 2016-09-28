using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPUserData
    {
        public int UserId { get; set; }

        public string SessionId { get; set; }

        public bool isCreated { get; set; }
    }
}
