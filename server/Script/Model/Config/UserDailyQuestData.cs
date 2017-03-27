
using System;
using ProtoBuf;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 每日任务数据
    /// </summary>
    [Serializable, ProtoContract]
    public class UserDailyQuestData : EntityChangeEvent
    {

        public UserDailyQuestData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 任务ID
        /// </summary>
        [ProtoMember(1)]
        public TaskType ID { get; set; }


        /// <summary>
        /// 进度计数
        /// </summary>
        [ProtoMember(2)]
        public int Count { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [ProtoMember(3)]
        public TaskStatus Status { get; set; }
        

    }
}