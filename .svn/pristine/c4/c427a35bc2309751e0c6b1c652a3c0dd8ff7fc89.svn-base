
using System;
using ProtoBuf;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 用户事件奖励（签到，首周，在线时间）
    /// </summary>
    [Serializable, ProtoContract]
    public class UserEventAwardData : EntityChangeEvent
    {

        public UserEventAwardData()
            : base(false)
        {
        }
      
        /// <summary>
        /// 本月签到计数
        /// </summary>
        private int _SignCount;
        [ProtoMember(1)]
        public int SignCount
        {
            get
            {
                return _SignCount;
            }
            set
            {
                _SignCount = value;
            }
        }

        /// <summary>
        /// 今天是否签到
        /// </summary>
        private bool _IsTodaySign;
        [ProtoMember(2)]
        public bool IsTodaySign
        {
            get
            {
                return _IsTodaySign;
            }
            set
            {
                _IsTodaySign = value;
            }
        }

        /// <summary>
        /// 首周领取计数
        /// </summary>
        private int _FirstWeekCount;
        [ProtoMember(3)]
        public int FirstWeekCount
        {
            get
            {
                return _FirstWeekCount;
            }
            set
            {
                _FirstWeekCount = value;
            }
        }

        /// <summary>
        /// 今天是否领取首周奖励
        /// </summary>
        private bool _IsTodayReceiveFirstWeek;
        [ProtoMember(4)]
        public bool IsTodayReceiveFirstWeek
        {
            get
            {
                return _IsTodayReceiveFirstWeek;
            }
            set
            {
                _IsTodayReceiveFirstWeek = value;
            }
        }

        /// <summary>
        /// 在线时间计数(秒)
        /// </summary>
        private int _TodayOnlineTime;
        [ProtoMember(5)]
        public int TodayOnlineTime
        {
            get
            {
                return _TodayOnlineTime;
            }
            set
            {
                _TodayOnlineTime = value;
            }
        }

        /// <summary>
        /// 在线时间奖励阶段
        /// </summary>
        private int _OnlineAwardId;
        [ProtoMember(6)]
        public int OnlineAwardId
        {
            get
            {
                return _OnlineAwardId;
            }
            set
            {
                _OnlineAwardId = value;
            }
        }

        /// <summary>
        /// 在线开始计时时间
        /// </summary>
        private DateTime _OnlineStartTime;
        [ProtoMember(7)]
        public DateTime OnlineStartTime
        {
            get
            {
                return _OnlineStartTime;
            }
            set
            {
                _OnlineStartTime = value;
            }
        }
    }
}