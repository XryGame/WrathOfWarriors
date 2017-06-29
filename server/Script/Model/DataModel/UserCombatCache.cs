
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
        /// 购买通天塔挑战次数
        /// </summary>
        private int _BuyTimes;
        [ProtoMember(4)]
        [EntityField("BuyTimes")]
        public int BuyTimes
        {
            get
            {
                return _BuyTimes;
            }
            set
            {
                SetChange("BuyTimes", value);
            }
        }

        /// <summary>
        /// 竞技场匹配剩余次数
        /// </summary>
        private int _MatchTimes;
        [ProtoMember(5)]
        [EntityField("MatchTimes")]
        public int MatchTimes
        {
            get
            {
                return _MatchTimes;
            }
            set
            {
                SetChange("MatchTimes", value);
            }
        }

        /// <summary>
        /// 购买竞技场匹配次数
        /// </summary>
        private int _BuyMatchTimes;
        [ProtoMember(6)]
        [EntityField("BuyMatchTimes")]
        public int BuyMatchTimes
        {
            get
            {
                return _BuyMatchTimes;
            }
            set
            {
                SetChange("BuyMatchTimes", value);
            }
        }


        private CacheList<CombatLogData> _LogList;
        [ProtoMember(7)]
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

        /// <summary>
        /// 竞技币
        /// </summary>
        private int _CombatCoin;
        [ProtoMember(8)]
        [EntityField("CombatCoin")]
        public int CombatCoin
        {
            get
            {
                return _CombatCoin;
            }
            set
            {
                SetChange("CombatCoin", value);
            }
        }

        /// <summary>
        /// 匹配挑战失败时间
        /// </summary>
        private DateTime _LastMatchFightFailedDate;
        [ProtoMember(9)]
        [EntityField("LastMatchFightFailedDate")]
        public DateTime LastMatchFightFailedDate
        {
            get
            {
                return _LastMatchFightFailedDate;
            }
            set
            {
                SetChange("LastMatchFightFailedDate", value);
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
                    case "BuyTimes": return BuyTimes;
                    case "MatchTimes": return MatchTimes;
                    case "BuyMatchTimes": return BuyMatchTimes;
                    case "LogList": return LogList;
                    case "CombatCoin": return CombatCoin;
                    case "LastMatchFightFailedDate": return LastMatchFightFailedDate;
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
                    case "BuyTimes":
                        _BuyTimes = value.ToInt();
                        break;
                    case "MatchTimes":
                        _MatchTimes = value.ToInt();
                        break;
                    case "BuyMatchTimes":
                        _BuyMatchTimes = value.ToInt();
                        break;
                    case "LogList":
                        _LogList = ConvertCustomField<CacheList<CombatLogData>>(value, index);
                        break;
                    case "CombatCoin":
                        _CombatCoin = value.ToInt();
                        break;
                    case "LastMatchFightFailedDate":
                        _LastMatchFightFailedDate = value.ToDateTime();
                        break;
                    default: throw new ArgumentException(string.Format("UserCombatCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetCache()
        {
            TimeSpan timespan = TimeSpan.FromMinutes(10);
            LastFailedDate = DateTime.Now.Subtract(timespan);
            LastMatchFightFailedDate = DateTime.Now.Subtract(timespan);
            CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            BuyTimes = 0;
            MatchTimes = ConfigEnvSet.GetInt("Combat.MatchTimes");
            BuyMatchTimes = 0;
            LogList.Clear();
            CombatCoin = 0;

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