
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 偷取盗抢目标
    /// </summary>
    [Serializable, ProtoContract]
    public class StealRobTarget : EntityChangeEvent
    {

        public StealRobTarget()
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
        ///金币
        /// </summary>
        [ProtoMember(4)]
        public string Gold { get; set; }

        /// <summary>
        /// 是否主目标
        /// </summary>
        [ProtoMember(5)]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// 对方等级
        /// </summary>
        [ProtoMember(6)]
        public int RivalLevel { get; set; }

        /// <summary>
        /// 随机金币数
        /// </summary>
        [ProtoMember(7)]
        public int RandGold { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        [ProtoMember(8)]
        public int RivalProfession { get; set; }
    }
}