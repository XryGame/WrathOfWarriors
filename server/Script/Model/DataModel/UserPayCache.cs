
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户充值信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserPayCache : BaseEntity
    {

        public UserPayCache()
            : base(AccessLevel.ReadWrite)
        {

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
        /// 是否领取首充
        /// </summary>
        private bool _IsReceiveFirstPay;
        [ProtoMember(3)]
        [EntityField("IsReceiveFirstPay")]
        public bool IsReceiveFirstPay
        {
            get
            {
                return _IsReceiveFirstPay;
            }
            set
            {
                SetChange("IsReceiveFirstPay", value);
            }
        }

        /// <summary>
        /// 周卡剩余天数
        /// </summary>
        private int _WeekCardDays;
        [ProtoMember(4)]
        [EntityField("WeekCardDays")]
        public int WeekCardDays
        {
            get
            {
                return _WeekCardDays;
            }
            set
            {
                SetChange("WeekCardDays", value);
            }
        }

        /// <summary>
        /// 月卡剩余天数
        /// </summary>
        private int _MonthCardDays;
        [ProtoMember(5)]
        [EntityField("MonthCardDays")]
        public int MonthCardDays
        {
            get
            {
                return _MonthCardDays;
            }
            set
            {
                SetChange("MonthCardDays", value);
            }
        }
        protected override int GetIdentityId()
        {
            //allow modify return value
            return DefIdentityId;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "IsReceiveFirstPay": return IsReceiveFirstPay;
                    case "WeekCardDays": return WeekCardDays;
                    case "MonthCardDays": return MonthCardDays;
                    default: throw new ArgumentException(string.Format("UserPayCache index[{0}] isn't exist.", index));
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
                    case "IsReceiveFirstPay":
                        _IsReceiveFirstPay = value.ToBool();
                        break;
                    case "WeekCardDays":
                        _WeekCardDays = value.ToInt();
                        break;
                    case "MonthCardDays":
                        _MonthCardDays = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserPayCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

    }
}