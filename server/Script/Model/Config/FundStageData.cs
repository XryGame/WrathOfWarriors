
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 背包格子数据
    /// </summary>
    [Serializable, ProtoContract]
    public class FundStageData : EntityChangeEvent
    {

        public FundStageData()
            : base(false)
        {
        }

        /// <summary>
        /// ID
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }


        /// <summary>
        /// 状态 
        /// </summary>
        [ProtoMember(2)]
        public FundStatus Status { get; set; }



    }
}