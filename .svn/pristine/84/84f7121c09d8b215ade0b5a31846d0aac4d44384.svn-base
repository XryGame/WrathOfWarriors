
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 好友数据
    /// </summary>
    [Serializable, ProtoContract]
    public class FriendData : EntityChangeEvent
    {

        public FriendData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 用户Id
        /// </summary>
        [ProtoMember(1)]
        public int UserId { get; set; }
        /// <summary>
        /// 是否已赠送
        /// </summary>
        [ProtoMember(2)]
        public bool IsGiveAway { get; set; }

        /// <summary>
        /// 是否被赠送
        /// </summary>
        [ProtoMember(3)]
        public bool IsByGiveAway { get; set; }

        /// <summary>
        /// 是否领取赠送
        /// </summary>
        [ProtoMember(4)]
        public bool IsReceiveGiveAway { get; set; }

    }
}