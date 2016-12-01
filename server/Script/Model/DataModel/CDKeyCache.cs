using System;
using ProtoBuf;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{
    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class CDKeyCache : ShareEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public string CDKey { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public DateTime UsedTime { get; set; }



    }
}
