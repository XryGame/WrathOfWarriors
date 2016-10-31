using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{
    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class TUser : ShareEntity, IUser
    {
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public string PassportID { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public string Password { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public int ServerId { get; set; }

        [ProtoMember(5)]
        [EntityField]
        public DateTime AccessTime { get; set; }

        [ProtoMember(6)]
        [EntityField]
        public string Token { get; set; }

        public bool IsLock { get; private set; }
        public bool IsOnlining { get; private set; }
        public int ChatVesion { get; set; }

        public int GetUserId()
        {
            return UserId;
        }

        public string GetPassportId()
        {
            return PassportID;
        }

        public void RefleshOnlineDate()
        {
            AccessTime = DateTime.Now;
        }

    }
}
