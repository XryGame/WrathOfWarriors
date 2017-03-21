
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
        private int _hp;
        [EntityField("hp")]
        public int hp
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
        private int _attack;
        [EntityField("attack")]
        public int attack
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
        private int _defense;
        [EntityField("defense")]
        public int defense
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
        private int _dodge;
        [EntityField("dodge")]
        public int dodge
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
        private int _crit;
        [EntityField("crit")]
        public int crit
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
        private int _hit;
        [EntityField("hit")]
        public int hit
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
        private int _tenacity;
        [EntityField("tenacity")]
        public int tenacity
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
                        _hp = value.ToInt(); 
                        break;
                    case "attack":
                        _attack = value.ToInt();
                        break;
                    case "defense":
                        _defense = value.ToInt(); 
                        break; 
                    case "dodge":
                        _dodge = value.ToInt(); 
                        break;
                    case "crit":
                        _crit = value.ToInt();
                        break;
                    case "hit":
                        _hit = value.ToInt();
                        break;
                    case "tenacity":
                        _tenacity = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_RoleGrade index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion

	}
}