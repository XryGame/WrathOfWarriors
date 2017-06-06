
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
    public class Config_Soulstrong : ShareEntity
    {

        public Config_Soulstrong()
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
            private set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 战魂id
        /// </summary>
        private int _Soulid;
        [EntityField("Soulid")]
        public int Soulid
        {
            get
            {
                return _Soulid;
            }
            private set
            {
                SetChange("Soulid", value);
            }
        }

        /// <summary>
        /// 格子id
        /// </summary>
        private int _GridId;
        [EntityField("GridId")]
        public int GridId
        {
            get
            {
                return _GridId;
            }
            private set
            {
                SetChange("GridId", value);
            }
        }

        /// <summary>
        /// 限制等级
        /// </summary>
        private int _Grade;
        [EntityField("Grade")]
        public int Grade
        {
            get
            {
                return _Grade;
            }
            private set
            {
                SetChange("Grade", value);
            }
        }

        /// <summary>
        /// 格子加成属性类型
        /// </summary>
        private SoulAddType _Attdef;
        [EntityField("Attdef")]
        public SoulAddType Attdef
        {
            get
            {
                return _Attdef;
            }
            private set
            {
                SetChange("Attdef", value);
            }
        }

        /// <summary>
        /// 加成值
        /// </summary>
        private long _AddValue;
        [EntityField("AddValue")]
        public long AddValue
        {
            get
            {
                return _AddValue;
            }
            private set
            {
                SetChange("AddValue", value);
            }
        }

        /// <summary>
        /// 格子初始状态
        /// </summary>
        private int _GridState;
        [EntityField("GridState")]
        public int GridState
        {
            get
            {
                return _GridState;
            }
            private set
            {
                SetChange("GridState", value);
            }
        }

        /// <summary>
        /// 点亮此格子需要消耗的货币类型
        /// </summary>
        private CoinType _ConsumeType;
        [EntityField("ConsumeType")]
        public CoinType ConsumeType
        {
            get
            {
                return _ConsumeType;
            }
            private set
            {
                SetChange("ConsumeType", value);
            }
        }

        /// <summary>
        /// 点亮此格子需要消耗的货币数量
        /// </summary>
        private string _ConsumeNum;
        [EntityField("ConsumeNum")]
        public string ConsumeNum
        {
            get
            {
                return _ConsumeNum;
            }
            private set
            {
                SetChange("ConsumeNum", value);
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
                    case "Soulid": return Soulid;
                    case "GridId": return GridId;
                    case "Grade": return Grade;
                    case "Attdef": return Attdef;
                    case "AddValue": return AddValue;
                    case "GridState": return GridState;
                    case "ConsumeType": return ConsumeType;
                    case "ConsumeNum": return ConsumeNum;
                    default: throw new ArgumentException(string.Format("Config_Soulstrong index[{0}] isn't exist.", index));
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
                    case "Soulid":
                        _Soulid = value.ToInt(); 
                        break;
                    case "GridId":
                        _GridId = value.ToInt();
                        break;
                    case "Grade":
                        _Grade = value.ToInt(); 
                        break; 
                    case "Attdef":
                        _Attdef = value.ToEnum<SoulAddType>();
                        break; 
                    case "AddValue":
                        _AddValue = value.ToLong();
                        break;
                    case "GridState":
                        _GridState = value.ToInt();
                        break;
                    case "ConsumeType":
                        _ConsumeType = value.ToEnum<CoinType>();
                        break;
                    case "ConsumeNum":
                        _ConsumeNum = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Soulstrong index[{0}] isn't exist.", index));
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