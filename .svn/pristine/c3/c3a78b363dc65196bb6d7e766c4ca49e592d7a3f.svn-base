
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
    public class Config_Task : ShareEntity
    {

        
        public Config_Task()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// id
        /// </summary>
        private TaskType _id;
        [EntityField("id", IsKey = true)]
        public TaskType id
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
        /// 任务目标
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
        /// 任务奖励类型
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
        /// 任务奖励数量
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
                    case "Objective": return Objective;
                    case "RewardsType": return RewardsType;
                    case "RewardsNum": return RewardsNum;
                    default: throw new ArgumentException(string.Format("Config_Task index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "id":
                        _id = value.ToEnum<TaskType>(); 
                        break; 
                    case "Objective":
                        _Objective = value.ToNotNullString(); 
                        break;
                    case "RewardsType":
                        _RewardsType = value.ToEnum<TaskAwardType>();
                        break;
                    case "RewardsNum":
                        _RewardsNum = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Task index[{0}] isn't exist.", index));
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