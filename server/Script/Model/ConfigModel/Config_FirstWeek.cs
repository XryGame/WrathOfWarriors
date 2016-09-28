
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
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_FirstWeek : ShareEntity
    {

        
        public Config_FirstWeek()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 天数id
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
        /// 奖励类型
        /// </summary>
        private AwardType _AwardType;
        [EntityField("AwardType")]
        public AwardType AwardType
        {
            get
            {
                return _AwardType;
            }
            set
            {
                SetChange("AwardType", value);
            }
        }
        /// <summary>
        /// 奖励ID
        /// </summary>
        private int _AwardID;
        [EntityField("AwardID")]
        public int AwardID
        {
            get
            {
                return _AwardID;
            }
            set
            {
                SetChange("AwardID", value);
            }
        }

        /// <summary>
        /// 奖励数量
        /// </summary>
        private int _AwardNum;
        [EntityField("AwardNum")]
        public int AwardNum
        {
            get
            {
                return _AwardNum;
            }
            set
            {
                SetChange("AwardNum", value);
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
                    case "AwardType": return AwardType;
                    case "AwardID": return AwardID;
                    case "AwardNum": return AwardNum;
                    default: throw new ArgumentException(string.Format("Config_FirstWeek index[{0}] isn't exist.", index));
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
                    case "AwardType":
                        _AwardType = value.ToEnum<AwardType>();
                        break;
                    case "AwardID":
                        _AwardID = value.ToInt();
                        break;
                    case "AwardNum":
                        _AwardNum = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_FirstWeek index[{0}] isn't exist.", index));
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