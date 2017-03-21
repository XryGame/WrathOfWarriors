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
        public int UserID { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string NickName { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public string OpenID { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public int ServerID { get; set; }

        [ProtoMember(5)]
        [EntityField]
        public DateTime AccessTime { get; set; }

        [ProtoMember(6)]
        [EntityField]
        public int LoginNum { get; set; }

        [ProtoMember(7)]
        [EntityField]
        public string RetailID { get; set; }



    }
}
