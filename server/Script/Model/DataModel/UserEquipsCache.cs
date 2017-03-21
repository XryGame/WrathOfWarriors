
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
    /// 用户装备信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserEquipsCache : BaseEntity
    {

        public UserEquipsCache()
            : base(AccessLevel.ReadWrite)
        {
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
        /// 武器
        /// </summary>
        private EquipData _Weapon;
        [ProtoMember(2)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public EquipData Weapon
        {
            get
            {
                return _Weapon;
            }
            set
            {
                SetChange("Weapon", value);
            }
        }

        /// <summary>
        /// 衣服
        /// </summary>
        private EquipData _Coat;
        [ProtoMember(3)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public EquipData Coat
        {
            get
            {
                return _Coat;
            }
            set
            {
                SetChange("Coat", value);
            }
        }

        /// <summary>
        /// 戒指
        /// </summary>
        private EquipData _Ring;
        [ProtoMember(4)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public EquipData Ring
        {
            get
            {
                return _Ring;
            }
            set
            {
                SetChange("Ring", value);
            }
        }

        /// <summary>
        /// 鞋子
        /// </summary>
        private EquipData _Shoe;
        [ProtoMember(5)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public EquipData Shoe
        {
            get
            {
                return _Shoe;
            }
            set
            {
                SetChange("Shoe", value);
            }
        }

        /// <summary>
        /// 饰品
        /// </summary>
        private EquipData _Accessory;
        [ProtoMember(6)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public EquipData Accessory
        {
            get
            {
                return _Accessory;
            }
            set
            {
                SetChange("Accessory", value);
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
                    case "Weapon": return Weapon;
                    case "Coat": return Coat;
                    case "Ring": return Ring;
                    case "Shoe": return Shoe;
                    case "Accessory": return Accessory;
                    default: throw new ArgumentException(string.Format("UserEquipCache index[{0}] isn't exist.", index));
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
                    case "Weapon":
                        _Weapon = ConvertCustomField<EquipData>(value, index);
                        break;
                    case "Coat":
                        _Coat = ConvertCustomField<EquipData>(value, index);
                        break;
                    case "Ring":
                        _Ring = ConvertCustomField<EquipData>(value, index);
                        break;
                    case "Shoe":
                        _Shoe = ConvertCustomField<EquipData>(value, index);
                        break;
                    case "Accessory":
                        _Accessory = ConvertCustomField<EquipData>(value, index);
                        break;
                    default: throw new ArgumentException(string.Format("UserEquipCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public EquipData FindEquipData(EquipID id)
        {
            EquipData equip = null;
            switch (id)
            {
                case EquipID.Coat:
                    equip = Coat;
                    break;
                case EquipID.Weapon:
                    equip = Weapon;
                    break;
                case EquipID.Shoe:
                    equip = Shoe;
                    break;
                case EquipID.Accessory:
                    equip = Accessory;
                    break;
                case EquipID.Ring:
                    equip = Ring;
                    break;
            }
            return equip;
        }

        public void ResetCache()
        {

            Weapon = new EquipData()
            {
                ID = EquipID.Weapon,
                Lv = 1,
            };
            Coat = new EquipData()
            {
                ID = EquipID.Coat,
                Lv = 1,
            };
            Ring = new EquipData()
            {
                ID = EquipID.Ring,
                Lv = 1,
            };
            Shoe = new EquipData()
            {
                ID = EquipID.Shoe,
                Lv = 1,
            };
            Accessory = new EquipData()
            {
                ID = EquipID.Accessory,
                Lv = 1,
            };

        }

    }
}