
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
    /// 用户赠送物品
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserTransferItemCache : BaseEntity
    {
        
        public UserTransferItemCache()
            : base(AccessLevel.ReadWrite)
        {
            ReceiveList = new CacheList<ReceiveTransferItemData>();
            SendList = new CacheList<SendTransferItemData>();
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
        /// 接收列表
        /// </summary>
        private CacheList<ReceiveTransferItemData> _ReceiveList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<ReceiveTransferItemData> ReceiveList
        {
            get
            {
                return _ReceiveList;
            }
            set
            {
                SetChange("ReceiveList", value);
            }
        }

        /// <summary>
        /// 发送列表
        /// </summary>
        private CacheList<SendTransferItemData> _SendList;
        [ProtoMember(3)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<SendTransferItemData> SendList
        {
            get
            {
                return _SendList;
            }
            set
            {
                SetChange("SendList", value);
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
                    case "ReceiveList": return ReceiveList;
                    case "SendList": return SendList;
                    default: throw new ArgumentException(string.Format("UserTransferItemCache index[{0}] isn't exist.", index));
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
                    case "ReceiveList":
                        _ReceiveList = ConvertCustomField<CacheList<ReceiveTransferItemData>>(value, index);
                        break;
                    case "SendList":
                        _SendList = ConvertCustomField<CacheList<SendTransferItemData>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserTransferItemCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public ReceiveTransferItemData FindReceive(string id)
        {
            return ReceiveList.Find(t => (t.ID == id));
        }

        public SendTransferItemData FindSend(string id)
        {
            return SendList.Find(t => (t.ID == id));
        }

        public void AddReceive(ReceiveTransferItemData data)
        {
            ReceiveList.Add(data);
        }


        
        public void ResetCache()
        {
            ReceiveList.Clear();
            SendList.Clear();
        }
    }
}