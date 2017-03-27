
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户邮箱信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserMailBoxCache : BaseEntity
    {
        public UserMailBoxCache()
            : base(AccessLevel.ReadWrite)
        {
            MailList = new CacheList<MailData>();
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
        /// 背包物品列表
        /// </summary>
        private CacheList<MailData> _MailList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<MailData> MailList
        {
            get
            {
                return _MailList;
            }
            set
            {
                SetChange("MailList", value);
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
                    case "MailList": return MailList;
                    default: throw new ArgumentException(string.Format("UserMailBoxCache index[{0}] isn't exist.", index));
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
                    case "MailList":
                        _MailList = ConvertCustomField<CacheList<MailData>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserMailBoxCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetCache()
        {
            MailList.Clear();
        }

        public MailData findMail(string id)
        {
            return MailList.Find(t => (t.ID == id));
        }


    }
}