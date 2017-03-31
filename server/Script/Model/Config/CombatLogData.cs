
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 竞技场日志数据
    /// </summary>
    [Serializable, ProtoContract]
    public class CombatLogData : EntityChangeEvent
    {

        public CombatLogData()
            : base(false)
        {
        }

        /// <summary>
        /// 对方Uid
        /// </summary>
        [ProtoMember(1)]
        public int RivalUid { get; set; }

        /// <summary>
        /// 对方Uid
        /// </summary>
        [ProtoMember(2)]
        public string RivalName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [ProtoMember(3)]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [ProtoMember(4)]
        public EventType Type { get; set; }

        /// <summary>
        /// 胜利还是失败
        /// </summary>
        [ProtoMember(5)]
        public EventStatus Status { get; set; }

        /// <summary>
        /// 排名上升/下降
        /// </summary>
        [ProtoMember(6)]
        public int RankIdDiff { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        [ProtoMember(7)]
        public int RankId { get; set; }
    }
}