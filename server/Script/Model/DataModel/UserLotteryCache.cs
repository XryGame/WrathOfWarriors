
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
    /// 用户抽奖信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserLotteryCache : BaseEntity
    {
        
        public UserLotteryCache()
            : base(AccessLevel.ReadWrite)
        {
            StealList = new CacheList<StealRobTarget>();
            Rob = new StealRobTarget();
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
        /// 当天抽奖剩余次数
        /// </summary>
        private int _LotteryTimes;
        [ProtoMember(2)]
        [EntityField("LotteryTimes")]
        public int LotteryTimes
        {
            get
            {
                return _LotteryTimes;
            }
            set
            {
                SetChange("LotteryTimes", value);
            }
        }
        /// <summary>
        /// 开始恢复抽奖次数时间
        /// </summary>
        private DateTime _StartRestoreLotteryTimesDate;
        [ProtoMember(3)]
        [EntityField("StartRestoreLotteryTimesDate")]
        public DateTime StartRestoreLotteryTimesDate
        {
            get
            {
                return _StartRestoreLotteryTimesDate;
            }
            set
            {
                SetChange("StartRestoreLotteryTimesDate", value);
            }
        }

        /// <summary>
        /// 偷取目标
        /// </summary>
        private CacheList<StealRobTarget> _StealList;
        [ProtoMember(4)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<StealRobTarget> StealList
        {
            get
            {
                return _StealList;
            }
            set
            {
                SetChange("StealList", value);
            }
        }

        /// <summary>
        /// 盗抢目标
        /// </summary>
        private StealRobTarget _Rob;
        [ProtoMember(5)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public StealRobTarget Rob
        {
            get
            {
                return _Rob;
            }
            set
            {
                SetChange("Rob", value);
            }
        }

        /// <summary>
        /// 偷取有效次数
        /// </summary>
        private int _StealTimes;
        [ProtoMember(6)]
        [EntityField("StealTimes")]
        public int StealTimes
        {
            get
            {
                return _StealTimes;
            }
            set
            {
                SetChange("StealTimes", value);
            }
        }

        [ProtoMember(7)]
        public long RemainTimeSec { get; set; }

        /// <summary>
        /// 抢夺有效次数
        /// </summary>
        private int _RobTimes;
        [ProtoMember(8)]
        [EntityField("RobTimes")]
        public int RobTimes
        {
            get
            {
                return _RobTimes;
            }
            set
            {
                SetChange("RobTimes", value);
            }
        }

        /// <summary>
        /// 抽奖总计数
        /// </summary>
        private int _TotalCount;
        [ProtoMember(9)]
        [EntityField("TotalCount")]
        public int TotalCount
        {
            get
            {
                return _TotalCount;
            }
            set
            {
                SetChange("TotalCount", value);
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
                    case "LotteryTimes": return LotteryTimes;
                    case "StartRestoreLotteryTimesDate": return StartRestoreLotteryTimesDate;
                    case "StealList": return StealList;
                    case "Rob": return Rob;
                    case "StealTimes": return StealTimes;
                    case "RobTimes": return RobTimes;
                    case "TotalCount": return TotalCount;
                    default: throw new ArgumentException(string.Format("UserLotteryCache index[{0}] isn't exist.", index));
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
                    case "LotteryTimes":
                        _LotteryTimes = value.ToInt();
                        break;
                    case "StartRestoreLotteryTimesDate":
                        _StartRestoreLotteryTimesDate = value.ToDateTime();
                        break;
                    case "StealList":
                        _StealList = ConvertCustomField<CacheList<StealRobTarget>>(value, index);
                        break;
                    case "Rob":
                        _Rob = ConvertCustomField<StealRobTarget>(value, index);
                        break;
                    case "StealTimes":
                        _StealTimes = value.ToInt();
                        break;
                    case "RobTimes":
                        _RobTimes = value.ToInt();
                        break;
                    case "TotalCount":
                        _TotalCount = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserLotteryCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public void ResetCache()
        {
            LotteryTimes = ConfigEnvSet.GetInt("User.LotteryTimesMax");
            StartRestoreLotteryTimesDate = DateTime.Now;
        }


    }
}