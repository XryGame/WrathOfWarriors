
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
    /// 用户技能
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserSkillCache : BaseEntity
    {
        
        public UserSkillCache()
            : base(AccessLevel.ReadWrite)
        {
            SkillList = new CacheList<SkillData>();
            CarryList = new CacheList<int>();
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
        /// 技能列表
        /// </summary>
        private CacheList<SkillData> _SkillList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<SkillData> SkillList
        {
            get
            {
                return _SkillList;
            }
            set
            {
                SetChange("SkillList", value);
            }
        }

        /// <summary>
        /// 技能携带列表
        /// </summary>
        private CacheList<int> _CarryList;
        [ProtoMember(3)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> CarryList
        {
            get
            {
                return _CarryList;
            }
            set
            {
                SetChange("CarryList", value);
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
                    case "SkillList": return SkillList;
                    case "CarryList": return CarryList;
                    default: throw new ArgumentException(string.Format("UserSkillCache index[{0}] isn't exist.", index));
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
                    case "SkillList":
                        _SkillList = ConvertCustomField<CacheList<SkillData>>(value, index);
                        break;
                    case "CarryList":
                        _CarryList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserSkillCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public SkillData FindSkill(int id)
        {
            return SkillList.Find(t => (t.ID == id));
        }
        

        /// <summary>  
        /// 用户获得道具
        /// </summary>  
        /// <returns></returns>  
        public bool AddSkill(int id)
        {
            if (id == 0)
                return false;

            var skill = SkillList.Find(t => (t.ID == id));
            if (skill != null)
            {
                return false;
            }
            skill = new SkillData();
            skill.ID = id;
            skill.Lv = 1;
            SkillList.Add(skill);
            return true;
        }

        public void ResetCache(int profession)
        {
            
            SkillList.Clear();
            CarryList.Clear();

            var skillConfig = new ShareCacheStruct<Config_Skill>();
            var list = skillConfig.FindAll(t => (
                t.SkillGroup == profession/* && (t.SkillID % 10000 == 0 || t.SkillID % 10000 == 1)*/)
                );
 
            foreach (var v in list)
            {
                AddSkill(v.SkillID);
            }

            if (list.Count > 3)
            {
                Random random = new Random();
                while (CarryList.Count < 3)
                {
                    int index = random.Next(list.Count);
                    int addSkillId = list[index].SkillID;
                    if (addSkillId == 10000 || addSkillId == 20000)
                        continue;
                    if (CarryList.Find(t => t == addSkillId) == 0)
                        CarryList.Add(addSkillId);
                }
            }
            
        }
    }
}