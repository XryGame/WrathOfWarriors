using GameServer.CsScript.Com;
using System.Collections.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPComp64Role
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int VipLv { get; set; }
        
    }

    public class JPGroupX4
    {
        public JPGroupX4()
        {
            for (int i = 0; i < 4; ++i)
                Set[i] = new List<int>();
        }
        public List<int>[] Set = new List<int>[4];
    }

    public class JPGroupX2
    {
        public JPGroupX2()
        {
            for (int i = 0; i < 2; ++i)
                Set[i] = new List<int>();
        }
        public List<int>[] Set = new List<int>[2];
    }

    public class JPGroupX1
    {
        public List<int> Set = new List<int>();
    }

    public class JPCompetition64Data
    {
        public JPCompetition64Data()
        {
            Comp64RoleList = new List<JPComp64Role>();
            Group64 = new JPGroupX4();
            Group32 = new JPGroupX4();
            Group16 = new JPGroupX4();
            Group8 = new JPGroupX4();
            Group4 = new JPGroupX4();
            Group2 = new JPGroupX2();
            Group1 = new JPGroupX1();
        }
        public CompetitionStage Stage;
        public List<JPComp64Role> Comp64RoleList;
        public JPGroupX4 Group64;
        public JPGroupX4 Group32;
        public JPGroupX4 Group16;
        public JPGroupX4 Group8;
        public JPGroupX4 Group4;
        public JPGroupX2 Group2;
        public JPGroupX1 Group1;
    }
}
