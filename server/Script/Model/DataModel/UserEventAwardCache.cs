
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
    /// 签到，首周，在线奖励数据
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserEventAwardCache : BaseEntity
    {
        
        public UserEventAwardCache()
            : base(AccessLevel.ReadWrite)
        {
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
        /// 签到计数
        /// </summary>
        private int _SignCount;
        [ProtoMember(2)]
        [EntityField("SignCount")]
        public int SignCount
        {
            get
            {
                return _SignCount;
            }
            set
            {
                SetChange("SignCount", value);
            }
        }

        /// <summary>
        /// 今天是否签到
        /// </summary>
        private bool _IsTodaySign;
        [ProtoMember(3)]
        [EntityField("IsTodaySign")]
        public bool IsTodaySign
        {
            get
            {
                return _IsTodaySign;
            }
            set
            {
                SetChange("IsTodaySign", value);
            }
        }

        ///// <summary>
        ///// 首周领取计数
        ///// </summary>
        //private int _FirstWeekCount;
        //[ProtoMember(4)]
        //[EntityField("FirstWeekCount")]
        //public int FirstWeekCount
        //{
        //    get
        //    {
        //        return _FirstWeekCount;
        //    }
        //    set
        //    {
        //        SetChange("FirstWeekCount", value);
        //    }
        //}

        ///// <summary>
        ///// 今天是否领取首周奖励
        ///// </summary>
        //private bool _IsTodayReceiveFirstWeek;
        //[ProtoMember(5)]
        //[EntityField("IsTodayReceiveFirstWeek")]
        //public bool IsTodayReceiveFirstWeek
        //{
        //    get
        //    {
        //        return _IsTodayReceiveFirstWeek;
        //    }
        //    set
        //    {
        //        SetChange("IsTodayReceiveFirstWeek", value);
        //    }
        //}

        /// <summary>
        /// 在线时间奖励阶段
        /// </summary>
        private int _OnlineAwardId;
        [ProtoMember(6)]
        [EntityField("OnlineAwardId")]
        public int OnlineAwardId
        {
            get
            {
                return _OnlineAwardId;
            }
            set
            {
                SetChange("OnlineAwardId", value);
            }
        }

        /// <summary>
        /// 在线开始计时时间
        /// </summary>
        private DateTime _OnlineStartTime;
        [ProtoMember(7)]
        [EntityField("OnlineStartTime")]
        public DateTime OnlineStartTime
        {
            get
            {
                return _OnlineStartTime;
            }
            set
            {
                SetChange("OnlineStartTime", value);
            }
        }

        /// <summary>
        /// 是否已经领取或CDK
        /// </summary>
        private bool _IsReceivedCDK;
        [ProtoMember(8)]
        [EntityField("IsReceivedCDK")]
        public bool IsReceivedCDK
        {
            get
            {
                return _IsReceivedCDK;
            }
            set
            {
                SetChange("IsReceivedCDK", value);
            }
        }

        ///// <summary>
        ///// 上次领取在线奖时间
        ///// </summary>
        //private DateTime _LastOnlineAwayReceiveTime;
        //[ProtoMember(8)]
        //[EntityField("LastOnlineAwayReceiveTime")]
        //public DateTime LastOnlineAwayReceiveTime
        //{
        //    get
        //    {
        //        return _LastOnlineAwayReceiveTime;
        //    }
        //    set
        //    {
        //        SetChange("LastOnlineAwayReceiveTime", value);
        //    }
        //}

        ///// <summary>
        ///// 是否已经开始在线计时
        ///// </summary>
        //private bool _IsStartedOnlineTime;
        //[ProtoMember(9)]
        //[EntityField("IsStartedOnlineTime")]
        //public bool IsStartedOnlineTime
        //{
        //    get
        //    {
        //        return _IsStartedOnlineTime;
        //    }
        //    set
        //    {
        //        SetChange("IsStartedOnlineTime", value);
        //    }
        //}

        /// <summary>
        /// 签到周ID
        /// </summary>
        private int _SignStartID;
        [ProtoMember(10)]
        [EntityField("SignStartID")]
        public int SignStartID
        {
            get
            {
                return _SignStartID;
            }
            set
            {
                SetChange("SignStartID", value);
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
                    case "SignCount": return SignCount;
                    case "IsTodaySign": return IsTodaySign;
                    //case "FirstWeekCount": return FirstWeekCount;
                    //case "IsTodayReceiveFirstWeek": return IsTodayReceiveFirstWeek;
                    case "OnlineAwardId": return OnlineAwardId;
                    case "OnlineStartTime": return OnlineStartTime;
                    case "IsReceivedCDK": return IsReceivedCDK;
                    //case "LastOnlineAwayReceiveTime": return LastOnlineAwayReceiveTime;
                    //case "IsStartedOnlineTime": return IsStartedOnlineTime;
                    case "SignStartID": return SignStartID;
                    default: throw new ArgumentException(string.Format("UserEventAwardCache index[{0}] isn't exist.", index));
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
                    case "SignCount":
                        _SignCount = value.ToInt();
                        break;
                    case "IsTodaySign":
                        _IsTodaySign = value.ToBool();
                        break;
                    //case "FirstWeekCount":
                    //    _FirstWeekCount = value.ToInt();
                    //    break;
                    //case "IsTodayReceiveFirstWeek":
                    //    _IsTodayReceiveFirstWeek = value.ToBool();
                    //    break;
                    case "OnlineAwardId":
                        _OnlineAwardId = value.ToInt();
                        break;
                    case "OnlineStartTime":
                        _OnlineStartTime = value.ToDateTime();
                        break;
                    case "IsReceivedCDK":
                        _IsReceivedCDK = value.ToBool();
                        break;
                    //case "LastOnlineAwayReceiveTime":
                    //    _LastOnlineAwayReceiveTime = value.ToDateTime();
                    //    break;
                    //case "IsStartedOnlineTime":
                    //    _IsStartedOnlineTime = value.ToBool();
                    //    break; 
                    case "SignStartID":
                        _SignStartID = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserEventAwardCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetCache()
        {
            SignCount = 0;
            IsTodaySign = false;
            //FirstWeekCount = 0;
            //IsTodayReceiveFirstWeek = false;
            OnlineAwardId = 0;
            //LastOnlineAwayReceiveTime = DateTime.Now;
            //IsStartedOnlineTime = true;
            OnlineStartTime = DateTime.Now;

            IsReceivedCDK = false;

            SignStartID = DataHelper.SignStartID;
        }

    }
}