
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
        private int _ObjectiveNum;
        [EntityField("ObjectiveNum")]
        public int ObjectiveNum
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
        /// 成就奖励类型
        /// </summary>
        private AwardType _RewardsType;
        [EntityField("RewardsType")]
        public AwardType RewardsType
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
        private int _RewardsID;
        [EntityField("RewardsID")]
        public int RewardsID
        {
            get
            {
                return _RewardsID;
            }
            set
            {
                SetChange("RewardsID", value);
            }
        }

        /// <summary>
        /// 成就奖励数量
        /// </summary>
        private int _RewardsNum;
        [EntityField("RewardsNum")]
        public int RewardsNum
        {
            get
            {
                return _RewardsNum;
            }
            set
            {
                SetChange("RewardsNum", value);
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
                    case "RewardsType": return RewardsType;
                    case "RewardsID": return RewardsID;
                    case "RewardsNum": return RewardsNum;
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
                        _ObjectiveNum = value.ToInt();
                        break;
                    case "RewardsType":
                        _RewardsType = value.ToEnum<AwardType>();
                        break;
                    case "RewardsID":
                        _RewardsID = value.ToInt();
                        break;
                    case "RewardsNum":
                        _RewardsNum = value.ToInt();
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