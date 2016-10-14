
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
    public class Config_Purchase : ShareEntity
    {

        
        public Config_Purchase()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 次数id
        /// </summary>
        private int _id;
        [EntityField("id", IsKey = true)]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                SetChange("id", value);
            }
        }

        /// <summary>
        /// 体力
        /// </summary>
        private int _Stamina;
        [EntityField("Stamina")]
        public int Stamina
        {
            get
            {
                return _Stamina;
            }
            set
            {
                SetChange("Stamina", value);
            }
        }

        /// <summary>
        /// 消耗钻石
        /// </summary>
        private int _SpendDiamond;
        [EntityField("SpendDiamond")]
        public int SpendDiamond
        {
            get
            {
                return _SpendDiamond;
            }
            set
            {
                SetChange("SpendDiamond", value);
            }
        }
        
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "id": return id;
                    case "Stamina": return Stamina;
                    case "SpendDiamond": return SpendDiamond;
                    default: throw new ArgumentException(string.Format("Config_Purchase index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "id":
                        _id = value.ToInt(); 
                        break; 
                    case "Stamina":
                        _Stamina = value.ToInt(); 
                        break;
                    case "SpendDiamond":
                        _SpendDiamond = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Purchase index[{0}] isn't exist.", index));
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