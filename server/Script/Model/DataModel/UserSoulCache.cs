
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
    /// 用户战魂信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserSoulCache : BaseEntity
    {
        
        public UserSoulCache()
            : base(AccessLevel.ReadWrite)
        {
            OpenList = new CacheList<int>();
            //ResetCache();
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
        /// 当前战魂ID
        /// </summary>
        private int _SoulID;
        [ProtoMember(2)]
        [EntityField("SoulID")]
        public int SoulID
        {
            get
            {
                return _SoulID;
            }
            set
            {
                SetChange("SoulID", value);
            }
        }

        /// <summary>
        /// 当前战魂开启格子记录
        /// </summary>
        private CacheList<int> _OpenList;
        [ProtoMember(3)]
        [EntityField(true, ColumnDbType.LongBlob)]
        public CacheList<int> OpenList
        {
            get
            {
                return _OpenList;
            }
            set
            {
                SetChange("OpenList", value);
            }
        }

        ///// <summary>
        ///// 生命
        ///// </summary>
        //private int _Hp;
        //[ProtoMember(4)]
        //[EntityField("Hp")]
        //public int Hp
        //{
        //    get
        //    {
        //        return _Hp;
        //    }
        //    set
        //    {
        //        SetChange("Hp", value);
        //    }
        //}

        ///// <summary>
        ///// 攻击
        ///// </summary>
        //private int _Atk;
        //[ProtoMember(5)]
        //[EntityField("Atk")]
        //public int Atk
        //{
        //    get
        //    {
        //        return _Atk;
        //    }
        //    set
        //    {
        //        SetChange("Atk", value);
        //    }
        //}

        ///// <summary>
        ///// 防御
        ///// </summary>
        //private int _Def;
        //[ProtoMember(6)]
        //[EntityField("Def")]
        //public int Def
        //{
        //    get
        //    {
        //        return _Def;
        //    }
        //    set
        //    {
        //        SetChange("Def", value);
        //    }
        //}

        ///// <summary>
        ///// 暴击
        ///// </summary>
        //private int _Crit;
        //[ProtoMember(7)]
        //[EntityField("Crit")]
        //public int Crit
        //{
        //    get
        //    {
        //        return _Crit;
        //    }
        //    set
        //    {
        //        SetChange("Crit", value);
        //    }
        //}

        ///// <summary>
        ///// 命中
        ///// </summary>
        //private int _Hit;
        //[ProtoMember(8)]
        //[EntityField("Hit")]
        //public int Hit
        //{
        //    get
        //    {
        //        return _Hit;
        //    }
        //    set
        //    {
        //        SetChange("Hit", value);
        //    }
        //}

        ///// <summary>
        ///// 闪避
        ///// </summary>
        //private int _Dodge;
        //[ProtoMember(9)]
        //[EntityField("Dodge")]
        //public int Dodge
        //{
        //    get
        //    {
        //        return _Dodge;
        //    }
        //    set
        //    {
        //        SetChange("Dodge", value);
        //    }
        //}

        ///// <summary>
        ///// 韧性
        ///// </summary>
        //private int _Tenacity;
        //[ProtoMember(10)]
        //[EntityField("Tenacity")]
        //public int Tenacity
        //{
        //    get
        //    {
        //        return _Tenacity;
        //    }
        //    set
        //    {
        //        SetChange("Tenacity", value);
        //    }
        //}


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
                    case "SoulID": return SoulID;
                    case "OpenList": return OpenList;
                    //case "Hp": return Hp;
                    //case "Atk": return Atk;
                    //case "Def": return Def;
                    //case "Crit": return Crit;
                    //case "Hit": return Hit;
                    //case "Dodge": return Dodge;
                    //case "Tenacity": return Tenacity;
                    default: throw new ArgumentException(string.Format("UserSoulCache index[{0}] isn't exist.", index));
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
                    case "SoulID":
                        _SoulID = value.ToInt();
                        break;
                    case "OpenList":
                        _OpenList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    //case "Hp":
                    //    _Hp = value.ToInt();
                    //    break;
                    //case "Atk":
                    //    _Atk = value.ToInt();
                    //    break;
                    //case "Def":
                    //    _Def = value.ToInt();
                    //    break;
                    //case "Crit":
                    //    _Crit = value.ToInt();
                    //    break;
                    //case "Hit":
                    //    _Hit = value.ToInt();
                    //    break;
                    //case "Dodge":
                    //    _Dodge = value.ToInt();
                    //    break;
                    //case "Tenacity":
                    //    _Tenacity = value.ToInt();
                    //    break;
                    default: throw new ArgumentException(string.Format("UserSoulCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetCache()
        {
            SoulID = 10001;
            OpenList.Clear();
        }
    }
}