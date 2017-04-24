
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
    /// 用户精灵
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserElfCache : BaseEntity
    {
        
        public UserElfCache()
            : base(AccessLevel.ReadWrite)
        {
            ElfList = new CacheList<ElfData>();
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
        private CacheList<ElfData> _ElfList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<ElfData> ElfList
        {
            get
            {
                return _ElfList;
            }
            set
            {
                SetChange("ElfList", value);
            }
        }

        private int _SelectID;
        [ProtoMember(3)]
        [EntityField("SelectID")]
        public int SelectID
        {
            get
            {
                return _SelectID;
            }
            set
            {
                SetChange("SelectID", value);
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
                    case "ElfList": return ElfList;
                    case "SelectID": return SelectID;
                    default: throw new ArgumentException(string.Format("UserElfCache index[{0}] isn't exist.", index));
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
                    case "ElfList":
                        _ElfList = ConvertCustomField<CacheList<ElfData>>(value, index);
                        break;
                    case "SelectID":
                        _SelectID = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("UserElfCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public ElfData FindElf(int id)
        {
            return ElfList.Find(t => (t.ID == id));
        }

        /// <summary>  
        /// 用户获得精灵
        /// </summary>  
        /// <returns></returns>  
        public bool AddElf(int elfid)
        {
            if (elfid == 0)
                return false;

            var elf = ElfList.Find(t => (t.ID == elfid));
            if (elf != null)
            {
                return false;
            }
            elf = new ElfData();
            elf.ID = elfid;
            elf.Lv = 1;
            ElfList.Add(elf);
            return true;
        }

        public void ResetCache()
        {
            ElfList.Clear();

            var elvesSet = new ShareCacheStruct<Config_Elves>();
            var list = elvesSet.FindAll(t => (t.ElvesGrade == 1));
            foreach (var v in list)
            {
                AddElf(v.ElvesID);
            }
        }
    }
}