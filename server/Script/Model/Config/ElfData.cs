
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 精灵数据
    /// </summary>
    [Serializable, ProtoContract]
    public class ElfData : EntityChangeEvent
    {

        public ElfData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 精灵ID
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }
        /// <summary>
        /// 精灵等级
        /// </summary>
        [ProtoMember(2)]
        public int Lv { get; set; }

    }
}