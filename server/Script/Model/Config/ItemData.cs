
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 背包格子数据
    /// </summary>
    [Serializable, ProtoContract]
    public class ItemData : EntityChangeEvent
    {

        public ItemData()
            : base(false)
        {
        }

        /// <summary>
        /// 道具ID
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [ProtoMember(2)]
        public int Num { get; set; }

    }
}