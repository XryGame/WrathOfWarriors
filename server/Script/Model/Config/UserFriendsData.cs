using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 用户竞技场信息
    /// </summary>
    [Serializable, ProtoContract]
    public class UserFriendsData : EntityChangeEvent
    {

        public UserFriendsData()
            : base(false)
        {
            FriendsList = new CacheList<FriendData>();
            ApplyList = new CacheList<FriendApplyData>();
        }

        /// <summary>
        /// 好友UserId List
        /// </summary>
        private CacheList<FriendData> _FriendsList;
        [ProtoMember(1)]
        public CacheList<FriendData> FriendsList
        {
            get
            {
                return _FriendsList;
            }
            set
            {
                _FriendsList = value;
            }
        }

        /// <summary>
        /// 申请人UserId List
        /// </summary>
        private CacheList<FriendApplyData> _ApplyList;
        [ProtoMember(2)]
        public CacheList<FriendApplyData> ApplyList
        {
            get
            {
                return _ApplyList;
            }
            set
            {
                _ApplyList = value;
            }
        }

        /// <summary>
        /// 馈赠次数
        /// </summary>
        private int _GiveAwayCount;
        [ProtoMember(3)]
        public int GiveAwayCount
        {
            get
            {
                return _GiveAwayCount;
            }
            set
            {
                _GiveAwayCount = value;
            }
        }
    }
}
