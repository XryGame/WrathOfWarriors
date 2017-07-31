
using System;
using ProtoBuf;
using ZyGames.Framework.Model;
using ZyGames.Framework.Event;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.Script.Model.Config
{

    /// <summary>
    /// 背包格子数据
    /// </summary>
    [Serializable, ProtoContract]
    public class FundData : EntityChangeEvent
    {

        public FundData()
            : base(false)
        {
            List = new CacheList<FundStageData>();
        }


        /// <summary>
        /// 是否激活
        /// </summary>
        [ProtoMember(1)]
        public bool IsActivate { get; set; }


        /// <summary>
        /// 数据
        /// </summary>
        [ProtoMember(2)]

        private CacheList<FundStageData> _List;
        public CacheList<FundStageData> List
        {
            get
            {
                return _List;
            }
            set
            {
                _List = value;
            }
        }

    }
}