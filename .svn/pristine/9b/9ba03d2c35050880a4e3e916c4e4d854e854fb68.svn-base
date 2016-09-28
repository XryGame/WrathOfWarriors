
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 道具数据
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
        /// 道具数量
        /// </summary>
        [ProtoMember(2)]
        public int Num { get; set; }

    }
}