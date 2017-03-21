
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
    /// 用户背包信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserPackageCache : BaseEntity
    {
        
        public UserPackageCache()
            : base(AccessLevel.ReadWrite)
        {
            ItemList = new CacheList<ItemData>();
            NewItemCache = new CacheList<ItemData>();
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
        /// 背包物品列表
        /// </summary>
        private CacheList<ItemData> _ItemList;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<ItemData> ItemList
        {
            get
            {
                return _ItemList;
            }
            set
            {
                SetChange("ItemList", value);
            }
        }

        [ProtoMember(3)]
        public CacheList<ItemData> NewItemCache { get; set; }

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
                    case "ItemList": return ItemList;
                    default: throw new ArgumentException(string.Format("UserPackageCache index[{0}] isn't exist.", index));
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
                    case "ItemList":
                        _ItemList = ConvertCustomField<CacheList<ItemData>>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserPackageCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }


        public ItemData FindItem(int id)
        {
            return ItemList.Find(t => (t.ID == id));
        }
        

        /// <summary>  
        /// 用户获得道具
        /// </summary>  
        /// <returns></returns>  
        public bool AddItem(int id, int num)
        {
            if (id == 0)
                return false;

            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(id);
            if (itemcfg == null)
                return false;

            ItemData item = ItemList.Find(t => (t.ID == id));
            if (item == null)
            {
                item = new ItemData();
                item.ID = id;
                item.Num = MathUtils.Addition(item.Num, num);
                ItemList.Add(item);
            }
            else
            {
                item.Num = MathUtils.Addition(item.Num, num);
            }

            item = NewItemCache.Find(t => (t.ID == id));
            if (item == null)
            {
                item = new ItemData();
                item.ID = id;
                item.Num = MathUtils.Addition(item.Num, num);
                NewItemCache.Add(item);
            }
            else
            {
                item.Num = MathUtils.Addition(item.Num, num);
            }
            return true;
        }

        public void RemoveItem(int itemId, int itemNum)
        {
            ItemData item = FindItem(itemId);
            if (item.Num > itemNum)
            {
                item.Num = item.Num - itemNum;
            }
            else
            {
                ItemList.Remove(item);
            }
        }

        public void ResetCache()
        {
            ItemList.Clear();
            NewItemCache.Clear();
        }
    }
}