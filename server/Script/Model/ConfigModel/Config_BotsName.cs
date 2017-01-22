
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_BotsName : ShareEntity
    {

        public Config_BotsName()
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
        private string _String;
        /// <summary>
        /// 技能名称
        /// </summary>
        [EntityField("String")]
        public string String
        {
            get
            {
                return _String;
            }
            private set
            {
                SetChange("String", value);
            }
        }
        private string _Value;
        /// <summary>
        /// 技能初始等级
        /// </summary>
        [EntityField("Value")]
        public string Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                SetChange("Value", value);
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
                    case "String": return String;
                    case "Value": return Value;
                    default: throw new ArgumentException(string.Format("Config_BotsName index[{0}] isn't exist.", index));
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
                    case "String":
                        _String = value.ToNotNullString(); 
                        break;
                    case "Value":
                        _Value = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_BotsName index[{0}] isn't exist.", index));
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