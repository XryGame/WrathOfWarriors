
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
            Date = DateTime.Now;
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

        /// <summary>
        /// 是否新的
        /// </summary>
        [ProtoMember(3)]
        public bool IsNew { get; set; }

        /// <summary>
        /// 是否体验的
        /// </summary>
        [ProtoMember(4)]
        public bool IsExperience { get; set; }

        /// <summary>
        /// 体验期限(分)
        /// </summary>
        [ProtoMember(5)]
        public long ExperienceTimeMin { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [ProtoMember(6)]
        public DateTime Date { get; set; }
    }
}