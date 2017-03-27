
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 成就数据
    /// </summary>
    [Serializable, ProtoContract]
    public class AchievementData : EntityChangeEvent
    {

        public AchievementData()
            : base(false)
        {
        }

        /// <summary>
        /// 成就类型
        /// </summary>
        [ProtoMember(1)]
        public AchievementType Type { get; set; }

        /// <summary>
        /// 成就ID
        /// </summary>
        [ProtoMember(2)]
        public int ID { get; set; }

        /// <summary>
        /// 成就完成计数
        /// </summary>
        [ProtoMember(3)]
        public string Count { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [ProtoMember(4)]
        public TaskStatus Status { get; set; }
        

    }
}