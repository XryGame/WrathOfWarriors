using System;
using ProtoBuf;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{
    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class UserCenterUser : ShareEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string PassportID { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public int ServerId { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public DateTime AccessTime { get; set; }

        [ProtoMember(5)]
        [EntityField]
        public int LoginNum { get; set; }



    }
}
