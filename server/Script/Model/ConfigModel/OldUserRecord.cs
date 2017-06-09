
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(CacheType.Entity, DbConfig.Config, StorageType = StorageType.ReadWriteDB, IsExpired = false)]
    public class OldUserRecord : ShareEntity
    {

        
        public OldUserRecord()
            : base(false)
        {
        }

        #region auto-generated Property

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

        #endregion

    }
}