
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 受攻击日志数据
    /// </summary>
    [Serializable, ProtoContract]
    public class EnemyLogData : EntityChangeEvent
    {

        public EnemyLogData()
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
        /// 对方头像
        /// </summary>
        [ProtoMember(3)]
        public string RivalAvatarUrl { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [ProtoMember(4)]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 胜利还是失败
        /// </summary>
        [ProtoMember(5)]
        public EventStatus Status { get; set; }

        /// <summary>
        /// 等级下降
        /// </summary>
        [ProtoMember(6)]
        public int LevelDown { get; set; }

        /// <summary>
        /// 损失金币
        /// </summary>
        [ProtoMember(7)]
        public string LossGold { get; set; }

        /// <summary>
        /// 是否偷取
        /// </summary>
        [ProtoMember(8)]
        public bool IsSteal { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [ProtoMember(9)]
        public int RivalProfession { get; set; }

    }
}