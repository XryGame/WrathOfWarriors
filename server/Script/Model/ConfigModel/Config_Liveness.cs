
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
    public class Config_Liveness : ShareEntity
    {

        
        public Config_Liveness()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// ID
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
        /// 活跃度
        /// </summary>
        private int _Liveness;
        [EntityField("Liveness")]
        public int Liveness
        {
            get
            {
                return _Liveness;
            }
            set
            {
                SetChange("Liveness", value);
            }
        }

        /// <summary>
        /// 奖励礼包ID
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
        

        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "ID": return ID;
                    case "Liveness": return Liveness;
                    case "ItemID": return ItemID;
                    default: throw new ArgumentException(string.Format("Config_Liveness index[{0}] isn't exist.", index));
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
                    case "Liveness":
                        _Liveness = value.ToInt();
                        break;
                    case "ItemID":
                        _ItemID = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Liveness index[{0}] isn't exist.", index));
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