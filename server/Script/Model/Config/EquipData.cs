
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 装备数据
    /// </summary>
    [Serializable, ProtoContract]
    public class EquipData : EntityChangeEvent
    {

        public EquipData()
            : base(false)
        {
        }

        /// <summary>
        /// 装备ID
        /// </summary>
        [ProtoMember(1)]
        public EquipID ID { get; set; }

        /// <summary>
        /// 装备等级
        /// </summary>
        [ProtoMember(2)]
        public int Lv { get; set; }

        /// <summary>
        /// 攻击宝石
        /// </summary>
        [ProtoMember(3)]
        public int AtkGem { get; set; }

        /// <summary>
        /// 防御宝石
        /// </summary>
        [ProtoMember(4)]
        public int DefGem { get; set; }

        /// <summary>
        /// 生命宝石
        /// </summary>
        [ProtoMember(5)]
        public int HpGem { get; set; }

        /// <summary>
        /// 暴击宝石
        /// </summary>
        [ProtoMember(6)]
        public int CritGem { get; set; }

        /// <summary>
        /// 命中宝石
        /// </summary>
        [ProtoMember(7)]
        public int HitGem { get; set; }

        /// <summary>
        /// 闪避宝石
        /// </summary>
        [ProtoMember(8)]
        public int DodgeGem { get; set; }

        /// <summary>
        /// 韧性宝石
        /// </summary>
        [ProtoMember(9)]
        public int TenacityGem { get; set; }

    }
}