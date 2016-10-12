
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 用户名人榜信息
    /// </summary>
    [Serializable, ProtoContract]
    public class UserCombatData : EntityChangeEvent
    {

        public UserCombatData()
            : base(false)
        {
        }
      
        /// <summary>
        /// 排名
        /// </summary>
        private int _RankID;
        [ProtoMember(1)]
        public int RankID
        {
            get
            {
                return _RankID;
            }
            set
            {
                _RankID = value;
            }
        }

        /// <summary>
        /// 挑战剩余次数
        /// </summary>
        private int _CombatTimes;
        [ProtoMember(2)]
        public int CombatTimes
        {
            get
            {
                return _CombatTimes;
            }
            set
            {
                _CombatTimes = value;
            }
        }

        /// <summary>
        /// 挑战失败时间
        /// </summary>
        private DateTime _LastFailedDate;
        [ProtoMember(3)]
        public DateTime LastFailedDate
        {
            get
            {
                return _LastFailedDate;
            }
            set
            {
                _LastFailedDate = value;
            }
        }
    }
}