
using System;
using ProtoBuf;
using ZyGames.Framework.Common;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;

namespace GameServer.Script.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(AccessLevel.ReadOnly, DbConfig.Config)]
    public class Config_CelebrityRanking : ShareEntity
    {

        
        public Config_CelebrityRanking()
            : base(AccessLevel.ReadOnly)
        {
        }

        #region auto-generated Property

        /// <summary>
        /// id
        /// </summary>
        private int _ID;
        [EntityField("ID", IsKey = true)]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                SetChange("ID", value);
            }
        }

        /// <summary>
        /// 名次
        /// </summary>
        private int _Ranking;
        [EntityField("Ranking")]
        public int Ranking
        {
            get
            {
                return _Ranking;
            }
            set
            {
                SetChange("Ranking", value);
            }
        }

        /// <summary>
        /// 奖励数量
        /// </summary>
        private int _AwardNum;
        [EntityField("AwardNum")]
        public int AwardNum
        {
            get
            {
                return _AwardNum;
            }
            set
            {
                SetChange("AwardNum", value);
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
                    case "Ranking": return Ranking;
                    case "AwardNum": return AwardNum;
                    default: throw new ArgumentException(string.Format("Config_CelebrityRanking index[{0}] isn't exist.", index));
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
                    case "Ranking":
                        _Ranking = value.ToInt();
                        break;
                    case "AwardNum":
                        _AwardNum = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("Config_CelebrityRanking index[{0}] isn't exist.", index));
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