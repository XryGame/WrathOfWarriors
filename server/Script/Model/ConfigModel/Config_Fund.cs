
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
    public class Config_Fund : ShareEntity
    {

        
        public Config_Fund()
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
        /// 等级
        /// </summary>
        private int _grade;
        [EntityField("grade")]
        public int grade
        {
            get
            {
                return _grade;
            }
            set
            {
                SetChange("grade", value);
            }
        }

        /// <summary>
        /// 50元返利
        /// </summary>
        private int _fund50;
        [EntityField("fund50")]
        public int fund50
        {
            get
            {
                return _fund50;
            }
            set
            {
                SetChange("fund50", value);
            }
        }

        /// <summary>
        /// 98元返利
        /// </summary>
        private int _fund98;
        [EntityField("fund98")]
        public int fund98
        {
            get
            {
                return _fund98;
            }
            set
            {
                SetChange("fund98", value);
            }
        }

        /// <summary>
        /// 298元返利
        /// </summary>
        private int _fund298;
        [EntityField("fund298")]
        public int fund298
        {
            get
            {
                return _fund298;
            }
            set
            {
                SetChange("fund298", value);
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
                    case "grade": return grade;
                    case "fund50": return fund50;
                    case "fund98": return fund98;
                    case "fund298": return fund298;
                    default: throw new ArgumentException(string.Format("Config_Fund index[{0}] isn't exist.", index));
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
                    case "grade":
                        _grade = value.ToInt(); 
                        break;
                    case "fund50":
                        _fund50 = value.ToInt();
                        break;
                    case "fund98":
                        _fund98 = value.ToInt();
                        break;
                    case "fund298":
                        _fund298 = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Fund index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
	}
}