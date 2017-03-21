
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户成就信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserAchievementCache : BaseEntity
    {

        public UserAchievementCache()
            : base(AccessLevel.ReadWrite)
        {
            AchievementList = new CacheList<AchievementData>();
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
        /// 成就列表
        /// </summary>
        private CacheList<AchievementData> _AchievementList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<AchievementData> AchievementList
        {
            get
            {
                return _AchievementList;
            }
            set
            {
                SetChange("AchievementList", value);
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
                    case "AchievementList": return AchievementList;
                    default: throw new ArgumentException(string.Format("UserAchievementCache index[{0}] isn't exist.", index));
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
                    case "AchievementList":
                        _AchievementList = ConvertCustomField<CacheList<AchievementData>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserAchievementCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public void ResetCache()
        {
            AchievementList.Clear();
            for (AchievementType type = AchievementType.LevelCount; type <= AchievementType.Diamond; ++type)
            {
                var achievement = new ShareCacheStruct<Config_Achievement>().Find(t => (t.AchievementType == type));
                if (achievement == null)
                    continue;
                AchievementData achdata = new AchievementData();
                achdata.Type = achievement.AchievementType;
                achdata.ID = achievement.id;
                if (type == AchievementType.LevelCount)
                {
                    achdata.Count = ConfigEnvSet.GetInt("User.Level");
                }
                AchievementList.Add(achdata);
            }
        }

        public AchievementData FindAchievement(AchievementType type)
        {
            return AchievementList.Find(t => t.Type == type);
        }

    }
}