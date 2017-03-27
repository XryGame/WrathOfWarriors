
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
    public class Config_Achievement : ShareEntity
    {

        
        public Config_Achievement()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// id
        /// </summary>
        private int _id;
        [EntityField("id", IsKey = true)]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                SetChange("id", value);
            }
        }
        /// <summary>
        /// 成就类型
        /// </summary>
        private AchievementType _AchievementType;
        [EntityField("AchievementType")]
        public AchievementType AchievementType
        {
            get
            {
                return _AchievementType;
            }
            set
            {
                SetChange("AchievementType", value);
            }
        }
        /// <summary>
        /// 成就名称
        /// </summary>
        private string _Achievement;
        [EntityField("Achievement")]
        public string Achievement
        {
            get
            {
                return _Achievement;
            }
            set
            {
                SetChange("Achievement", value);
            }
        }
        /// <summary>
        /// 成就目标
        /// </summary>
        private string _Objective;
        [EntityField("Objective")]
        public string Objective
        {
            get
            {
                return _Objective;
            }
            set
            {
                SetChange("Objective", value);
            }
        }
        /// <summary>
        /// 成就目标数量
        /// </summary>
        private string _ObjectiveNum;
        [EntityField("ObjectiveNum")]
        public string ObjectiveNum
        {
            get
            {
                return _ObjectiveNum;
            }
            set
            {
                SetChange("ObjectiveNum", value);
            }
        }

        /// <summary>
        /// 成就目标等级
        /// </summary>
        private int _ObjectiveGrade;
        [EntityField("ObjectiveGrade")]
        public int ObjectiveGrade
        {
            get
            {
                return _ObjectiveGrade;
            }
            set
            {
                SetChange("ObjectiveGrade", value);
            }
        }

        /// <summary>
        /// 成就奖励类型
        /// </summary>
        private TaskAwardType _RewardsType;
        [EntityField("RewardsType")]
        public TaskAwardType RewardsType
        {
            get
            {
                return _RewardsType;
            }
            set
            {
                SetChange("RewardsType", value);
            }
        }

        /// <summary>
        /// 成就奖励id
        /// </summary>
        private int _RewardsItemID;
        [EntityField("RewardsItemID")]
        public int RewardsItemID
        {
            get
            {
                return _RewardsItemID;
            }
            set
            {
                SetChange("RewardsItemID", value);
            }
        }

        /// <summary>
        /// 成就奖励数量
        /// </summary>
        private string _RewardsItemNum;
        [EntityField("RewardsItemNum")]
        public string RewardsItemNum
        {
            get
            {
                return _RewardsItemNum;
            }
            set
            {
                SetChange("RewardsItemNum", value);
            }
        }

        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "id": return id;
                    case "AchievementType": return AchievementType;
                    case "Achievement": return Achievement;
                    case "Objective": return Objective;
                    case "ObjectiveNum": return ObjectiveNum;
                    case "ObjectiveGrade": return ObjectiveGrade;
                    case "RewardsType": return RewardsType;
                    case "RewardsItemID": return RewardsItemID;
                    case "RewardsItemNum": return RewardsItemNum;
                    default: throw new ArgumentException(string.Format("Config_Achievement index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "id":
                        _id = value.ToInt(); 
                        break; 
                    case "AchievementType":
                        _AchievementType = value.ToEnum<AchievementType>();
                        break;
                    case "Achievement":
                        _Achievement = value.ToNotNullString();
                        break;
                    case "Objective":
                        _Objective = value.ToNotNullString();
                        break;
                    case "ObjectiveNum":
                        _ObjectiveNum = value.ToNotNullString();
                        break;
                    case "ObjectiveGrade":
                        _ObjectiveGrade = value.ToInt();
                        break;
                    case "RewardsType":
                        _RewardsType = value.ToEnum<TaskAwardType>();
                        break;
                    case "RewardsItemID":
                        _RewardsItemID = value.ToInt();
                        break;
                    case "RewardsItemNum":
                        _RewardsItemNum = value.ToNotNullString();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Achievement index[{0}] isn't exist.", index));
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