
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;
using System.Collections.Generic;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户任务信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserTaskCache : BaseEntity
    {
        
        public UserTaskCache()
            : base(AccessLevel.ReadWrite)
        {
            DailyQuestList = new CacheList<UserDailyQuestData>();
            ResetCache();
        }
        
        private int _UserID;
        [ProtoMember(1)]
        [EntityField("UserID", IsKey = true)]
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                SetChange("UserID", value);
            }
        }

        /// <summary>
        /// 每日任务
        /// </summary>
        private CacheList<UserDailyQuestData> _DailyQuestList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<UserDailyQuestData> DailyQuestList
        {
            get
            {
                return _DailyQuestList;
            }
            set
            {
                SetChange("DailyQuestList", value);
            }
        }

        /// <summary>
        /// 剧情ID
        /// </summary>
        private int _PlotId;
        [ProtoMember(3)]
        [EntityField("PlotId")]
        public int PlotId
        {
            get
            {
                return _PlotId;
            }
            set
            {
                SetChange("PlotId", value);
            }
        }


        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "DailyQuestList": return DailyQuestList;
                    case "PlotId": return PlotId;
                    default: throw new ArgumentException(string.Format("UserTaskCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
            set
            {
                #region
                switch (index)
                {
                    case "UserID":
                        _UserID = value.ToInt();
                        break;
                    case "DailyQuestList":
                        _DailyQuestList = ConvertCustomField<CacheList<UserDailyQuestData>>(value, index);
                        break;
                    case "PlotId":
                        _PlotId = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserTaskCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public void ResetCache()
        {
            DailyQuestList.Clear();
            var taskSet = new ShareCacheStruct<Config_Task>();
            for (TaskType type = TaskType.Login; type <= TaskType.WorldChat; ++type)
            {
                var taskcfg = taskSet.Find(t => (t.id == type));
                if (taskcfg == null)
                    continue;
                var dailyQuest = new UserDailyQuestData();
                dailyQuest.ID = type;
                dailyQuest.Count = 0;
                dailyQuest.IsFinished = false;
                dailyQuest.IsReceived = false;
               
                DailyQuestList.Add(dailyQuest);
            }
        }

        public UserDailyQuestData FindTask(TaskType id)
        {
            return DailyQuestList.Find(t => t.ID == id);
        }
    }
}