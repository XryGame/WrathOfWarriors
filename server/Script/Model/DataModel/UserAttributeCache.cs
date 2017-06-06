
using System;
using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.ConfigModel;
using ZyGames.Framework.Common.Log;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Config;

namespace GameServer.Script.Model.DataModel
{

    /// <summary>
    /// 用户属性信息
    /// </summary>

    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class UserAttributeCache : BaseEntity
    {

        public UserAttributeCache()
            : base(AccessLevel.ReadWrite)
        {

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
        /// 战斗力
        /// </summary>
        private long _FightValue;
        [ProtoMember(2)]
        [EntityField("FightValue")]
        public long FightValue
        {
            get
            {
                return _FightValue;
            }
            set
            {
                SetChange("FightValue", value);
            }
        }

        /// <summary>
        /// 生命
        /// </summary>
        private long _Hp;
        [ProtoMember(3)]
        [EntityField("Hp")]
        public long Hp
        {
            get
            {
                return _Hp;
            }
            set
            {
                SetChange("Hp", value);
            }
        }

        /// <summary>
        /// 攻击
        /// </summary>
        private long _Atk;
        [ProtoMember(4)]
        [EntityField("Atk")]
        public long Atk
        {
            get
            {
                return _Atk;
            }
            set
            {
                SetChange("Atk", value);
            }
        }

        /// <summary>
        /// 防御
        /// </summary>
        private long _Def;
        [ProtoMember(5)]
        [EntityField("Def")]
        public long Def
        {
            get
            {
                return _Def;
            }
            set
            {
                SetChange("Def", value);
            }
        }

        /// <summary>
        /// 暴击
        /// </summary>
        private long _Crit;
        [ProtoMember(6)]
        [EntityField("Crit")]
        public long Crit
        {
            get
            {
                return _Crit;
            }
            set
            {
                SetChange("Crit", value);
            }
        }

        /// <summary>
        /// 命中
        /// </summary>
        private long _Hit;
        [ProtoMember(7)]
        [EntityField("Hit")]
        public long Hit
        {
            get
            {
                return _Hit;
            }
            set
            {
                SetChange("Hit", value);
            }
        }

        /// <summary>
        /// 闪避
        /// </summary>
        private long _Dodge;
        [ProtoMember(8)]
        [EntityField("Dodge")]
        public long Dodge
        {
            get
            {
                return _Dodge;
            }
            set
            {
                SetChange("Dodge", value);
            }
        }

        /// <summary>
        /// 韧性
        /// </summary>
        private long _Tenacity;
        [ProtoMember(9)]
        [EntityField("Tenacity")]
        public long Tenacity
        {
            get
            {
                return _Tenacity;
            }
            set
            {
                SetChange("Tenacity", value);
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
                    case "FightValue": return FightValue;
                    case "Hp": return Hp;
                    case "Atk": return Atk;
                    case "Def": return Def;
                    case "Crit": return Crit;
                    case "Hit": return Hit;
                    case "Dodge": return Dodge;
                    case "Tenacity": return Tenacity;
                    default: throw new ArgumentException(string.Format("UserAttributeCache index[{0}] isn't exist.", index));
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
                    case "FightValue":
                        _FightValue = value.ToLong();
                        break;
                    case "Hp":
                        _Hp = value.ToLong();
                        break;
                    case "Atk":
                        _Atk = value.ToLong();
                        break;
                    case "Def":
                        _Def = value.ToLong();
                        break;
                    case "Crit":
                        _Crit = value.ToLong();
                        break;
                    case "Hit":
                        _Hit = value.ToLong();
                        break;
                    case "Dodge":
                        _Dodge = value.ToLong();
                        break;
                    case "Tenacity":
                        _Tenacity = value.ToLong();
                        break;
                    default: throw new ArgumentException(string.Format("UserAttributeCache index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }

        public void ResetAtt()
        {
            Hp = 0;
            Atk = 0;
            Def = 0;
            Crit = 0;
            Hit = 0;
            Dodge = 0;
            Tenacity = 0;
            FightValue = 0;
        }

        public void AppandBaseAttribute(int lv)
        {
            var roleconfig = new ShareCacheStruct<Config_RoleInitial>().FindKey(lv);
            if (roleconfig == null)
            {
                TraceLog.WriteError("AppandBaseAttribute: No found grade config! code={0}", lv);
                return;
            }
            
            Hp += roleconfig.hp;
            Atk += roleconfig.attack;
            Def += roleconfig.defense;
            Dodge += roleconfig.dodge;
            Crit += roleconfig.crit;
            Hit += roleconfig.hit;
            Tenacity += roleconfig.tenacity;
        }

        public void AppandEquipAttribute(EquipData equip)
        {
            var equipSet = new ShareCacheStruct<Config_Equip>();
            var weaponcfg = equipSet.Find(t => (t.EquipID == equip.ID && t.Grade == equip.Lv));
            if (weaponcfg == null)
            {
                TraceLog.WriteError("AppandEquipAttribute: No found equip! code={0}_{1}", equip.ID, equip.Lv);
                return;
            }

            Hp += weaponcfg.hp;
            Atk += weaponcfg.attack;
            Def += weaponcfg.defense;
            Dodge += weaponcfg.dodge;
            Crit += weaponcfg.crit;
            Hit += weaponcfg.hit;
            Tenacity += weaponcfg.tenacity;

            // 宝石属性
            var itemSet = new ShareCacheStruct<Config_Item>();
            if (equip.AtkGem != 0)
            {
                var gem = itemSet.FindKey(equip.AtkGem);
                if (gem != null)
                    Atk += gem.attack;
            }
            if (equip.DefGem != 0)
            {
                var gem = itemSet.FindKey(equip.DefGem);
                if (gem != null)
                    Def += gem.defense;
            }
            if (equip.HpGem != 0)
            {
                var gem = itemSet.FindKey(equip.HpGem);
                if (gem != null)
                    Hp += gem.hp;
            }
            if (equip.CritGem != 0)
            {
                var gem = itemSet.FindKey(equip.CritGem);
                if (gem != null)
                    Crit += gem.crit;
            }
            if (equip.HitGem != 0)
            {
                var gem = itemSet.FindKey(equip.HitGem);
                if (gem != null)
                    Hit += gem.hit;
            }
            if (equip.DodgeGem != 0)
            {
                var gem = itemSet.FindKey(equip.DodgeGem);
                if (gem != null)
                    Dodge += gem.dodge;
            }
            if (equip.TenacityGem != 0)
            {
                var gem = itemSet.FindKey(equip.TenacityGem);
                if (gem != null)
                    Tenacity += gem.tenacity;
            }
        }

        public void AppandSoulAttribute(UserSoulCache soul)
        {

            var soulSet = new ShareCacheStruct<Config_Soulstrong>();
            var list = soulSet.FindAll(t => (t.Soulid < soul.SoulID));
            foreach (var v in soul.OpenList)
            {
                list.Add(soulSet.FindKey(v));
            }

            foreach (var v in list)
            {
                switch (v.Attdef)
                {
                    case SoulAddType.Atk:
                        Atk += v.AddValue;
                        break;
                    case SoulAddType.Def:
                        Def += v.AddValue;
                        break;
                    case SoulAddType.Hp:
                        Hp += v.AddValue;
                        break;
                    case SoulAddType.Crit:
                        Crit += v.AddValue;
                        break;
                    case SoulAddType.Hit:
                        Hit += v.AddValue;
                        break;
                    case SoulAddType.Dodge:
                        Dodge += v.AddValue;
                        break;
                    case SoulAddType.Tenacity:
                        Tenacity += v.AddValue;
                        break;
                    case SoulAddType.Skill:
                        // GetSoul.Atk += soulcfg.AddValue;
                        break;
                }
            }

        }

        public void AppandElfAttribute(UserElfCache elf)
        {
            foreach (var v in elf.ElfList)
            {
                var elfcfg = new ShareCacheStruct<Config_Elves>().Find(t => (t.ElvesID == v.ID && t.ElvesGrade == v.Lv));
                if (elf == null)
                    continue;
                Hp += elfcfg.hp;
                Atk += elfcfg.attack;
                Def += elfcfg.defense;
            }
        }

        /// <summary>  
        /// 刷新计算战斗力
        /// </summary>  
        /// <returns></returns>  
        public void ConvertFightValue()
        {
            FightValue = Hp + Atk * 10 + Def * 10 + Dodge * 50 + Crit * 50 + Hit * 50 + Tenacity * 50;
        }
    }
}