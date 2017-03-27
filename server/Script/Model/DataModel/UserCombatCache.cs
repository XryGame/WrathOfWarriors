
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;
using System.Collections.Generic;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户竞技场信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserCombatCache : BaseEntity
    {
        
        public UserCombatCache()
            : base(AccessLevel.ReadWrite)
        {
            LogList = new CacheList<CombatLogData>();
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
        /// 挑战剩余次数
        /// </summary>
        private int _CombatTimes;
        [ProtoMember(2)]
        [EntityField("CombatTimes")]
        public int CombatTimes
        {
            get
            {
                return _CombatTimes;
            }
            set
            {
                SetChange("CombatTimes", value);
            }
        }

        /// <summary>
        /// 挑战失败时间
        /// </summary>
        private DateTime _LastFailedDate;
        [ProtoMember(3)]
        [EntityField("LastFailedDate")]
        public DateTime LastFailedDate
        {
            get
            {
                return _LastFailedDate;
            }
            set
            {
                SetChange("LastFailedDate", value);
            }
        }

        /// <summary>
        /// 购买名人榜挑战次数
        /// </summary>
        private int _ButTimes;
        [ProtoMember(4)]
        [EntityField("ButTimes")]
        public int ButTimes
        {
            get
            {
                return _ButTimes;
            }
            set
            {
                SetChange("ButTimes", value);
            }
        }


        private CacheList<CombatLogData> _LogList;
        [ProtoMember(5)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<CombatLogData> LogList
        {
            get
            {
                return _LogList;
            }
            set
            {
                SetChange("LogList", value);
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
                    case "CombatTimes": return CombatTimes;
                    case "LastFailedDate": return LastFailedDate;
                    case "ButTimes": return ButTimes;
                    case "LogList": return LogList;
                    default: throw new ArgumentException(string.Format("UserCombatCache index[{0}] isn't exist.", index));
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
                    case "CombatTimes":
                        _CombatTimes = value.ToInt();
                        break;
                    case "LastFailedDate":
                        _LastFailedDate = value.ToDateTime();
                        break;
                    case "ButTimes":
                        _ButTimes = value.ToInt();
                        break;
                    case "LogList":
                        _LogList = ConvertCustomField<CacheList<CombatLogData>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserCombatCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetCache()
        {
            LastFailedDate = DateTime.MinValue;
            CombatTimes = 0;
            ButTimes = 0;
            LogList.Clear();
            

        }

        public void PushCombatLog(CombatLogData log)
        {
            if (LogList.Count >= DataHelper.CombatLogCountMax)
            {
                LogList.RemoveAt(0);
            }

            LogList.Add(log);
        }




    }
}