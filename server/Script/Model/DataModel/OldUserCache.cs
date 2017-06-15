using System;
using ProtoBuf;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model
{
    [ProtoContract]
    [EntityTable(CacheType.None, DbConfig.Data)]
    public class OldUserCache : MemoryEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public string OpenID { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string NickName { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public string AvatarUrl { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public DateTime CreateDate { get; set; }



    }
}
