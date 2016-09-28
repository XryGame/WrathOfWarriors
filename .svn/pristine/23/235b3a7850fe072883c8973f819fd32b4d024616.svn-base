
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
    public class SkillData : EntityChangeEvent
    {

        public SkillData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 技能ID
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }
        /// <summary>
        /// 技能等级
        /// </summary>
        [ProtoMember(2)]
        public int Lv { get; set; }

    }
}