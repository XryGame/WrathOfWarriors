using System;
using ProtoBuf;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{
    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class UserCenterPassport : ShareEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public string PassportID { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string Password { get; set; }
        
        [ProtoMember(3)]
        [EntityField]
        public DateTime CreateTime { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public string RetailId { get; set; }

        [ProtoMember(5)]
        [EntityField]
        public string OpenId { get; set; }


    }
}
