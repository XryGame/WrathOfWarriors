
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
        /// 发信人Vip 
        /// </summary>
        [ProtoMember(3)]
        public short FromUserVip
        {
            get;
            set;
        }

        /// <summary>
        /// 发信人名称
        /// </summary>
        [ProtoMember(4)]
        public string FromUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 收信人Vip 
        /// </summary>
        [ProtoMember(6)]
        public short ToUserVip
        {
            get;
            set;
        }

        /// <summary>
        /// 收信人名称
        /// </summary>
        [ProtoMember(7)]
        public string ToUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 聊天类型
        /// </summary>
        [ProtoMember(9)]
        public ChatType ChatType
        {
            get;
            set;
        }

        /// <summary>
        /// 班级ID
        /// </summary>
        [ProtoMember(11)]
        public int ClassID { get; set; }

        /// <summary>
        /// 形象ID
        /// </summary>
        [ProtoMember(12)]
        public int LooksId { get; set; }

        /// <summary>
        /// 是否是拉选票
        /// </summary>
        [ProtoMember(13)]
        public bool IsCanvass { get; set; }
    }
}