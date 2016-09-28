using System;
using ProtoBuf;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{
    /// <summary>
    /// 玩家昵称缓存
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, DbConfig.Data, StorageType = StorageType.ReadWriteDB, IsExpired = false)]
    public class NickNameCache : ShareEntity
    {
        public NickNameCache()
            : base(false)
        {
        }

        public NickNameCache(int userId)
            : this()
        {
            UserId = userId;
        }

        /// <summary>
        /// 
        /// </summary>        
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        [ProtoMember(2)]
        [EntityField]
        public string NickName { get; set; }

    }
}
