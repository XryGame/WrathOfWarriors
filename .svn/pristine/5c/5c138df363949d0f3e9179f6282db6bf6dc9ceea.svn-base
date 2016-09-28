
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
    public class Config_Skill : ShareEntity
    {

        public Config_Skill()
            : base(AccessLevel.ReadOnly)
        {
        }
        
        #region auto-generated Property
        private int _ID;
        /// <summary>
        /// id
        /// </summary>
        [EntityField("ID", IsKey = true)]
        public int ID
        {
            get
            {
                return _ID;
            }
            private set
            {
                SetChange("ID", value);
            }
        }
        private string _Name;
        /// <summary>
        /// 技能名称
        /// </summary>
        [EntityField("Name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                SetChange("Name", value);
            }
        }
        private int _SkillLv;
        /// <summary>
        /// 技能初始等级
        /// </summary>
        [EntityField("SkillLv")]
        public int SkillLv
        {
            get
            {
                return _SkillLv;
            }
            private set
            {
                SetChange("SkillLv", value);
            }
        }
        private string _Effect;
        /// <summary>
        /// 技能特效
        /// </summary>
        [EntityField("Effect")]
        public string Effect
        {
            get
            {
                return _Effect;
            }
            private set
            {
                SetChange("Effect", value);
            }
        }
        private string _Rudio;
        /// <summary>
        /// 技能音效
        /// </summary>
        [EntityField("Rudio")]
        public string Rudio
        {
            get
            {
                return _Rudio;
            }
            private set
            {
                SetChange("Rudio", value);
            }
        }
        private string _Icon;
        /// <summary>
        /// 技能图标
        /// </summary>
        [EntityField("Icon")]
        public string Icon
        {
            get
            {
                return _Icon;
            }
            private set
            {
                SetChange("Icon", value);
            }
        }
        private int _GainProbability;
        /// <summary>
        /// 获得概率
        /// </summary>
        [EntityField("GainProbability")]
        public int GainProbability
        {
            get
            {
                return _GainProbability;
            }
            private set
            {
                SetChange("GainProbability", value);
            }
        }

   
        protected override object this[string index]
		{
			get
			{
                #region
				switch (index)
				{
                    case "ID": return ID;
                    case "Name": return Name;
                    case "SkillLv": return SkillLv;
                    case "Effect": return Effect;
                    case "Rudio": return Rudio;
                    case "Icon": return Icon;
                    case "GainProbability": return GainProbability;
                    default: throw new ArgumentException(string.Format("Config_Skill index[{0}] isn't exist.", index));
				}
                #endregion
			}
			set
			{
                #region
				switch (index)
				{
                    case "ID":
                        _ID = value.ToInt(); 
                        break; 
                    case "Name":
                        _Name = value.ToNotNullString(); 
                        break;
                    case "SkillLv":
                        _SkillLv = value.ToInt();
                        break;
                    case "Effect":
                        _Effect = value.ToNotNullString(); 
                        break; 
                    case "Rudio":
                        _Rudio = value.ToNotNullString();
                        break;
                    case "Icon":
                        _Icon = value.ToNotNullString();
                        break;
                    case "GainProbability":
                        _GainProbability = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_Skill index[{0}] isn't exist.", index));
				}
                #endregion
			}
		}
        
        #endregion
                
        protected override int GetIdentityId()
        {
            //allow modify return value
            return DefIdentityId;
        }
	}
}