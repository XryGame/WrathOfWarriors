
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 技能等级
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_SkillGrade : ShareEntity
    {

        public Config_SkillGrade()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 唯一ID
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
        /// 技能id
        /// </summary>
        private int _SkillId;
        [EntityField("SkillId")]
        public int SkillId
        {
            get
            {
                return _SkillId;
            }
            private set
            {
                SetChange("SkillId", value);
            }
        }

        /// <summary>
        /// 技能等级
        /// </summary>
        private int _SkillGrade;
        [EntityField("SkillGrade")]
        public int SkillGrade
        {
            get
            {
                return _SkillGrade;
            }
            private set
            {
                SetChange("SkillGrade", value);
            }
        }

        /// <summary>
        /// 升级消耗货币类型
        /// </summary>
        private ConsumeType _ConsumeType;
        [EntityField("ConsumeType")]
        public ConsumeType ConsumeType
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
        /// 升级消耗货币数量
        /// </summary>
        private string _ConsumeNumber;
        [EntityField("ConsumeNumber")]
        public string ConsumeNumber
        {
            get
            {
                return _ConsumeNumber;
            }
            private set
            {
                SetChange("ConsumeNumber", value);
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
                    case "SkillId": return SkillId;
                    case "SkillGrade": return SkillGrade;
                    case "ConsumeType": return ConsumeType;
                    case "ConsumeNumber": return ConsumeNumber;
                    default: throw new ArgumentException(string.Format("Config_SkillGrade index[{0}] isn't exist.", index));
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
                    case "SkillId":
                        _SkillId = value.ToInt();
                        break;
                    case "SkillGrade":
                        _SkillGrade = value.ToInt();
                        break;
                    case "ConsumeType":
                        _ConsumeType = value.ToEnum<ConsumeType>();
                        break;
                    case "ConsumeNumber":
                        _ConsumeNumber = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_SkillGrade index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}