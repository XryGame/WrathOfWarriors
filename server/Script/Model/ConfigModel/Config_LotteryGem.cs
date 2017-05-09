
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
    public class Config_LotteryGem : ShareEntity
    {

        
        public Config_LotteryGem()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 宝石id
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
        /// 权重数值
        /// </summary>
        private int _Weight;
        [EntityField("Weight")]
        public int Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                SetChange("Weight", value);
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
                    case "Weight": return Weight;
                    default: throw new ArgumentException(string.Format("Config_LotteryGem index[{0}] isn't exist.", index));
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
                    case "Weight":
                        _Weight = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_LotteryGem index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
	}
}