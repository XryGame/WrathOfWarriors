
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Config;
using System.Collections.Generic;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Giftbag : ShareEntity
    {

        public Config_Giftbag()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// id
        /// </summary>
        private int _ID;
        [EntityField("ID", IsKey = true)]
        public int ID
        {
            get
            {
                return _ID;
            }
            private set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 礼包id
        /// </summary>
        private int _ItemID;
        [EntityField("ItemID")]
        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            private set
            {
                SetChange("ItemID", value);
            }
        }

        /// <summary>
        /// 获得物品A
        /// </summary>
        private int _AwardItemA;
        [EntityField("AwardItemA")]
        public int AwardItemA
        {
            get
            {
                return _AwardItemA;
            }
            private set
            {
                SetChange("AwardItemA", value);
            }
        }

        /// <summary>
        /// 获得物品A数量
        /// </summary>
        private int _AwardItemANum;
        [EntityField("AwardItemANum")]
        public int AwardItemANum
        {
            get
            {
                return _AwardItemANum;
            }
            private set
            {
                SetChange("AwardItemANum", value);
            }
        }

        /// <summary>
        ///B
        /// </summary>
        private int _AwardItemB;
        [EntityField("AwardItemB")]
        public int AwardItemB
        {
            get
            {
                return _AwardItemB;
            }
            private set
            {
                SetChange("AwardItemB", value);
            }
        }

        /// <summary>
        /// B数量
        /// </summary>
        private int _AwardItemBNum;
        [EntityField("AwardItemBNum")]
        public int AwardItemBNum
        {
            get
            {
                return _AwardItemBNum;
            }
            private set
            {
                SetChange("AwardItemBNum", value);
            }
        }

        /// <summary>
        ///C
        /// </summary>
        private int _AwardItemC;
        [EntityField("AwardItemC")]
        public int AwardItemC
        {
            get
            {
                return _AwardItemC;
            }
            private set
            {
                SetChange("AwardItemC", value);
            }
        }

        /// <summary>
        /// C数量
        /// </summary>
        private int _AwardItemCNum;
        [EntityField("AwardItemCNum")]
        public int AwardItemCNum
        {
            get
            {
                return _AwardItemCNum;
            }
            private set
            {
                SetChange("AwardItemCNum", value);
            }
        }

        /// <summary>
        ///D
        /// </summary>
        private int _AwardItemD;
        [EntityField("AwardItemD")]
        public int AwardItemD
        {
            get
            {
                return _AwardItemD;
            }
            private set
            {
                SetChange("AwardItemD", value);
            }
        }

        /// <summary>
        /// D数量
        /// </summary>
        private int _AwardItemDNum;
        [EntityField("AwardItemDNum")]
        public int AwardItemDNum
        {
            get
            {
                return _AwardItemDNum;
            }
            private set
            {
                SetChange("AwardItemDNum", value);
            }
        }

        /// <summary>
        ///E
        /// </summary>
        private int _AwardItemE;
        [EntityField("AwardItemE")]
        public int AwardItemE
        {
            get
            {
                return _AwardItemE;
            }
            private set
            {
                SetChange("AwardItemE", value);
            }
        }

        /// <summary>
        /// E数量
        /// </summary>
        private int _AwardItemENum;
        [EntityField("AwardItemENum")]
        public int AwardItemENum
        {
            get
            {
                return _AwardItemENum;
            }
            private set
            {
                SetChange("AwardItemENum", value);
            }
        }

        /// <summary>
        ///F
        /// </summary>
        private int _AwardItemF;
        [EntityField("AwardItemF")]
        public int AwardItemF
        {
            get
            {
                return _AwardItemF;
            }
            private set
            {
                SetChange("AwardItemF", value);
            }
        }

        /// <summary>
        ///F数量
        /// </summary>
        private int _AwardItemFNum;
        [EntityField("AwardItemFNum")]
        public int AwardItemFNum
        {
            get
            {
                return _AwardItemFNum;
            }
            private set
            {
                SetChange("AwardItemFNum", value);
            }
        }

        /// <summary>
        ///G
        /// </summary>
        private int _AwardItemG;
        [EntityField("AwardItemG")]
        public int AwardItemG
        {
            get
            {
                return _AwardItemG;
            }
            private set
            {
                SetChange("AwardItemG", value);
            }
        }

        /// <summary>
        ///G数量
        /// </summary>
        private int _AwardItemGNum;
        [EntityField("AwardItemGNum")]
        public int AwardItemGNum
        {
            get
            {
                return _AwardItemGNum;
            }
            private set
            {
                SetChange("AwardItemGNum", value);
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
                    case "AwardItemA": return AwardItemA;
                    case "AwardItemANum": return AwardItemANum;
                    case "AwardItemB": return AwardItemB;
                    case "AwardItemBNum": return AwardItemBNum;
                    case "AwardItemC": return AwardItemC;
                    case "AwardItemCNum": return AwardItemCNum;
                    case "AwardItemD": return AwardItemD;
                    case "AwardItemDNum": return AwardItemDNum;
                    case "AwardItemE": return AwardItemE;
                    case "AwardItemENum": return AwardItemENum;
                    case "AwardItemF": return AwardItemF;
                    case "AwardItemFNum": return AwardItemFNum;
                    case "AwardItemG": return AwardItemG;
                    case "AwardItemGNum": return AwardItemGNum;
                    default: throw new ArgumentException(string.Format("Config_Role index[{0}] isn't exist.", index));
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
                    case "AwardItemA":
                        _AwardItemA = value.ToInt();
                        break;
                    case "AwardItemANum":
                        _AwardItemANum = value.ToInt(); 
                        break;
                    case "AwardItemB":
                        _AwardItemB = value.ToInt();
                        break;
                    case "AwardItemBNum":
                        _AwardItemBNum = value.ToInt();
                        break;
                    case "AwardItemC":
                        _AwardItemC = value.ToInt();
                        break;
                    case "AwardItemCNum":
                        _AwardItemCNum = value.ToInt();
                        break;
                    case "AwardItemD":
                        _AwardItemD = value.ToInt();
                        break;
                    case "AwardItemDNum":
                        _AwardItemDNum = value.ToInt();
                        break;
                    case "AwardItemE":
                        _AwardItemE = value.ToInt();
                        break;
                    case "AwardItemENum":
                        _AwardItemENum = value.ToInt();
                        break;
                    case "AwardItemF":
                        _AwardItemF = value.ToInt();
                        break;
                    case "AwardItemFNum":
                        _AwardItemFNum = value.ToInt();
                        break;
                    case "AwardItemG":
                        _AwardItemG = value.ToInt();
                        break;
                    case "AwardItemGNum":
                        _AwardItemGNum = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Rolo index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
        protected override int GetIdentityId()
        {
            //allow modify return value
            return DefIdentityId;
        }

        public List<ItemData> GetRewardsItem()
        {
            List<ItemData> list = new List<ItemData>();
            if (AwardItemA > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemA,
                    Num = AwardItemANum
                };
                list.Add(data);
            }
            if (AwardItemB > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemB,
                    Num = AwardItemBNum
                };
                list.Add(data);
            }
            if (AwardItemC > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemC,
                    Num = AwardItemCNum
                };
                list.Add(data);
            }
            if (AwardItemD > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemD,
                    Num = AwardItemDNum
                };
                list.Add(data);
            }
            if (AwardItemE > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemE,
                    Num = AwardItemENum
                };
                list.Add(data);
            }
            if (AwardItemF > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemF,
                    Num = AwardItemFNum
                };
                list.Add(data);
            }
            if (AwardItemG > 0)
            {
                ItemData data = new ItemData()
                {
                    ID = AwardItemG,
                    Num = AwardItemGNum
                };
                list.Add(data);
            }

            return list;
        }
	}
}