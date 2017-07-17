
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户好友信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserFriendsCache : BaseEntity
    {

        public UserFriendsCache()
            : base(AccessLevel.ReadWrite)
        {
            FriendsList = new CacheList<FriendData>();
            ApplyList = new CacheList<FriendApplyData>();
            TodayRobList = new CacheList<int>();
            //ResetCache();
        }
        
        private int _UserID;
        [ProtoMember(1)]
        [EntityField("UserID", IsKey = true)]
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                SetChange("UserID", value);
            }
        }


        /// <summary>
        /// 好友UserId List
        /// </summary>
        private CacheList<FriendData> _FriendsList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<FriendData> FriendsList
        {
            get
            {
                return _FriendsList;
            }
            set
            {
                SetChange("FriendsList", value);
            }
        }

        /// <summary>
        /// 申请人UserId List
        /// </summary>
        private CacheList<FriendApplyData> _ApplyList;
        [ProtoMember(3)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<FriendApplyData> ApplyList
        {
            get
            {
                return _ApplyList;
            }
            set
            {
                SetChange("ApplyList", value);
            }
        }



        /// <summary>
        /// 馈赠次数
        /// </summary>
        private int _GiveAwayCount;
        [ProtoMember(4)]
        [EntityField("GiveAwayCount")]
        public int GiveAwayCount
        {
            get
            {
                return _GiveAwayCount;
            }
            set
            {
                SetChange("GiveAwayCount", value);
            }
        }

        /// <summary>
        /// 今天挑战记录
        /// </summary>
        private CacheList<int> _TodayRobList;
        [ProtoMember(5)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> TodayRobList
        {
            get
            {
                return _TodayRobList;
            }
            set
            {
                SetChange("TodayRobList", value);
            }
        }

        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "FriendsList": return FriendsList;
                    case "ApplyList": return ApplyList;
                    case "GiveAwayCount": return GiveAwayCount;
                    case "TodayRobList": return TodayRobList;
                    default: throw new ArgumentException(string.Format("UserFriendsCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserID":
                        _UserID = value.ToInt();
                        break;
                    case "FriendsList":
                        _FriendsList = ConvertCustomField<CacheList<FriendData>>(value, index);
                        break;
                    case "ApplyList":
                        _ApplyList = ConvertCustomField<CacheList<FriendApplyData>>(value, index);
                        break;
                    case "GiveAwayCount":
                        _GiveAwayCount = value.ToInt();
                        break;
                    case "TodayRobList":
                        _TodayRobList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserFriendsCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        /// <summary>
        /// 是否好友人数满了
        /// </summary>
        /// <returns></returns>
        public bool IsFriendNumFull()
        {
            return FriendsList.Count >= DataHelper.FriendCountMax;
        }

        /// <summary>
        /// 是否有此好友
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFriend(int uid)
        {
            return FriendsList.Find(t => (t.UserId == uid)) != null;
        }

        /// <summary>
        /// 是否有此好友申请
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFriendApply(int uid)
        {
            return ApplyList.Find(t => (t.UserId == uid)) != null;
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <returns></returns>
        public void AddFriend(int uid)
        {
            FriendData fd = FriendsList.Find(t => (t.UserId == uid));
            if (fd == null)
            {
                fd = new FriendData()
                {
                    UserId = uid,
                    IsGiveAway = false
                };
                FriendsList.Add(fd);
            }

        }

        /// <summary>
        /// 查找好友
        /// </summary>
        /// <returns></returns>
        public FriendData FindFriend(int uid)
        {
            return FriendsList.Find(t => (t.UserId == uid));
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <returns></returns>
        public void RemoveFriend(int uid)
        {
            FriendData fd = FriendsList.Find(t => (t.UserId == uid));
            if (fd != null)
                FriendsList.Remove(fd);
        }

        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <returns></returns>
        public void AddFriendApply(int uid)
        {
            if (ApplyList.Count >= DataHelper.FriendApplyCountMax)
            {
                ApplyList.RemoveAt(ApplyList.Count - 1);
            }
            FriendApplyData apply = new FriendApplyData()
            {
                UserId = uid,
                ApplyDate = DateTime.Now
            };
            ApplyList.Add(apply);
        }

        /// <summary>
        /// 查找好友申请
        /// </summary>
        /// <returns></returns>
        public FriendApplyData FindFriendApply(int uid)
        {
            return ApplyList.Find(t => (t.UserId == uid));
        }

        /// <summary>
        /// 是否有对方的馈赠
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFriendGiveAway(int uid)
        {
            FriendData fd = FindFriend(uid);
            return fd != null && fd.IsByGiveAway;
        }

        /// <summary>
        /// 添加挑战记录
        /// </summary>
        /// <returns></returns>
        public void AddRobRecord(int uid)
        {
            if (TodayRobList.Find(t => t == uid) == 0)
            {
                TodayRobList.Add(uid);
            }

        }

        public void ResetCache()
        {
            GiveAwayCount = 0;
            FriendsList.Clear();
            ApplyList.Clear();
            TodayRobList.Clear();
        }
    }
}