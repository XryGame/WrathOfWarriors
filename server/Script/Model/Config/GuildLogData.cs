
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 公会日志数据
    /// </summary>
    [Serializable, ProtoContract]
    public class GuildLogData : EntityChangeEvent
    {

        public GuildLogData()
            : base(false)
        {
        }


        /// <summary>
        /// 时间
        /// </summary>
        [ProtoMember(1)]
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 对方Uid
        /// </summary>
        [ProtoMember(2)]
        public int UserId { get; set; }

        /// <summary>
        /// 对方name
        /// </summary>
        [ProtoMember(3)]
        public string UserName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [ProtoMember(4)]
        public string Content { get; set; }
    }
}