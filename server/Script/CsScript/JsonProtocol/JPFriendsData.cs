using System;
using System.Collections.Generic;

namespace GameServer.CsScript.JsonProtocol
{

    public class JPFriendData
    {
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int UserLv { get; set; }

        public int VipLv { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsOnline { get; set; }

        public bool IsGiveAway { get; set; }

        public bool IsByGiveAway { get; set; }

        public bool IsReceiveGiveAway { get; set; }

        public string RobGold { get; set; }

    }

    public class JPFriendRobData
    {
        public int UserId { get; set; }

        public string Gold { get; set; }
    }
    public class JPFriendApplyData
    {

        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int UserLv { get; set; }

        public string AvatarUrl { get; set; }

        public int VipLv { get; set; }

        public DateTime ApplyTime { get; set; }

        public bool IsOnline { get; set; }

    }


    public class JPFriendsData
    {
        public JPFriendsData()
        {
            FriendsList = new List<JPFriendData>();
            ApplyList = new List<JPFriendApplyData>();
            //TodayRobList = new List<int>();
        }
        public int GiveAwayCount { get; set; }

        public List<JPFriendData> FriendsList;

        public List<JPFriendApplyData> ApplyList;

        //public List<int> TodayRobList;

    }
}
