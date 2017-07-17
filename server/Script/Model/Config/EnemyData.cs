
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 仇人数据
    /// </summary>
    [Serializable, ProtoContract]
    public class EnemyData : EntityChangeEvent
    {

        public EnemyData()
            : base(false)
        {
            RobGold = "0";
        }
        
        /// <summary>
        /// 用户Id
        /// </summary>
        [ProtoMember(1)]
        public int UserId { get; set; }
        
        /// <summary>
        /// 是否领取赠送
        /// </summary>
        [ProtoMember(2)]
        public string RobGold { get; set; }

    }
}