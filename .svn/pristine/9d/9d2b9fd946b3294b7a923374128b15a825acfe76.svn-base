
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 竞选用户数据
    /// </summary>
    [Serializable, ProtoContract]
    public class CampaignUserData : EntityChangeEvent
    {

        public CampaignUserData()
            : base(false)
        {
        }
        
        /// <summary>
        /// 选人Id
        /// </summary>
        [ProtoMember(1)]
        public int UserId { get; set; }
        /// <summary>
        /// 选人昵称
        /// </summary>
        [ProtoMember(2)]
        public string NickName { get; set; }
        /// <summary>
        /// 选人所在班级
        /// </summary>
        [ProtoMember(3)]
        public int ClassId { get; set; }
        /// <summary>
        /// 选票计数
        /// </summary>
        [ProtoMember(4)]
        public int VoteCount { get; set; }
        /// <summary>
        /// LooksId
        /// </summary>
        [ProtoMember(5)]
        public int LooksId { get; set; }

    }
}