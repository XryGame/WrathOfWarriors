
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
        private int _ID;
        /// <summary>
        /// id
        /// </summary>
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
        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        [EntityField("Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                SetChange("Name", value);
            }
        }
        private ItemType _Type;
        /// <summary>
        /// 道具类型
        /// </summary>
        [EntityField("Type")]
        public ItemType Type
        {
            get
            {
                return _Type;
            }
            private set
            {
                SetChange("Type", value);
            }
        }
        private int _Map;
        /// <summary>
        /// 场景
        /// </summary>
        [EntityField("Map")]
        public int Map
        {
            get
            {
                return _Map;
            }
            private set
            {
                SetChange("Map", value);
            }
        }
        private string _Resource;
        /// <summary>
        /// 资源
        /// </summary>
        [EntityField("Resource")]
        public string Resource
        {
            get
            {
                return _Resource;
            }
            private set
            {
                SetChange("Resource", value);
            }
        }
        private int _GainProbability;
        /// <summary>
        /// 获得概率
        /// </summary>
        [EntityField("GainProbability")]
        public int GainProbability
        {
            get
            {
                return _GainProbability;
            }
            private set
            {
                SetChange("GainProbability", value);
            }
        }

        private string _Des;
        /// <summary>
        /// 描述
        /// </summary>
        [EntityField("Des")]
        public string Des
        {
            get
            {
                return _Des;
            }
            private set
            {
                SetChange("Des", value);
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
                    case "Name": return Name;
                    case "Type": return Type;
                    case "Map": return Map;
                    case "Resource": return Resource;
                    case "GainProbability": return GainProbability;
                    case "Des": return Des;
                    default: throw new ArgumentException(string.Format("Config_Item index[{0}] isn't exist.", index));
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
                    case "Name":
                        _Name = value.ToNotNullString(); 
                        break;
                    case "Type":
                        _Type = value.ToEnum<ItemType>();
                        break;
                    case "Map":
                        _Map = value.ToInt(); 
                        break; 
                    case "Resource":
                        _Resource = value.ToNotNullString();
                        break;
                    case "GainProbability":
                        _GainProbability = value.ToInt();
                        break;
                    case "Des":
                        _Des = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Item index[{0}] isn't exist.", index));
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
	}
}