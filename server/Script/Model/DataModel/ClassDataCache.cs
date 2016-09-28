
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 班级 信息缓存
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class ClassDataCache : ShareEntity
    {

        public ClassDataCache()
            : base(AccessLevel.ReadWrite)
        {
            MemberList = new CacheList<int>();
        }
        
        private int _ClassID;
        [ProtoMember(1)]
        [EntityField("ClassID", IsKey = true)]
        public int ClassID
        {
            get
            {
                return _ClassID;
            }
            set
            {
                SetChange("ClassID", value);
            }
        }
        /// <summary>
        /// 班级名称
        /// </summary>
        private string _Name;
        [ProtoMember(2)]
        [EntityField("Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetChange("Name", value);
            }
        }
        /// <summary>
        /// 班级所在年级
        /// </summary>
        private int _Lv;
        [ProtoMember(3)]
        [EntityField("Lv")]
        public int Lv
        {
            get
            {
                return _Lv;
            }
            set
            {
                SetChange("Lv", value);
            }
        }


        /// <summary>
        /// 班级成员
        /// </summary>
        private CacheList<int> _MemberList;
        [ProtoMember(4)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<int> MemberList
        {
            get
            {
                return _MemberList;
            }
            set
            {
                SetChange("MemberList", value);
            }
        }

        /// <summary>
        /// 班长
        /// </summary>
        private int _Monitor;
        [ProtoMember(5)]
        [EntityField("Monitor")]
        public int Monitor
        {
            get
            {
                return _Monitor;
            }
            set
            {
                SetChange("Monitor", value);
            }
        }

        /// <summary>
        /// 班级成员是否在挑战班长
        /// </summary>
        [ProtoMember(6)]
        public bool IsChallenging
        {
            get;
            set;
        }

        /// <summary>
        /// 挑战班长的成员UserId
        /// </summary>
        [ProtoMember(7)]
        public int ChallengeUserId
        {
            get;
            set;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "ClassID": return ClassID;
                    case "Name": return Name;
                    case "Lv": return Lv;
                    case "MemberList": return MemberList;
                    case "Monitor": return Monitor;
                    default: throw new ArgumentException(string.Format("ClassDataCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "ClassID":
                        _ClassID = value.ToInt();
                        break;
                    case "Name":
                        _Name = value.ToNotNullString();
                        break;
                    case "Lv":
                        _Lv = value.ToInt();
                        break;
                    case "MemberList":
                        _MemberList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "Monitor":
                        _Monitor = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("ClassDataCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        protected override int GetIdentityId()
        {
            return ClassID;
        }
    }
}