
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 技能
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_Skill : ShareEntity
    {

        public Config_Skill()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 技能id
        /// </summary>
        private int _SkillID;
        [EntityField("SkillID", IsKey = true)]
        public int SkillID
        {
            get
            {
                return _SkillID;
            }
            private set
            {
                SetChange("SkillID", value);
            }
        }

        /// <summary>
        /// 技能名称
        /// </summary>
        private string _SkillName;
        [EntityField("SkillName")]
        public string SkillName
        {
            get
            {
                return _SkillName;
            }
            private set
            {
                SetChange("SkillName", value);
            }
        }

        /// <summary>
        /// 所属职业
        /// </summary>
        private int _SkillGroup;
        [EntityField("SkillGroup")]
        public int SkillGroup
        {
            get
            {
                return _SkillGroup;
            }
            private set
            {
                SetChange("SkillGroup", value);
            }
        }

        /// <summary>
        /// 技能类型
        /// </summary>
        private int _SkillType;
        [EntityField("SkillType")]
        public int SkillType
        {
            get
            {
                return _SkillType;
            }
            private set
            {
                SetChange("SkillType", value);
            }
        }
        /// <summary>
        /// 技能最大等级
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

        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "SkillID": return SkillID;
                    case "SkillName": return SkillName;
                    case "SkillGroup": return SkillGroup;
                    case "SkillType": return SkillType;
                    case "SkillGrade": return SkillGrade;
                    default: throw new ArgumentException(string.Format("Config_Skill index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "SkillID":
                        _SkillID = value.ToInt();
                        break;
                    case "SkillName":
                        _SkillName = value.ToNotNullString();
                        break;
                    case "SkillGroup":
                        _SkillGroup = value.ToInt();
                        break;
                    case "SkillType":
                        _SkillType = value.ToInt();
                        break;
                    case "SkillGrade":
                        _SkillGrade = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Skill index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}