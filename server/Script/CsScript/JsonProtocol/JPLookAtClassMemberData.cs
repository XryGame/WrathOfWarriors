using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System.Collections.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPLookAtClassMemberData
    {
        public JPLookAtClassMemberData()
        {
            List = new List<JPClassMemberData>();
        }
        public int ClassId { get; set; }

        public string ClassName { get; set; }
        
        public List<JPClassMemberData> List { get; set; }
        
    }
}
