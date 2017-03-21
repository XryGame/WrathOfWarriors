using System;
using ProtoBuf;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model
{
    [ProtoContract]
    [EntityTable(CacheType.None, "")]
    public class ChatUser : MemoryEntity
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string UserName { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public int Profession { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public int VipLv { get; set; }


        [ProtoMember(5)]
        [EntityField]
        public int ServerID { get; set; }

        [ProtoMember(6)]
        [EntityField]
        public int GuildID { get; set; }



    }
}
