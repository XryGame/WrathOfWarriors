
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 公会角色数据
    /// </summary>
    [Serializable, ProtoContract]
    public class GuildCharacter : EntityChangeEvent
    {

        public GuildCharacter()
            : base(false)
        {
        }

        /// <summary>
        /// UserID
        /// </summary>
        [ProtoMember(1)]
        public int UserID { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [ProtoMember(2)]
        public GuildJobTitle JobTitle { get; set; }

        /// <summary>
        /// 活跃度
        /// </summary>
        [ProtoMember(3)]
        public int Liveness { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [ProtoMember(4)]
        public DateTime Date { get; set; }
    }
}