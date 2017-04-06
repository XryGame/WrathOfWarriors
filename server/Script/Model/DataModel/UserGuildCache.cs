
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;
using System.Collections.Generic;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户公会信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserGuildCache : BaseEntity
    {
        
        public UserGuildCache()
            : base(AccessLevel.ReadWrite)
        {
            //ResetCache();
        }

        private int _UserID;
        [ProtoMember(1)]
        [EntityField("UserID", IsKey = true)]
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                SetChange("UserID", value);
            }
        }

        /// <summary>
        /// 是否签到
        /// </summary>
        private bool _IsSignIn;
        [ProtoMember(2)]
        [EntityField("IsSignIn")]
        public bool IsSignIn
        {
            get
            {
                return _IsSignIn;
            }
            set
            {
                SetChange("IsSignIn", value);
            }
        }

        /// <summary>
        /// 所属公会ID
        /// </summary>
        private string _GuildID;
        [ProtoMember(3)]
        [EntityField("GuildID")]
        public string GuildID
        {
            get
            {
                return _GuildID;
            }
            set
            {
                SetChange("GuildID", value);
            }
        }

        /// <summary>
        /// 公会币
        /// </summary>
        private int _GuildCoin;
        [ProtoMember(4)]
        [EntityField("GuildCoin")]
        public int GuildCoin
        {
            get
            {
                return _GuildCoin;
            }
            set
            {
                SetChange("GuildCoin", value);
            }
        }


        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "IsSignIn": return IsSignIn;
                    case "GuildID": return GuildID;
                    case "GuildCoin": return GuildCoin;
                    default: throw new ArgumentException(string.Format("UserGuildCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserID":
                        _UserID = value.ToInt();
                        break;
                    case "IsSignIn":
                        _IsSignIn = value.ToBool();
                        break;
                    case "GuildID":
                        _GuildID = value.ToNotNullString();
                        break;
                    case "GuildCoin":
                        _GuildCoin = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserGuildCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public void ResetCache()
        {
            GuildID = string.Empty;
            IsSignIn = false;
        }



    }
}