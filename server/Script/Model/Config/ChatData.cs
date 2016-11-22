
using System;
using ProtoBuf;
using ZyGames.Framework.Game.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{
    /// <summary>
    /// 聊天
    /// </summary>
    [Serializable, ProtoContract]
    public class ChatData : ChatMessage
    {
        /// <summary>
        /// 子类型
        /// </summary>
        [ProtoMember(1)]
        public ChatChildType ChildType
        {
            get;
            set;
        }

        /// <summary>
        /// 发信人Vip 
        /// </summary>
        [ProtoMember(2)]
        public short FromUserVip
        {
            get;
            set;
        }

        /// <summary>
        /// 发信人名称
        /// </summary>
        [ProtoMember(3)]
        public string FromUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 收信人Vip 
        /// </summary>
        [ProtoMember(4)]
        public short ToUserVip
        {
            get;
            set;
        }

        /// <summary>
        /// 收信人名称
        /// </summary>
        [ProtoMember(5)]
        public string ToUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 聊天类型
        /// </summary>
        [ProtoMember(6)]
        public ChatType ChatType
        {
            get;
            set;
        }

        /// <summary>
        /// 班级ID
        /// </summary>
        [ProtoMember(7)]
        public int ClassID { get; set; }

        /// <summary>
        /// 形象ID
        /// </summary>
        [ProtoMember(8)]
        public int LooksId { get; set; }

    }
}