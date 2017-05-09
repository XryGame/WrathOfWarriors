
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
    public class Config_Lottery : ShareEntity
    {

        
        public Config_Lottery()
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
            set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 等级限制
        /// </summary>
        private int _Level;
        [EntityField("Level")]
        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                SetChange("Level", value);
            }
        }

        /// <summary>
        /// 奖励类型(1物品，2钻石)
        /// </summary>
        private LotteryAwardType _Type;
        [EntityField("Type")]
        public LotteryAwardType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                SetChange("Type", value);
            }
        }

        /// <summary>
        /// 数量或id
        /// </summary>
        private string _Content;
        [EntityField("Content")]
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                SetChange("Content", value);
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
                    case "Level": return Level;
                    case "Type": return Type;
                    case "Content": return Content;
                    case "Weight": return Weight;
                    default: throw new ArgumentException(string.Format("Config_Lottery index[{0}] isn't exist.", index));
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
                    case "Level":
                        _Level = value.ToInt(); 
                        break;
                    case "Type":
                        _Type = value.ToEnum<LotteryAwardType>();
                        break;
                    case "Content":
                        _Content = value.ToNotNullString("0");
                        break;
                    case "Weight":
                        _Weight = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Lottery index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
	}
}