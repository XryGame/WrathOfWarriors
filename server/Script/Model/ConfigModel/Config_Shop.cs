
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
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Shop : ShareEntity
    {

        
        public Config_Shop()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 唯一id
        /// </summary>
        private int _ID;
        [EntityField("ID", IsKey = true)]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 物品id
        /// </summary>
        private int _ItemID;
        [EntityField("ItemID")]
        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                SetChange("ItemID", value);
            }
        }


        /// <summary>
        /// 购买类型
        /// </summary>
        private ShopType _LimitType;
        [EntityField("LimitType")]
        public ShopType LimitType
        {
            get
            {
                return _LimitType;
            }
            set
            {
                SetChange("LimitType", value);
            }
        }

        /// <summary>
        /// 折扣
        /// </summary>
        private int _Discount;
        [EntityField("Discount")]
        public int Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                SetChange("Discount", value);
            }
        }

        /// <summary>
        /// 出售货币类型
        /// </summary>
        private CoinType _CurrencyType;
        [EntityField("CurrencyType")]
        public CoinType CurrencyType
        {
            get
            {
                return _CurrencyType;
            }
            set
            {
                SetChange("CurrencyType", value);
            }
        }

        /// <summary>
        /// 打折前价格
        /// </summary>
        private int _Price;
        [EntityField("Price")]
        public int Price
        {
            get
            {
                return _Price;
            }
            set
            {
                SetChange("Price", value);
            }
        }
        
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "ID": return ID;
                    case "ItemID": return ItemID;
                    case "LimitType": return LimitType;
                    case "Discount": return Discount;
                    case "CurrencyType": return CurrencyType;
                    case "Price": return Price;
                    default: throw new ArgumentException(string.Format("Config_Shop index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "ID":
                        _ID = value.ToInt(); 
                        break; 
                    case "ItemID":
                        _ItemID = value.ToInt(); 
                        break;
                    case "LimitType":
                        _LimitType = value.ToEnum<ShopType>();
                        break;
                    case "Discount":
                        _Discount = value.ToInt();
                        break;
                    case "CurrencyType":
                        _CurrencyType = value.ToEnum<CoinType>();
                        break;
                    case "Price":
                        _Price = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Shop index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}