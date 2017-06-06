
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Item : ShareEntity
    {

        public Config_Item()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 物品id
        /// </summary>
        private int _ItemID;
        [EntityField("ItemID", IsKey = true)]
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
        /// 物品名称
        /// </summary>
        private string _ItemName;
        [EntityField("ItemName")]
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            private set
            {
                SetChange("ItemName", value);
            }
        }

        /// <summary>
        /// 物品类型
        /// </summary>
        private ItemType _ItemType;
        [EntityField("ItemType")]
        public ItemType ItemType
        {
            get
            {
                return _ItemType;
            }
            private set
            {
                SetChange("ItemType", value);
            }
        }

        /// <summary>
        /// 物品子类型
        /// </summary>
        private int _Species;
        [EntityField("Species")]
        public int Species
        {
            get
            {
                return _Species;
            }
            private set
            {
                SetChange("Species", value);
            }
        }

        /// <summary>
        /// 物品初始品质
        /// </summary>
        private ItemQuality _Quality;
        [EntityField("Quality")]
        public ItemQuality Quality
        {
            get
            {
                return _Quality;
            }
            private set
            {
                SetChange("Quality", value);
            }
        }

        /// <summary>
        /// 使用获得资源类型
        /// </summary>
        private ResourceType _ResourceType;
        [EntityField("ResourceType")]
        public ResourceType ResourceType
        {
            get
            {
                return _ResourceType;
            }
            private set
            {
                SetChange("ResourceType", value);
            }
        }

        /// <summary>
        /// 获得资源数量
        /// </summary>
        private string _ResourceNum;
        [EntityField("ResourceNum")]
        public string ResourceNum
        {
            get
            {
                return _ResourceNum;
            }
            private set
            {
                SetChange("ResourceNum", value);
            }
        }

        /// <summary>
        /// 道具等级
        /// </summary>
        private int _ItemGrade;
        [EntityField("ItemGrade")]
        public int ItemGrade
        {
            get
            {
                return _ItemGrade;
            }
            private set
            {
                SetChange("ItemGrade", value);
            }
        }

        /// <summary>
        /// 出售获得金钱
        /// </summary>
        private string _SellGold;
        [EntityField("SellGold")]
        public string SellGold
        {
            get
            {
                return _SellGold;
            }
            private set
            {
                SetChange("SellGold", value);
            }
        }

        /// <summary>
        /// 初始生命
        /// </summary>
        private long _hp;
        [EntityField("hp")]
        public long hp
        {
            get
            {
                return _hp;
            }
            private set
            {
                SetChange("hp", value);
            }
        }

        /// <summary>
        /// 初始攻击
        /// </summary>
        private long _attack;
        [EntityField("attack")]
        public long attack
        {
            get
            {
                return _attack;
            }
            private set
            {
                SetChange("attack", value);
            }
        }

        /// <summary>
        /// 初始防御
        /// </summary>
        private long _defense;
        [EntityField("defense")]
        public long defense
        {
            get
            {
                return _defense;
            }
            private set
            {
                SetChange("defense", value);
            }
        }

        /// <summary>
        /// 初始闪避
        /// </summary>
        private long _dodge;
        [EntityField("dodge")]
        public long dodge
        {
            get
            {
                return _dodge;
            }
            private set
            {
                SetChange("dodge", value);
            }
        }

        /// <summary>
        /// 初始暴击
        /// </summary>
        private long _crit;
        [EntityField("crit")]
        public long crit
        {
            get
            {
                return _crit;
            }
            private set
            {
                SetChange("crit", value);
            }
        }

        /// <summary>
        /// 初始命中
        /// </summary>
        private long _hit;
        [EntityField("hit")]
        public long hit
        {
            get
            {
                return _hit;
            }
            private set
            {
                SetChange("hit", value);
            }
        }

        /// <summary>
        /// 初始韧性
        /// </summary>
        private long _tenacity;
        [EntityField("tenacity")]
        public long tenacity
        {
            get
            {
                return _tenacity;
            }
            private set
            {
                SetChange("tenacity", value);
            }
        }
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "ItemID": return ItemID;
                    case "ItemName": return ItemName;
                    case "ItemType": return ItemType;
                    case "Species": return Species;
                    case "Quality": return Quality;
                    case "ResourceType": return ResourceType;
                    case "ResourceNum": return ResourceNum;
                    case "ItemGrade": return ItemGrade;
                    case "SellGold": return SellGold;
                    case "hp": return hp;
                    case "attack": return attack;
                    case "defense": return defense;
                    case "dodge": return dodge;
                    case "crit": return crit;
                    case "hit": return hit;
                    case "tenacity": return tenacity;
                    default: throw new ArgumentException(string.Format("Config_Item index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "ItemID":
                        _ItemID = value.ToInt(); 
                        break; 
                    case "ItemName":
                        _ItemName = value.ToNotNullString(); 
                        break;
                    case "ItemType":
                        _ItemType = value.ToEnum<ItemType>();
                        break;
                    case "Species":
                        _Species = value.ToInt(); 
                        break; 
                    case "Quality":
                        _Quality = value.ToEnum<ItemQuality>();
                        break; 
                    case "ResourceType":
                        _ResourceType = value.ToEnum<ResourceType>();
                        break;
                    case "ResourceNum":
                        _ResourceNum = value.ToNotNullString("0");
                        break;
                    case "ItemGrade":
                        _ItemGrade = value.ToInt();
                        break;
                    case "SellGold":
                        _SellGold = value.ToNotNullString("0");
                        break;
                    case "hp":
                        _hp = value.ToLong();
                        break;
                    case "attack":
                        _attack = value.ToLong();
                        break;
                    case "defense":
                        _defense = value.ToLong();
                        break;
                    case "dodge":
                        _dodge = value.ToLong();
                        break;
                    case "crit":
                        _crit = value.ToLong();
                        break;
                    case "hit":
                        _hit = value.ToLong();
                        break;
                    case "tenacity":
                        _tenacity = value.ToLong();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Item index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
        //protected override int GetIdentityId()
        //{
        //    //allow modify return value
        //    return DefIdentityId;
        //}
	}
}