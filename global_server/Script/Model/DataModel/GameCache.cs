using System;
using ProtoBuf;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{
    /// <summary>
    /// 维持一份共享数据用于游戏
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, "WOWGlobalData", StorageType = StorageType.ReadWriteDB, IsExpired = false)]
    public class GameCache : ShareEntity
    {
        public GameCache()
            : base(false)
        {
        }

        /// <summary>
        /// 
        /// </summary>        
        [ProtoMember(1)]
        [EntityField(true)]
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public string Value { get; set; }

    }
}
