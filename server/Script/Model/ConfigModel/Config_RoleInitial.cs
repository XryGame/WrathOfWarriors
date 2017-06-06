
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_RoleInitial : ShareEntity
    {

        public Config_RoleInitial()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// 初始等级
        /// </summary>
        private int _Grade;
        [EntityField("Grade", IsKey = true)]
        public int Grade
        {
            get
            {
                return _Grade;
            }
            private set
            {
                SetChange("Grade", value);
            }
        }

        /// <summary>
        /// 初始生命
        /// </summary>
        private long _hp;
        [EntityField("hp")]
        public long hp
        {
            get
            {
                return _hp;
            }
            private set
            {
                SetChange("hp", value);
            }
        }

        /// <summary>
        /// 初始攻击
        /// </summary>
        private long _attack;
        [EntityField("attack")]
        public long attack
        {
            get
            {
                return _attack;
            }
            private set
            {
                SetChange("attack", value);
            }
        }

        /// <summary>
        /// 初始防御
        /// </summary>
        private long _defense;
        [EntityField("defense")]
        public long defense
        {
            get
            {
                return _defense;
            }
            private set
            {
                SetChange("defense", value);
            }
        }

        /// <summary>
        /// 初始闪避
        /// </summary>
        private long _dodge;
        [EntityField("dodge")]
        public long dodge
        {
            get
            {
                return _dodge;
            }
            private set
            {
                SetChange("dodge", value);
            }
        }

        /// <summary>
        /// 初始暴击
        /// </summary>
        private long _crit;
        [EntityField("crit")]
        public long crit
        {
            get
            {
                return _crit;
            }
            private set
            {
                SetChange("crit", value);
            }
        }

        /// <summary>
        /// 初始命中
        /// </summary>
        private long _hit;
        [EntityField("hit")]
        public long hit
        {
            get
            {
                return _hit;
            }
            private set
            {
                SetChange("hit", value);
            }
        }

        /// <summary>
        /// 初始韧性
        /// </summary>
        private long _tenacity;
        [EntityField("tenacity")]
        public long tenacity
        {
            get
            {
                return _tenacity;
            }
            private set
            {
                SetChange("tenacity", value);
            }
        }


        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "Grade": return Grade;
                    case "hp": return hp;
                    case "attack": return attack;
                    case "defense": return defense;
                    case "dodge": return dodge;
                    case "crit": return crit;
                    case "hit": return hit;
                    case "tenacity": return tenacity;
                    default: throw new ArgumentException(string.Format("Config_Role index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "Grade":
                        _Grade = value.ToInt(); 
                        break; 
                    case "hp":
                        _hp = value.ToLong(); 
                        break;
                    case "attack":
                        _attack = value.ToLong();
                        break;
                    case "defense":
                        _defense = value.ToLong(); 
                        break; 
                    case "dodge":
                        _dodge = value.ToLong(); 
                        break;
                    case "crit":
                        _crit = value.ToLong();
                        break;
                    case "hit":
                        _hit = value.ToLong();
                        break;
                    case "tenacity":
                        _tenacity = value.ToLong();
                        break;
                    default: throw new ArgumentException(string.Format("Config_RoleGrade index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}