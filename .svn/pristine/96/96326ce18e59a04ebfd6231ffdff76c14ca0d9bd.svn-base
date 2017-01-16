
using System;
using ProtoBuf;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 分享数据
    /// </summary>
    [Serializable, ProtoContract]
    public class UserShareData : EntityChangeEvent
    {

        public UserShareData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 分享次数
        /// </summary>
        [ProtoMember(1)]
        public TaskType Count { get; set; }

        /// <summary>
        ///  是否领取
        /// </summary>
        [ProtoMember(2)]
        public bool IsReceived { get; set; }
        

    }
}