
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
    public class Config_CdKey : ShareEntity
    {

        
        public Config_CdKey()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// Key
        /// </summary>
        private string _Key;
        [EntityField("Key", IsKey = true)]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                SetChange("Key", value);
            }
        }


        /// <summary>
        /// 奖励A
        /// </summary>
        private int _AwardA;
        [EntityField("AwardA")]
        public int AwardA
        {
            get
            {
                return _AwardA;
            }
            set
            {
                SetChange("AwardA", value);
            }
        }

        /// <summary>
        /// 奖励B
        /// </summary>
        private int _AwardB;
        [EntityField("AwardB")]
        public int AwardB
        {
            get
            {
                return _AwardB;
            }
            set
            {
                SetChange("AwardB", value);
            }
        }

        /// <summary>
        /// 奖励C
        /// </summary>
        private int _AwardC;
        [EntityField("AwardC")]
        public int AwardC
        {
            get
            {
                return _AwardC;
            }
            set
            {
                SetChange("AwardC", value);
            }
        }

        /// <summary>
        /// 奖励D
        /// </summary>
        private int _AwardD;
        [EntityField("AwardD")]
        public int AwardD
        {
            get
            {
                return _AwardD;
            }
            set
            {
                SetChange("AwardD", value);
            }
        }

        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "Key": return Key;
                    case "AwardA": return AwardA;
                    case "AwardB": return AwardB;
                    case "AwardC": return AwardC;
                    case "AwardD": return AwardD;
                    default: throw new ArgumentException(string.Format("Config_CdKey index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "Key":
                        _Key = value.ToNotNullString();
                        break;
                    case "AwardA":
                        _AwardA = value.ToInt();
                        break;
                    case "AwardB":
                        _AwardB = value.ToInt();
                        break;
                    case "AwardC":
                        _AwardC = value.ToInt();
                        break;
                    case "AwardD":
                        _AwardD = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_CdKey index[{0}] isn't exist.", index));
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