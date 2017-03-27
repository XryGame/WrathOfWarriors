
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 好友申请数据
    /// </summary>
    [Serializable, ProtoContract]
    public class FriendApplyData : EntityChangeEvent
    {

        public FriendApplyData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 用户Id
        /// </summary>
        [ProtoMember(1)]
        public int UserId { get; set; }
  
        /// <summary>
        /// 时间
        /// </summary>
        [ProtoMember(2)]
        public DateTime ApplyDate { get; set; }

    }
}