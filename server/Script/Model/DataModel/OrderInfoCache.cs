
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 订单信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Entity, DbConfig.Data)]
    public class OrderInfoCache : ShareEntity
    {

        public OrderInfoCache()
            : base(AccessLevel.ReadWrite)
        {

        }
        
        /// <summary>
        /// 订单ID
        /// </summary>
        private string _OrderId;
        [ProtoMember(1)]
        [EntityField("OrderId", IsKey = true)]
        public string OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                SetChange("OrderId", value);
            }
        }


        /// <summary>
        /// 用户ID
        /// </summary>
        private int _UserId;
        [ProtoMember(2)]
        [EntityField("UserId")]
        public int UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                SetChange("UserId", value);
            }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        private string _NickName;
        [ProtoMember(3)]
        [EntityField("NickName")]
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                SetChange("NickName", value);
            }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        private string _MerchandiseName;
        [ProtoMember(4)]
        [EntityField("MerchandiseName")]
        public string MerchandiseName
        {
            get
            {
                return _MerchandiseName;
            }
            set
            {
                SetChange("MerchandiseName", value);
            }
        }

        /// <summary>
        ///  支付Id
        /// </summary>
        private int _PayId;
        [ProtoMember(5)]
        [EntityField("PayId")]
        public int PayId
        {
            get
            {
                return _PayId;
            }
            set
            {
                SetChange("PayId", value);
            }
        }

        /// <summary>
        /// 支付金额
        /// </summary>
        private float _Amount;
        [ProtoMember(6)]
        [EntityField("Amount")]
        public float Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                SetChange("Amount", value);
            }
        }

        /// <summary>
        /// 账号
        /// </summary>
        private string _PassportID;
        [ProtoMember(7)]
        [EntityField("PassportID")]
        public string PassportID
        {
            get
            {
                return _PassportID;
            }
            set
            {
                SetChange("PassportID", value);
            }
        }

        /// <summary>
        /// 分服ID
        /// </summary>
        private int _ServerID;
        [ProtoMember(8)]
        [EntityField("ServerID")]
        public int ServerID
        {
            get
            {
                return _ServerID;
            }
            set
            {
                SetChange("ServerID", value);
            }
        }

        /// <summary>
        /// 游戏币
        /// </summary>
        private int _GameCoins;
        [ProtoMember(9)]
        [EntityField("GameCoins")]
        public int GameCoins
        {
            get
            {
                return _GameCoins;
            }
            set
            {
                SetChange("GameCoins", value);
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _CreateDate;
        [ProtoMember(10)]
        [EntityField("CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetChange("CreateDate", value);
            }
        }

        /// <summary>
        /// 渠道商ID
        /// </summary>
        private string _RetailID;
        [ProtoMember(11)]
        [EntityField("RetailID")]
        public string RetailID
        {
            get
            {
                return _RetailID;
            }
            set
            {
                SetChange("RetailID", value);
            }
        }

        /// <summary>
        ///  平台支付Id
        /// </summary>
        private int _RcId;
        [ProtoMember(12)]
        [EntityField("RcId")]
        public int RcId
        {
            get
            {
                return _RcId;
            }
            set
            {
                SetChange("RcId", value);
            }
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "OrderId": return OrderId;
                    case "UserId": return UserId;
                    case "NickName": return NickName;
                    case "MerchandiseName": return MerchandiseName;
                    case "PayId": return PayId;
                    case "Amount": return Amount;
                    case "PassportID": return PassportID;
                    case "ServerID": return ServerID;
                    case "GameCoins": return GameCoins;
                    case "CreateDate": return CreateDate;
                    case "RetailID": return RetailID;
                    case "RcId": return RcId;
                    default: throw new ArgumentException(string.Format("OrderInfoCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "OrderId":
                        _OrderId = value.ToNotNullString();
                        break;
                    case "UserId":
                        _UserId = value.ToInt();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "MerchandiseName":
                        _MerchandiseName = value.ToNotNullString();
                        break;
                    case "PayId":
                        _PayId = value.ToInt();
                        break;
                    case "Amount":
                        _Amount = value.ToFloat();
                        break;
                    case "PassportID":
                        _PassportID = value.ToNotNullString();
                        break;
                    case "ServerID":
                        _ServerID = value.ToInt();
                        break;
                    case "GameCoins":
                        _GameCoins = value.ToInt();
                        break;
                    case "CreateDate":
                        _CreateDate = value.ToDateTime();
                        break;
                    case "RetailID":
                        _RetailID = value.ToNotNullString();
                        break;
                    case "RcId":
                        _RcId = value.ToInt();
                        break;

                    default: throw new ArgumentException(string.Format("OrderInfoCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

    }
}