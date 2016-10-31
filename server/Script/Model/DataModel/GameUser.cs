
using System;
using System.Data;
using System.Collections.Generic;
using ProtoBuf;
using ZyGames.Framework.Data;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Model;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;
using System.Linq;
using System.Reflection;

namespace GameServer.Script.Model.DataModel
{



    public delegate void AsyncDataChangeCallback(string property, int userId, object oldValue, object value);

    /// <summary>
    /// 
    /// </summary>
    [Serializable, ProtoContract, EntityTable(CacheType.Dictionary, DbConfig.Data)]
    public class GameUser : BaseUser
    {

        public static AsyncDataChangeCallback Callback { get; set; }

        public GameUser()
            : base(AccessLevel.ReadWrite)
        {
            IsRefreshing = true;
            ItemDataList = new CacheList<ItemData>();
            SkillDataList = new CacheList<SkillData>();
            SkillCarryList = new CacheList<int>();
            ClassData = new UserClassData();
            StudyTaskData = new UserStudyTaskData();
            ExerciseTaskData = new UserExerciseTaskData();
            ExpData = new UserExpData();
            CombatData = new UserCombatData();
            CombatLogList = new CacheList<CombatLogData>();
            FriendsData = new UserFriendsData();
            DailyQuestData = new UserDailyQuestData();
            AditionJobTitle = JobTitleType.No;
            OccupySceneList = new CacheList<SceneType>();
            AchievementList = new CacheList<AchievementData>();
            UnlockSceneMapList = new CacheList<int>();
            EventAwardData = new UserEventAwardData();
            MailBox = new CacheList<MailData>();
            ChallengeRoleList = new CacheList<int>();
        }
        public GameUser(int userid)
        : this()
        {
            UserID = userid;
        }

        #region auto-generated Property
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

        private int _EnterServerId;

        [ProtoMember(2)]
        [EntityField("EnterServerId")]
        public int EnterServerId
        {
            get
            {
                return _EnterServerId;
            }
            set
            {
                SetChange("EnterServerId", value);
            }
        }


        private string _RetailID;

        [ProtoMember(3)]
        [EntityField("RetailID")]
        public string RetailID
        {
            get
            {
                return _RetailID;
            }
            set
            {
                SetChange("RetailID", value);
            }
        }
        private string _Pid;
        [ProtoMember(4)]
        [EntityField("Pid")]
        public string Pid
        {
            get
            {
                return _Pid;
            }
            set
            {
                SetChange("Pid", value);
            }
        }

        private string _NickName;
        [ProtoMember(5)]
        [EntityField("NickName")]
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                SetChange("NickName", value);
            }
        }

        private short _UserLv;
        [ProtoMember(6)]
        [EntityField("UserLv")]
        public short UserLv
        {
            get
            {
                return _UserLv;
            }
            set
            {
                SetChange("UserLv", value);
            }
        }

        private SubjectStage _UserStage;
        [ProtoMember(7)]
        [EntityField("UserStage")]
        public SubjectStage UserStage
        {
            get
            {
                return _UserStage;
            }
            set
            {
                SetChange("UserStage", value);
            }
        }

        private int _GiveAwayDiamond;
        [ProtoMember(8)]
        [EntityField("GiveAwayDiamond")]
        public int GiveAwayDiamond
        {
            get
            {
                return _GiveAwayDiamond;
            }
            set
            {
                if (Callback != null && !IsRefreshing)
                {
                    Callback.BeginInvoke("DiamondChange", UserID, _GiveAwayDiamond, value, null, this);
                }
                SetChange("GiveAwayDiamond", value);
            }
        }
        private int _BuyDiamond;
        [ProtoMember(9)]
        [EntityField("BuyDiamond")]
        public int BuyDiamond
        {
            get
            {
                return _BuyDiamond;
            }
            set
            {
                if (Callback != null && !IsRefreshing)
                {
                    Callback.BeginInvoke("DiamondChange", UserID, _BuyDiamond, value, null, this);
                }
                SetChange("BuyDiamond", value);
            }
        }

        private int _UsedDiamond;
        [ProtoMember(10)]
        [EntityField("UsedDiamond")]
        public int UsedDiamond
        {
            get
            {
                return _UsedDiamond;
            }
            set
            {
                if (Callback != null && !IsRefreshing)
                {
                    Callback.BeginInvoke("DiamondChange", UserID, _UsedDiamond, value, null, this);
                }
                SetChange("UsedDiamond", value);
            }
        }

        private int _BaseExp;
        [ProtoMember(11)]
        [EntityField("BaseExp")]
        public int BaseExp
        {
            get
            {
                return _BaseExp;
            }
            set
            {
                SetChange("BaseExp", value);
            }
        }

        private int _FightExp;
        [ProtoMember(12)]
        [EntityField("FightExp")]
        public int FightExp
        {
            get
            {
                return _FightExp;
            }
            set
            {
                SetChange("FightExp", value);
            }
        }

        private int _Vit;
        [ProtoMember(13)]
        [EntityField("Vit")]
        public int Vit
        {
            get
            {
                return _Vit;
            }
            set
            {
                SetChange("Vit", value);
            }
        }

        private int _VipLv;
        [ProtoMember(14)]
        [EntityField("VipLv")]
        public int VipLv
        {
            get
            {
                return _VipLv;
            }
            set
            {
                SetChange("VipLv", value);
            }
        }
        private UserStatus _UserStatus;
        [ProtoMember(15)]
        [EntityField("UserStatus")]
        public UserStatus UserStatus
        {
            get
            {
                return _UserStatus;
            }
            set
            {
                SetChange("UserStatus", value);
            }
        }

        private int _FightingValue;
        [ProtoMember(16)]
        [EntityField("FightingValue")]
        public int FightingValue
        {
            get
            {
                return _FightingValue;
            }
            set
            {
                if (Callback != null && !IsRefreshing)
                {
                    Callback.BeginInvoke("FightValueChange", UserID, _FightingValue, value, null, this);
                }
                SetChange("FightingValue", value);
            }
        }

  
        
        private DateTime _CreateDate;
        [ProtoMember(17)]
        [EntityField("CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetChange("CreateDate", value);
            }
        }

        private DateTime _LoginDate;
        [ProtoMember(18)]
        [EntityField("LoginDate")]
        public DateTime LoginDate
        {
            get
            {
                return _LoginDate;
            }
            set
            {
                SetChange("LoginDate", value);
            }
        }

        private DateTime _OfflineDate;
        [ProtoMember(19)]
        [EntityField("OfflineDate")]
        public DateTime OfflineDate
        {
            get
            {
                return _OfflineDate;
            }
            set
            {
                SetChange("OfflineDate", value);
            }
        }

        #endregion

        private DateTime _RestoreDate;
        [ProtoMember(20)]
        [EntityField("RestoreDate")]
        public DateTime RestoreDate
        {
            get
            {
                return _RestoreDate;
            }
            set
            {
                SetChange("RestoreDate", value);
            }
        }


        private UserClassData _ClassData;
        [ProtoMember(21)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserClassData ClassData
        {
            get
            {
                return _ClassData;
            }
            set
            {
                SetChange("ClassData", value);
            }
        }

        private UserStudyTaskData _StudyTaskData;
        [ProtoMember(22)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserStudyTaskData StudyTaskData
        {
            get
            {
                return _StudyTaskData;
            }
            set
            {
                SetChange("StudyTaskData", value);
            }
        }

        private UserExerciseTaskData _ExerciseTaskData;
        [ProtoMember(23)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserExerciseTaskData ExerciseTaskData
        {
            get
            {
                return _ExerciseTaskData;
            }
            set
            {
                SetChange("ExerciseTaskData", value);
            }
        }
        private UserExpData _ExpData;
        [ProtoMember(24)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserExpData ExpData
        {
            get
            {
                return _ExpData;
            }
            set
            {
                SetChange("ExpData", value);
            }
        }
        
        //[ProtoMember(30)]
        //public CacheList<ItemData> ItemDataList { get; set; }

        /// <summary>
        /// 道具列表
        /// </summary>
        private CacheList<ItemData> _ItemDataList;
        [ProtoMember(25)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<ItemData> ItemDataList
        {
            get
            {
                return _ItemDataList;
            }
            set
            {
                SetChange("ItemDataList", value);
            }
        }

        //[ProtoMember(30)]
        //public CacheList<SkillData> SkillDataList { get; set; }
        /// <summary>
        /// 技能列表
        /// </summary>
        private CacheList<SkillData> _SkillDataList;
        [ProtoMember(26)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<SkillData> SkillDataList
        {
            get
            {
                return _SkillDataList;
            }
            set
            {
                SetChange("SkillDataList", value);
            }
        }

        private CacheList<int> _SkillCarryList;
        [ProtoMember(27)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<int> SkillCarryList
        {
            get
            {
                return _SkillCarryList;
            }
            set
            {
                SetChange("SkillCarryList", value);
            }
        }


        private UserCombatData _CombatData;
        [ProtoMember(28)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserCombatData CombatData
        {
            get
            {
                return _CombatData;
            }
            set
            {
                SetChange("CombatData", value);
            }
        }

        private CacheList<CombatLogData> _CombatLogList;
        [ProtoMember(29)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<CombatLogData> CombatLogList
        {
            get
            {
                return _CombatLogList;
            }
            set
            {
                SetChange("CombatLogList", value);
            }
        }

        private UserFriendsData _FriendsData;
        [ProtoMember(30)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserFriendsData FriendsData
        {
            get
            {
                return _FriendsData;
            }
            set
            {
                SetChange("FriendsData", value);
            }
        }

        /// <summary>
        /// 等级排名
        /// </summary>
        private int _LevelRankId;
        [ProtoMember(31)]
        [EntityField("LevelRankId")]
        public int LevelRankId
        {
            get
            {
                return _LevelRankId;
            }
            set
            {
                SetChange("LevelRankId", value);
            }
        }
        /// <summary>
        /// 战斗力排名
        /// </summary>
        private int _FightValueRankId;
        [ProtoMember(32)]
        [EntityField("FightValueRankId")]
        public int FightValueRankId
        {
            get
            {
                return _FightValueRankId;
            }
            set
            {
                SetChange("FightValueRankId", value);
            }
        }

        /// <summary>
        /// 形象ID
        /// </summary>
        private int _LooksId;
        [ProtoMember(33)]
        [EntityField("LooksId")]
        public int LooksId
        {
            get
            {
                return _LooksId;
            }
            set
            {
                SetChange("LooksId", value);
            }
        }


        /// <summary>
        /// 挑战班长有效次数
        /// </summary>
        private int _ChallengeMonitorTimes;
        [ProtoMember(34)]
        [EntityField("ChallengeMonitorTimes")]
        public int ChallengeMonitorTimes
        {
            get
            {
                return _ChallengeMonitorTimes;
            }
            set
            {
                SetChange("ChallengeMonitorTimes", value);
            }
        }

        /// <summary>
        /// 免费竞选票数
        /// </summary>
        private int _CampaignTicketNum;
        [ProtoMember(35)]
        [EntityField("CampaignTicketNum")]
        public int CampaignTicketNum
        {
            get
            {
                return _CampaignTicketNum;
            }
            set
            {
                SetChange("CampaignTicketNum", value);
            }
        }
        /// <summary>
        /// 购买的竞选票数
        /// </summary>
        private int _BuyCampaignTicketNum;
        [ProtoMember(36)]
        [EntityField("BuyCampaignTicketNum")]
        public int BuyCampaignTicketNum
        {
            get
            {
                return _BuyCampaignTicketNum;
            }
            set
            {
                SetChange("BuyCampaignTicketNum", value);
            }
        }

        /// <summary>
        /// 每日任务数据
        /// </summary>
        private UserDailyQuestData _DailyQuestData;
        [ProtoMember(37)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserDailyQuestData DailyQuestData
        {
            get
            {
                return _DailyQuestData;
            }
            set
            {
                SetChange("DailyQuestData", value);
            }
        }

        /// <summary>
        /// 占领挑战过的场景
        /// </summary>
        private CacheList<SceneType> _OccupySceneList;
        [ProtoMember(38)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<SceneType> OccupySceneList
        {
            get
            {
                return _OccupySceneList;
            }
            set
            {
                SetChange("OccupySceneList", value);
            }
        }



        /// <summary>
        /// 占领挑战过的场景
        /// </summary>
        private CacheList<AchievementData> _AchievementList;
        [ProtoMember(39)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<AchievementData> AchievementList
        {
            get
            {
                return _AchievementList;
            }
            set
            {
                SetChange("AchievementList", value);
            }
        }

        /// <summary>
        /// 解锁场景地图list
        /// </summary>
        private CacheList<int> _UnlockSceneMapList;
        [ProtoMember(40)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<int> UnlockSceneMapList
        {
            get
            {
                return _UnlockSceneMapList;
            }
            set
            {
                SetChange("UnlockSceneMapList", value);
            }
        }

        /// <summary>
        /// 选择默认的场景地图
        /// </summary>
        private int _SelectedSceneMapId;
        [ProtoMember(41)]
        [EntityField("SelectedSceneMapId")]
        public int SelectedSceneMapId
        {
            get
            {
                return _SelectedSceneMapId;
            }
            set
            {
                SetChange("SelectedSceneMapId", value);
            }
        }

        /// <summary>
        /// 邀请切磋目标Uid
        /// </summary>
        private int _InviteFightDestUid;
        [ProtoMember(42)]
        [EntityField("InviteFightDestUid")]
        public int InviteFightDestUid
        {
            get
            {
                return _InviteFightDestUid;
            }
            set
            {
                SetChange("InviteFightDestUid", value);
            }
        }

        /// <summary>
        /// 签到，首周，在线奖励数据
        /// </summary>
        private UserEventAwardData _EventAwardData;
        [ProtoMember(43)]
        [EntityField(true, ColumnDbType.LongText)]
        public UserEventAwardData EventAwardData
        {
            get
            {
                return _EventAwardData;
            }
            set
            {
                SetChange("EventAwardData", value);
            }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        private CacheList<MailData> _MailBox;
        [ProtoMember(44)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<MailData> MailBox
        {
            get
            {
                return _MailBox;
            }
            set
            {
                SetChange("MailBox", value);
            }
        }


        /// <summary>
        /// 当天是否抽奖
        /// </summary>
        private bool _IsTodayLottery;
        [ProtoMember(45)]
        [EntityField("IsTodayLottery")]
        public bool IsTodayLottery
        {
            get
            {
                return _IsTodayLottery;
            }
            set
            {
                SetChange("IsTodayLottery", value);
            }
        }


        /// <summary>
        /// 记录挑战过的角色
        /// </summary>
        private CacheList<int> _ChallengeRoleList;
        [ProtoMember(46)]
        [EntityField(true, ColumnDbType.LongText)]
        public CacheList<int> ChallengeRoleList
        {
            get
            {
                return _ChallengeRoleList;
            }
            set
            {
                SetChange("ChallengeRoleList", value);
            }
        }


        /// <summary>
        /// 扫荡的角色id
        /// </summary>
        private int _SweepingRoleId;
        [ProtoMember(47)]
        [EntityField("SweepingRoleId")]
        public int SweepingRoleId
        {
            get
            {
                return _SweepingRoleId;
            }
            set
            {
                SetChange("SweepingRoleId", value);
            }
        }

        /// <summary>
        /// 扫荡的次数
        /// </summary>
        private int _SweepTimes;
        [ProtoMember(48)]
        [EntityField("SweepTimes")]
        public int SweepTimes
        {
            get
            {
                return _SweepTimes;
            }
            set
            {
                SetChange("SweepTimes", value);
            }
        }

        /// <summary>
        /// 扫荡的开始时间
        /// </summary>
        private DateTime _StartSweepTime;
        [ProtoMember(49)]
        [EntityField("StartSweepTime")]
        public DateTime StartSweepTime
        {
            get
            {
                return _StartSweepTime;
            }
            set
            {
                SetChange("StartSweepTime", value);
            }
        }

        /// <summary>
        /// 剧情ID
        /// </summary>
        private int _PlotId;
        [ProtoMember(50)]
        [EntityField("PlotId")]
        public int PlotId
        {
            get
            {
                return _PlotId;
            }
            set
            {
                SetChange("PlotId", value);
            }
        }



        protected override int GetIdentityId()
        {
            //allow modify return value
            return UserID;
        }

        [ProtoMember(60)]
        public bool IsRefreshing { get; set; }

        private string _sessionID;
        public string SessionID
        {
            get
            {
                return _sessionID;
            }
            set
            {
                _sessionID = value;
            }
        }

        [ProtoMember(61)]
        public int GameId { get; set; }


        [ProtoMember(62)]
        public int ServerId { get; set; }


        [ProtoMember(63)]
        public bool IsOnline
        {
            get;
            set;
        }

        [ProtoMember(64)]
        public int Attack
        {
            get;
            set;
        }

        [ProtoMember(65)]
        public int Defense
        {
            get;
            set;
        }

        [ProtoMember(66)]
        public int Hp
        {
            get;
            set;
        }


        /// <summary>
        /// 聊天日期
        /// </summary>
        [ProtoMember(67)]
        public DateTime ChatDate
        {
            get;
            set;
        }

        /// <summary>
        ///  聊天版本
        /// </summary>
        private int _ChatVesion;
        [ProtoMember(68)]
        public int ChatVesion
        {
            get
            {
                return _ChatVesion;
            }
            set
            {
                _ChatVesion = value;
            }
        }

        /// <summary>
        /// 公告版本 
        /// </summary>
        [ProtoMember(69)]
        public int BroadcastVesion
        {
            get;
            set;
        }

        [ProtoMember(70)]
        public JobTitleType AditionJobTitle
        {
            get;
            set;
        }

        [ProtoMember(71)]
        public bool IsHaveJobTitle
        {
            get;
            set;
        }


        [ProtoMember(72)]
        public SceneType OccupySceneType
        {
            get;
            set;
        }


        /// <summary>
        /// 随机的抽奖id
        /// </summary>
        [ProtoMember(73)]
        public int RandomLotteryId
        {
            get;
            set;
        }

        public override string GetNickName()
        {
            return NickName;
        }

        public override string GetPassportId()
        {
            return Pid;
        }

        public override string GetRetailId()
        {
            return RetailID;
        }

        public override int GetUserId()
        {
            return UserID.ToInt();
        }

        public override bool IsLock
        {
            get { return UserStatus == UserStatus.Lock; }
        }


        public static bool IsNickName(string name)
        {
            bool bl = false;
            bl = new PersonalCacheStruct<GameUser>().IsExist(u => u.NickName.ToLower() == name.ToLower().Trim());
            if (!bl)
            {
                var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);

                var command = dbProvider.CreateCommandStruct("GameUser", CommandMode.Inquiry, "NickName");
                command.Filter = dbProvider.CreateCommandFilter();
                command.Filter.Condition = command.Filter.FormatExpression("NickName");
                command.Filter.AddParam("NickName", name);
                command.Parser();
                using (var reader = dbProvider.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
                {
                    while (reader.Read())
                    {
                        bl = true;
                    }
                }
            }

            return bl;
        }


        protected override object this[string index]
        {
            get
            {
                #region
                switch (index)
                {
                    case "UserID": return UserID;
                    case "EnterServerId": return EnterServerId;
                    case "RetailID": return RetailID;
                    case "Pid": return Pid;
                    case "NickName": return NickName;
                    case "UserLv": return UserLv;
                    case "UserStage": return UserStage;
                    case "GiveAwayDiamond": return GiveAwayDiamond;
                    case "BuyDiamond": return BuyDiamond;
                    case "UsedDiamond": return UsedDiamond;
                    case "BaseExp": return BaseExp;
                    case "FightExp": return FightExp;
                    case "Vit": return Vit;
                    case "VipLv": return VipLv;
                    case "UserStatus": return UserStatus;
                    case "FightingValue": return FightingValue;
                    case "CreateDate": return CreateDate;
                    case "LoginDate": return LoginDate;
                    case "OfflineDate": return OfflineDate;
                    case "SessionID": return SessionID;
                    case "RestoreDate": return RestoreDate;
                    case "StudyTaskData": return StudyTaskData;
                    case "ClassData": return ClassData;
                    case "ExerciseTaskData": return ExerciseTaskData;
                    case "ExpData": return ExpData;
                    case "ItemDataList": return ItemDataList;
                    case "SkillDataList": return SkillDataList;
                    case "SkillCarryList": return SkillCarryList;
                    case "CombatData": return CombatData;
                    case "CombatLogList": return CombatLogList;
                    case "FriendsData": return FriendsData;
                    case "LevelRankId": return LevelRankId;
                    case "FightValueRankId": return FightValueRankId;
                    case "LooksId": return LooksId;
                    case "ChallengeMonitorTimes": return ChallengeMonitorTimes;
                    case "CampaignTicketNum": return CampaignTicketNum;
                    case "BuyCampaignTicketNum": return BuyCampaignTicketNum;
                    case "DailyQuestData": return DailyQuestData;
                    case "OccupySceneList": return OccupySceneList;
                    case "AchievementList": return AchievementList;
                    case "UnlockSceneMapList": return UnlockSceneMapList;
                    case "SelectedSceneMapId": return SelectedSceneMapId;
                    case "InviteFightDestUid": return InviteFightDestUid;
                    case "EventAwardData": return EventAwardData;
                    case "MailBox": return MailBox;
                    case "IsTodayLottery": return IsTodayLottery;
                    case "ChallengeRoleList": return ChallengeRoleList;
                    case "SweepingRoleId": return SweepingRoleId;
                    case "SweepTimes": return SweepTimes;
                    case "StartSweepTime": return StartSweepTime;
                    case "PlotId": return PlotId;
                    default: throw new ArgumentException(string.Format("GameUser index[{0}] isn't exist.", index));
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
                    case "EnterServerId":
                        _EnterServerId = value.ToInt();
                        break;
                    case "RetailID":
                        _RetailID = value.ToNotNullString();
                        break;
                    case "Pid":
                        _Pid = value.ToNotNullString();
                        break;
                    case "NickName":
                        _NickName = value.ToNotNullString();
                        break;
                    case "UserLv":
                        _UserLv = value.ToShort();
                        break;
                    case "UserStage":
                        _UserStage = value.ToEnum<SubjectStage>();
                        break;
                    case "GiveAwayDiamond":
                        _GiveAwayDiamond = value.ToInt();
                        break;
                    case "BuyDiamond":
                        _BuyDiamond = value.ToInt();
                        break;
                    case "UsedDiamond":
                        _UsedDiamond = value.ToInt();
                        break;
                    case "BaseExp":
                        _BaseExp = value.ToInt();
                        break;
                    case "FightExp":
                        _FightExp = value.ToInt();
                        break;
                    case "Vit":
                        _Vit = value.ToInt();
                        break;
                    case "VipLv":
                        _VipLv = value.ToInt();
                        break;
                    case "UserStatus":
                        _UserStatus = value.ToEnum<UserStatus>();
                        break;
                    case "FightingValue":
                        _FightingValue = value.ToInt();
                        break;
                    case "CreateDate":
                        _CreateDate = value.ToDateTime();
                        break;
                    case "LoginDate":
                        _LoginDate = value.ToDateTime();
                        break;
                    case "OfflineDate":
                        _OfflineDate = value.ToDateTime();
                        break;
                    case "RestoreDate":
                        _RestoreDate = value.ToDateTime();
                        break;
                    case "ClassData":
                        _ClassData = ConvertCustomField<UserClassData>(value, index);
                        break;
                    case "StudyTaskData":
                        _StudyTaskData = ConvertCustomField<UserStudyTaskData>(value, index);
                        break;
                    case "ExerciseTaskData":
                        _ExerciseTaskData = ConvertCustomField<UserExerciseTaskData>(value, index);
                        break;
                    case "ExpData":
                        _ExpData = ConvertCustomField<UserExpData>(value, index);
                        break;
                    case "ItemDataList":
                        _ItemDataList = ConvertCustomField<CacheList<ItemData>>(value, index);
                        break;
                    case "SkillDataList":
                        _SkillDataList = ConvertCustomField<CacheList<SkillData>>(value, index);
                        break;
                    case "SkillCarryList":
                        _SkillCarryList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "CombatData":
                        _CombatData = ConvertCustomField<UserCombatData>(value, index);
                        break;
                    case "CombatLogList":
                        _CombatLogList = ConvertCustomField<CacheList<CombatLogData>>(value, index);
                        break;
                    case "FriendsData":
                        _FriendsData = ConvertCustomField<UserFriendsData>(value, index);
                        break;
                    case "LevelRankId":
                        _LevelRankId = value.ToInt();
                        break;
                    case "FightValueRankId":
                        _FightValueRankId = value.ToInt();
                        break;
                    case "LooksId":
                        _LooksId = value.ToInt();
                        break;
                    case "ChallengeMonitorTimes":
                        _ChallengeMonitorTimes = value.ToInt();
                        break;
                    case "CampaignTicketNum":
                        _CampaignTicketNum = value.ToInt();
                        break;
                    case "BuyCampaignTicketNum":
                        _BuyCampaignTicketNum = value.ToInt();
                        break;
                    case "DailyQuestData":
                        _DailyQuestData = ConvertCustomField<UserDailyQuestData>(value, index);
                        break;
                    case "OccupySceneList":
                        _OccupySceneList = ConvertCustomField<CacheList<SceneType>>(value, index);
                        break;
                    case "AchievementList":
                        _AchievementList = ConvertCustomField<CacheList<AchievementData>>(value, index);
                        break;
                    case "UnlockSceneMapList":
                        _UnlockSceneMapList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "SelectedSceneMapId":
                        _SelectedSceneMapId = value.ToInt();
                        break;
                    case "InviteFightDestUid":
                        _InviteFightDestUid = value.ToInt();
                        break;
                    case "EventAwardData":
                        _EventAwardData = ConvertCustomField<UserEventAwardData>(value, index);
                        break;
                    case "MailBox":
                        _MailBox = ConvertCustomField<CacheList<MailData>>(value, index);
                        break;
                    case "IsTodayLottery":
                        _IsTodayLottery = value.ToBool();
                        break;
                    case "ChallengeRoleList":
                        _ChallengeRoleList = ConvertCustomField<CacheList<int>>(value, index);
                        break;
                    case "SweepingRoleId":
                        _SweepingRoleId = value.ToInt();
                        break;
                    case "SweepTimes":
                        _SweepTimes = value.ToInt();
                        break;
                    case "StartSweepTime":
                        _StartSweepTime = value.ToDateTime();
                        break;
                    case "PlotId":
                        _PlotId = value.ToInt();
                        break;
                    default: throw new ArgumentException(string.Format("GameUser index[{0}] isn't exist.", index));
                }
                #endregion
            }
        }



        public int DiamondNum
        {
            get
            {
                var val = MathUtils.Addition(GiveAwayDiamond, BuyDiamond, int.MaxValue);
                val = MathUtils.Subtraction(val, UsedDiamond, 0);
                return val;
            }
        }


        public ItemData findItem(int id)
        {
            return ItemDataList.Find(t => (t.ID == id));
        }

        public SkillData findSkill(int id)
        {
            return SkillDataList.Find(t => (t.ID == id));
        }

        public MailData findMail(string id)
        {
            return MailBox.Find(t => (t.ID == id));
        }


        public SubjectStage getSubjectStage()
        {
            if (UserLv <= 13) return SubjectStage.PrimarySchool;
            if (UserLv <= 19) return SubjectStage.MiddleSchool;
            if (UserLv <= 25) return SubjectStage.SeniorHighSchool;
            if (UserLv <= 33) return SubjectStage.University;
            throw new Exception("getSubjectStage:" + UserLv + " is not exist");
        }

        /// <summary>  
        /// 根据经验值获得用户等级
        /// </summary>  
        /// <returns></returns>  
        public short ConvertExp2Level()
        {
            var roleGradeCache = new ShareCacheStruct<Config_RoleGrade>();
            List<Config_RoleGrade> list = roleGradeCache.FindAll(t => (t.BaseExp <= BaseExp && t.FightExp <= FightExp));

            if (list.Count > 0)
            {
                Config_RoleGrade findv = roleGradeCache.FindKey(list[list.Count - 1].ID + 1);
                if (findv != null)
                    return (short)findv.ID;
                else
                    return (short)list[list.Count - 1].ID;
            }
            else
            {
                return 1;
            }
        }


        /// <summary>  
        /// 获得当前阶段BaseExp
        /// </summary>  
        /// <returns></returns>  
        public int GetCurrentStageBaseExpValue(SubjectType type)
        {
            int ret = 0;
            foreach (Config_SubjectExp se in (type == SubjectType.Study ? DataHelper.StudyExplist : DataHelper.ExerciseExplist))
            {
                switch (se.id)
                {
                    case SubjectID.id1:
                        ret = MathUtils.Addition(ret, ExpData.id1, int.MaxValue);
                        break;
                    case SubjectID.id2:
                        ret = MathUtils.Addition(ret, ExpData.id2, int.MaxValue);
                        break;
                    case SubjectID.id3:
                        ret = MathUtils.Addition(ret, ExpData.id3, int.MaxValue);
                        break;
                    case SubjectID.id4:
                        ret = MathUtils.Addition(ret, ExpData.id4, int.MaxValue);
                        break;
                    case SubjectID.id5:
                        ret = MathUtils.Addition(ret, ExpData.id5, int.MaxValue);
                        break;
                    case SubjectID.id6:
                        ret = MathUtils.Addition(ret, ExpData.id6, int.MaxValue);
                        break;
                    case SubjectID.id7:
                        ret = MathUtils.Addition(ret, ExpData.id7, int.MaxValue);
                        break;
                    case SubjectID.id8:
                        ret = MathUtils.Addition(ret, ExpData.id8, int.MaxValue);
                        break;
                    case SubjectID.id9:
                        ret = MathUtils.Addition(ret, ExpData.id9, int.MaxValue);
                        break;
                    case SubjectID.id10:
                        ret = MathUtils.Addition(ret, ExpData.id10, int.MaxValue);
                        break;
                    case SubjectID.id11:
                        ret = MathUtils.Addition(ret, ExpData.id11, int.MaxValue);
                        break;
                    case SubjectID.id12:
                        ret = MathUtils.Addition(ret, ExpData.id12, int.MaxValue);
                        break;
                    case SubjectID.id13:
                        ret = MathUtils.Addition(ret, ExpData.id13, int.MaxValue);
                        break;
                    case SubjectID.id14:
                        ret = MathUtils.Addition(ret, ExpData.id14, int.MaxValue);
                        break;
                    case SubjectID.id15:
                        ret = MathUtils.Addition(ret, ExpData.id15, int.MaxValue);
                        break;
                    case SubjectID.id16:
                        ret = MathUtils.Addition(ret, ExpData.id16, int.MaxValue);
                        break;
                    case SubjectID.id17:
                        ret = MathUtils.Addition(ret, ExpData.id17, int.MaxValue);
                        break;
                    case SubjectID.id18:
                        ret = MathUtils.Addition(ret, ExpData.id18, int.MaxValue);
                        break;
                    case SubjectID.id19:
                        ret = MathUtils.Addition(ret, ExpData.id19, int.MaxValue);
                        break;
                    case SubjectID.id20:
                        ret = MathUtils.Addition(ret, ExpData.id20, int.MaxValue);
                        break;
                    case SubjectID.id21:
                        ret = MathUtils.Addition(ret, ExpData.id21, int.MaxValue);
                        break;
                    case SubjectID.id22:
                        ret = MathUtils.Addition(ret, ExpData.id22, int.MaxValue);
                        break;
                    case SubjectID.id23:
                        ret = MathUtils.Addition(ret, ExpData.id23, int.MaxValue);
                        break;
                    case SubjectID.id24:
                        ret = MathUtils.Addition(ret, ExpData.id24, int.MaxValue);
                        break;
                    case SubjectID.id25:
                        ret = MathUtils.Addition(ret, ExpData.id25, int.MaxValue);
                        break;
                    case SubjectID.id26:
                        ret = MathUtils.Addition(ret, ExpData.id26, int.MaxValue);
                        break;
                    case SubjectID.id27:
                        ret = MathUtils.Addition(ret, ExpData.id27, int.MaxValue);
                        break;
                    case SubjectID.id28:
                        ret = MathUtils.Addition(ret, ExpData.id28, int.MaxValue);
                        break;
                    case SubjectID.id29:
                        ret = MathUtils.Addition(ret, ExpData.id29, int.MaxValue);
                        break;
                    case SubjectID.id30:
                        ret = MathUtils.Addition(ret, ExpData.id30, int.MaxValue);
                        break;
                    case SubjectID.id31:
                        ret = MathUtils.Addition(ret, ExpData.id31, int.MaxValue);
                        break;
                    case SubjectID.id32:
                        ret = MathUtils.Addition(ret, ExpData.id32, int.MaxValue);
                        break;
                    case SubjectID.id33:
                        ret = MathUtils.Addition(ret, ExpData.id33, int.MaxValue);
                        break;
                    case SubjectID.id34:
                        ret = MathUtils.Addition(ret, ExpData.id34, int.MaxValue);
                        break;
                    case SubjectID.id35:
                        ret = MathUtils.Addition(ret, ExpData.id35, int.MaxValue);
                        break;
                    case SubjectID.id36:
                        ret = MathUtils.Addition(ret, ExpData.id36, int.MaxValue);
                        break;
                    default: throw new ArgumentException(string.Format("Config_SubjectExp id[{0}] isn't exist.", se.id));
                }

            }
            return ret;
        }

        public int GetSumBaseExp()
        {
            int studyexp = GetCurrentStageBaseExpValue(SubjectType.Study);
            int exerciseexp = GetCurrentStageBaseExpValue(SubjectType.Exercise);

            int sumexp = MathUtils.Addition(studyexp, exerciseexp, int.MaxValue);
            sumexp = MathUtils.Addition(sumexp, ExpData.Ext, int.MaxValue);

            return sumexp;
        }

        /// <summary>  
        /// 添加BaseExp
        /// </summary>  
        /// <returns>增加经验值</returns>  
        public int AdditionBaseExpValue(SubjectID sid, int count)
        {
            int maxaddv = 0;
            int sumexp = GetSumBaseExp();
            int lv = ConvertExp2Level();
            Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(lv);
            Config_RoleGrade lastgrade = null;
            if (lv > 1)
            {
                lastgrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(lv - 1);
            }

            if (rolegrade == null)
                return 0;

            if (lastgrade == null)
            {
                maxaddv = MathUtils.Subtraction(rolegrade.BaseExp, sumexp, 0);
            }
            else
            {
                maxaddv = MathUtils.Subtraction(rolegrade.BaseExp, lastgrade.BaseExp, 0);
                maxaddv = MathUtils.Subtraction(maxaddv, sumexp, 0);
            }

            if (maxaddv == 0)
                return 0;


            Config_SubjectExp subjectExp = new ShareCacheStruct<Config_SubjectExp>().FindKey(sid);

            if (subjectExp == null)
                throw new Exception("AdditionBaseExpValue:" + sid + " is not exist");
            
            int predictaddexpv = AdditionExpValue(subjectExp.UnitExp * count);

            switch (sid)
            {
                case SubjectID.id1:
                    ExpData.id1 = MathUtils.Addition(ExpData.id1, predictaddexpv, ExpData.id1 + maxaddv);
                    break;
                case SubjectID.id2:
                    ExpData.id2 = MathUtils.Addition(ExpData.id2, predictaddexpv, ExpData.id2 + maxaddv);
                    break;
                case SubjectID.id3:
                    ExpData.id3 = MathUtils.Addition(ExpData.id3, predictaddexpv, ExpData.id3 + maxaddv);
                    break;
                case SubjectID.id4:
                    ExpData.id4 = MathUtils.Addition(ExpData.id4, predictaddexpv, ExpData.id4 + maxaddv);
                    break;
                case SubjectID.id5:
                    ExpData.id5 = MathUtils.Addition(ExpData.id5, predictaddexpv, ExpData.id5 + maxaddv);
                    break;
                case SubjectID.id6:
                    ExpData.id6 = MathUtils.Addition(ExpData.id6, predictaddexpv, ExpData.id3 + maxaddv);
                    break;
                case SubjectID.id7:
                    ExpData.id7 = MathUtils.Addition(ExpData.id7, predictaddexpv, ExpData.id7 + maxaddv);
                    break;
                case SubjectID.id8:
                    ExpData.id8 = MathUtils.Addition(ExpData.id8, predictaddexpv, ExpData.id8 + maxaddv);
                    break;
                case SubjectID.id9:
                    ExpData.id9 = MathUtils.Addition(ExpData.id9, predictaddexpv, ExpData.id9 + maxaddv);
                    break;
                case SubjectID.id10:
                    ExpData.id10 = MathUtils.Addition(ExpData.id10, predictaddexpv, ExpData.id10 + maxaddv);
                    break;
                case SubjectID.id11:
                    ExpData.id11 = MathUtils.Addition(ExpData.id11, predictaddexpv, ExpData.id11 + maxaddv);
                    break;
                case SubjectID.id12:
                    ExpData.id12 = MathUtils.Addition(ExpData.id12, predictaddexpv, ExpData.id12 + maxaddv);
                    break;
                case SubjectID.id13:
                    ExpData.id13 = MathUtils.Addition(ExpData.id13, predictaddexpv, ExpData.id13 + maxaddv);
                    break;
                case SubjectID.id14:
                    ExpData.id14 = MathUtils.Addition(ExpData.id14, predictaddexpv, ExpData.id14 + maxaddv);
                    break;
                case SubjectID.id15:
                    ExpData.id15 = MathUtils.Addition(ExpData.id15, predictaddexpv, ExpData.id15 + maxaddv);
                    break;
                case SubjectID.id16:
                    ExpData.id16 = MathUtils.Addition(ExpData.id16, predictaddexpv, ExpData.id16 + maxaddv);
                    break;
                case SubjectID.id17:
                    ExpData.id17 = MathUtils.Addition(ExpData.id17, predictaddexpv, ExpData.id17 + maxaddv);
                    break;
                case SubjectID.id18:
                    ExpData.id18 = MathUtils.Addition(ExpData.id18, predictaddexpv, ExpData.id18 + maxaddv);
                    break;
                case SubjectID.id19:
                    ExpData.id19 = MathUtils.Addition(ExpData.id19, predictaddexpv, ExpData.id19 + maxaddv);
                    break;
                case SubjectID.id20:
                    ExpData.id20 = MathUtils.Addition(ExpData.id20, predictaddexpv, ExpData.id20 + maxaddv);
                    break;
                case SubjectID.id21:
                    ExpData.id21 = MathUtils.Addition(ExpData.id21, predictaddexpv, ExpData.id21 + maxaddv);
                    break;
                case SubjectID.id22:
                    ExpData.id22 = MathUtils.Addition(ExpData.id22, predictaddexpv, ExpData.id22 + maxaddv);
                    break;
                case SubjectID.id23:
                    ExpData.id23 = MathUtils.Addition(ExpData.id23, predictaddexpv, ExpData.id23 + maxaddv);
                    break;
                case SubjectID.id24:
                    ExpData.id24 = MathUtils.Addition(ExpData.id24, predictaddexpv, ExpData.id24 + maxaddv);
                    break;
                case SubjectID.id25:
                    ExpData.id25 = MathUtils.Addition(ExpData.id25, predictaddexpv, ExpData.id25 + maxaddv);
                    break;
                case SubjectID.id26:
                    ExpData.id26 = MathUtils.Addition(ExpData.id26, predictaddexpv, ExpData.id26 + maxaddv);
                    break;
                case SubjectID.id27:
                    ExpData.id27 = MathUtils.Addition(ExpData.id27, predictaddexpv, ExpData.id27 + maxaddv);
                    break;
                case SubjectID.id28:
                    ExpData.id28 = MathUtils.Addition(ExpData.id28, predictaddexpv, ExpData.id28 + maxaddv);
                    break;
                case SubjectID.id29:
                    ExpData.id29 = MathUtils.Addition(ExpData.id29, predictaddexpv, ExpData.id29 + maxaddv);
                    break;
                case SubjectID.id30:
                    ExpData.id30 = MathUtils.Addition(ExpData.id30, predictaddexpv, ExpData.id30 + maxaddv);
                    break;
                case SubjectID.id31:
                    ExpData.id31 = MathUtils.Addition(ExpData.id31, predictaddexpv, ExpData.id31 + maxaddv);
                    break;
                case SubjectID.id32:
                    ExpData.id32 = MathUtils.Addition(ExpData.id32, predictaddexpv, ExpData.id32 + maxaddv);
                    break;
                case SubjectID.id33:
                    ExpData.id33 = MathUtils.Addition(ExpData.id33, predictaddexpv, ExpData.id33 + maxaddv);
                    break;
                case SubjectID.id34:
                    ExpData.id34 = MathUtils.Addition(ExpData.id34, predictaddexpv, ExpData.id34 + maxaddv);
                    break;
                case SubjectID.id35:
                    ExpData.id35 = MathUtils.Addition(ExpData.id35, predictaddexpv, ExpData.id35 + maxaddv);
                    break;
                case SubjectID.id36:
                    ExpData.id36 = MathUtils.Addition(ExpData.id36, predictaddexpv, ExpData.id36 + maxaddv);
                    break;
                default: throw new ArgumentException(string.Format("Config_SubjectExp id[{0}] isn't exist.", sid));
            }

            int addvalue = Math.Min(subjectExp.UnitExp * count, maxaddv);
            BaseExp = MathUtils.Addition(BaseExp, addvalue, int.MaxValue);

            short currlv = ConvertExp2Level();
            if (currlv > UserLv)
            {
                UserLv = currlv;
                UserLevelUp();
            }

            return addvalue;
        }
        /// <summary>  
        /// 添加BaseExp
        /// </summary>  
        /// <returns>增加扩展经验值</returns>  
        public int AdditionBaseExtExpValue(int expvalue)
        {
            int maxaddv = 0;
            int sumexp = GetSumBaseExp();
            int lv = ConvertExp2Level();
            Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(lv);
            Config_RoleGrade lastgrade = null;
            if (lv > 1)
            {
                lastgrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(lv - 1);
            }

            if (rolegrade == null)
                return 0;

            if (lastgrade == null)
            {
                maxaddv = MathUtils.Subtraction(rolegrade.BaseExp, sumexp, 0);
            }
            else
            {
                maxaddv = MathUtils.Subtraction(rolegrade.BaseExp, lastgrade.BaseExp, 0);
                maxaddv = MathUtils.Subtraction(maxaddv, sumexp, 0);
            }

            if (maxaddv == 0)
                return 0;
            
            ExpData.Ext = MathUtils.Addition(ExpData.id1, expvalue, ExpData.id1 + maxaddv);


            int addvalue = Math.Min(expvalue, maxaddv);
            BaseExp = MathUtils.Addition(BaseExp, addvalue, int.MaxValue);

            short currlv = ConvertExp2Level();
            if (currlv > UserLv)
            {
                UserLv = currlv;
                UserLevelUp();
            }

            return addvalue;
        }

        public int AdditionFightExpValue(int expvalue)
        {
            Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(UserLv);
            if (rolegrade == null)
            {
                return 0;
            }

            int addvalue = AdditionExpValue(expvalue);
            addvalue = Math.Min(addvalue, rolegrade.FightExp - FightExp);


            FightExp = MathUtils.Addition(FightExp, addvalue, int.MaxValue);
            short currlv = ConvertExp2Level();
            if (currlv > UserLv)
            {
                UserLv = currlv;
                UserLevelUp();
            }

            return addvalue;
        }

        /// <summary>  
        /// 用户升级处理
        /// </summary>  
        /// <returns></returns>  
        public void UserLevelUp()
        {
            UserExpData data = new UserExpData();
            ExpData = data;
            RefreshFightValue();
            

            if (Callback != null && !IsRefreshing)
            {
                Callback.BeginInvoke("LevelUp", UserID, 0, 0, null, this);
            }


        }


        public void ResultStudyTask()
        {
            StudyTaskData.SubjectID = SubjectID.id0;
            StudyTaskData.StartTime = DateTime.MinValue;
            StudyTaskData.Count = 0;
            StudyTaskData.SceneId = SceneType.No;
        }

        public void ResultExerciseTask()
        {
            ExerciseTaskData.SubjectID = SubjectID.id0;
            ExerciseTaskData.StartTime = DateTime.MinValue;
            ExerciseTaskData.Count = 0;
            ExerciseTaskData.SceneId = SceneType.No;
        }

        /// <summary>
        /// 随机道具
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<int> RandItem(int count)
        {
            List<int> list = new List<int>();
            List<Config_Scene> scenes = new ShareCacheStruct<Config_Scene>().FindAll(t => (t.ClearGrade <= UserLv));
            if (scenes.Count == 0)
            {
                return list;
            }
            Random random = new Random();
            List<Config_Item> itemslist = new List<Config_Item>();
            foreach (Config_Scene sc in scenes)
            {
                List<Config_Item> items = new ShareCacheStruct<Config_Item>().FindAll(
                    t => (t.Type == ItemType.Item) && t.Map == (int)sc.ID
                    );
                itemslist.AddRange(items);
            }

            // 如果拥有道具达到最高等级，排除抽奖
            foreach (var v in ItemDataList)
            {
                var ritem = itemslist.Find(t => (t.ID == v.ID));
                if (ritem != null && v.Num >= GetItemLvMax(v.ID))
                {
                    itemslist.Remove(ritem);
                }
            }

            int itemprob = 0;
            foreach (Config_Item it in itemslist)
            {
                itemprob += it.GainProbability;
            }

            for (int i = 0; i < count && itemprob > 0; ++i)
            {
                int randv = random.Next(itemprob);
                int prob = 0;
                foreach (Config_Item it in itemslist)
                {
                    prob += it.GainProbability;
                    if (randv < prob)
                    {
                        list.Add(it.ID);

                        UserAddItem(it.ID, 1);
                        break;
                    }
                }
            }
            return list;
        }

        public List<int> RandSkillBook(int count)
        {
            List<int> list = new List<int>();
            List<Config_Scene> scenes = new ShareCacheStruct<Config_Scene>().FindAll(t => (t.ClearGrade <= UserLv));
            if (scenes.Count == 0)
            {
                return list;
            }
            Random random = new Random();
            //List<Config_Skill> skilllist = new ShareCacheStruct<Config_Skill>().FindAll();
            List<Config_Item> skilllist = new List<Config_Item>();
            foreach (Config_Scene sc in scenes)
            {
                List<Config_Item> items = new ShareCacheStruct<Config_Item>().FindAll(t => (t.Type == ItemType.Skill));
                skilllist.AddRange(items);
            }

            // 如果拥有技能达到最高等级，排除抽奖
            foreach (var v in SkillDataList)
            {
                var rskill = skilllist.Find(t => (t.ID == v.ID));
                if (rskill != null && v.Lv >= GetSkillLvMax(v.ID))
                {
                    skilllist.Remove(rskill);
                }
            }


            int skillprob = 0;
            foreach (Config_Item sk in skilllist)
            {
                skillprob += sk.GainProbability;
            }

            for (int i = 0; i < count && skillprob > 0; ++i)
            {
                int randv = random.Next(skillprob);
                int prob = 0;
                foreach (Config_Item sk in skilllist)
                {
                    prob += sk.GainProbability;
                    if (randv < prob)
                    {
                        list.Add(sk.ID);

                        UserAddItem(sk.ID, 1);
                        CheckAddSkillBook(sk.ID, 1);

                        break;
                    }
                }
            }
            return list;
        }

        public int GetItemLvMax(int id)
        {
            var list = new ShareCacheStruct<Config_ItemGrade>().FindAll(t => (t.ItemID == id));
            if (list.Count > 0)
            {
                return list[list.Count - 1].ItemLv;
            }
            return 0;
        }

        public int GetSkillLvMax(int id)
        {
            var list = new ShareCacheStruct<Config_SkillGrade>().FindAll(t => (t.SkillID == id));
            if (list.Count > 0)
            {
                return list[list.Count - 1].SkillLv;
            }
            return 0;
        }


        public int TotalExp
        {
            get { return MathUtils.Addition(BaseExp, FightExp, int.MaxValue); }
        }

        /// <summary>  
        /// 用户获得道具
        /// </summary>  
        /// <returns></returns>  
        public void UserAddItem(int id, int num)
        {
            ItemData item = ItemDataList.Find(t => (t.ID == id));
            if (item == null)
            {
                item = new ItemData();
                item.ID = id;
                item.Num = MathUtils.Addition(item.Num, num);
                ItemDataList.Add(item);
            }
            else
            {
                item.Num = MathUtils.Addition(item.Num, num);
            }
        }

        /// <summary>  
        /// 用户获得技能书
        /// </summary>  
        /// <returns></returns>  
        public void UserAddSkill(int id, int ownnum)
        {
            SkillData skill = SkillDataList.Find(t => (t.ID == id));
            bool isnew = false;
            if (skill == null)
            {
                skill = new SkillData();
                skill.ID = id;
                skill.Lv = 1;
                SkillDataList.Add(skill);

                isnew = true;
            }

            var sgcache = new ShareCacheStruct<Config_SkillGrade>();
            Config_SkillGrade sg = sgcache.Find(t => (t.SkillID == id && t.SkillLv == skill.Lv));
            if (sg == null)
            {
                new BaseLog().SaveLog(string.Format("SkillGrade table error SkillID={0}", id));
                return;
            }
            if (isnew)
            {
                ItemData item = ItemDataList.Find(t => (t.ID == sg.Condition));
                if (item != null)
                {
                    item.Num = 0;
                }
            }
            else
            {
                while (sg != null && ownnum >= sg.Number)
                {
                    Config_SkillGrade sgnext = sgcache.Find(t => (t.SkillID == id && t.SkillLv == skill.Lv + 1));
                    if (sgnext != null)
                    {
                        skill.Lv = skill.Lv + 1;
                        ItemData item = ItemDataList.Find(t => (t.ID == sg.Condition));
                        if (item != null)
                        {
                            item.Num = MathUtils.Subtraction(item.Num, sg.Number, 0);
                            ownnum = item.Num;
                        }
                    }
                    sg = sgnext;
                }
            }

            // 成就
            AchievementData achdata = AchievementList.Find(t => (t.Type == AchievementType.SkillLevelMax));
            if (achdata != null && achdata.ID != 0 && !achdata.IsFinish)
            {
                if (skill.Lv > achdata.Count)
                    achdata.Count = skill.Lv;
                var achconfig = new ShareCacheStruct<Config_Achievement>().FindKey(achdata.ID);
                if (achdata.Count >= achconfig.ObjectiveNum)
                {
                    achdata.IsFinish = true;
                    if (Callback != null && !IsRefreshing)
                    {
                        Callback.BeginInvoke("SkillLevelAchievement", UserID, 0, achdata.ID, null, this);
                    }
                }
            }
        }


        public void CheckAddSkillBook(int itemId, int num)
        {
            ItemData itemdata = findItem(itemId);
            Config_SkillGrade sg = new ShareCacheStruct<Config_SkillGrade>().Find(t => (t.Condition == itemId));
            if (sg != null && itemdata != null)
            {
                UserAddSkill(sg.SkillID, num);
            }
        }

        public int AdditionExpValue(int expvalue)
        {
            float fpredictaddexpv = expvalue;
            int baseaddv = 0;
            // 职称加成
            if (AditionJobTitle != JobTitleType.No)
            {
                switch (AditionJobTitle)
                {
                    case JobTitleType.Class:
                        baseaddv = 5;
                        break;
                    case JobTitleType.Sports:
                        baseaddv = 15;
                        break;
                    case JobTitleType.Art:
                        baseaddv = 15;
                        break;
                    case JobTitleType.Publicity:
                        baseaddv = 25;
                        break;
                    case JobTitleType.Study:
                        baseaddv = 25;
                        break;
                    case JobTitleType.Secretary:
                        baseaddv = 35;
                        break;
                    case JobTitleType.Leader:
                        baseaddv = 45;
                        break;
                }
                if (IsHaveJobTitle)
                    baseaddv += 5;
            }
            // 占领加成
            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            var scenes = new ShareCacheStruct<Config_Scene>();
            for (SceneType i = SceneType.Piazza; i <= SceneType.MusicHall; ++i)
            {
                var os = occupycache.FindKey(i);
                if (os == null)
                    continue;
                if (os.UserId == UserID)
                {
                    var scene = scenes.FindKey(i);
                    if (scene == null)
                        continue;
                    baseaddv += scene.OccupyAdd % 100;
                }
            }


            fpredictaddexpv = MathUtils.Addition(fpredictaddexpv, fpredictaddexpv / 100 * baseaddv);
            return fpredictaddexpv.ToInt();
        }
        public void PushCombatLog(ref CombatLogData log)
        {
            if (CombatLogList.Count >= DataHelper.CombatLogCountMax)
            {
                CombatLogList.RemoveAt(0);
            }

            CombatLogList.Add(log);
        }

        /// <summary>
        /// 刷新战斗力
        /// </summary>
        public void RefreshFightValue()
        {
            Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(UserLv);
            if (rolegrade == null)
            {
                return;
            }
            var sctcache = new ShareCacheStruct<Config_SubjectExp>();
            /// 如果是学前班阶段，按照小学阶段算
            SubjectStage stage = UserStage == SubjectStage.PreschoolSchool ? SubjectStage.PrimarySchool : UserStage;
            Attack = 0;
            Defense = 0;
            Hp = 0;


            Type type = ExpData.GetType();
            float percent = 0f;
            int hp_ = 0;
            int atk_ = 0;
            int def_ = 0;
            int exp_ = 0;
            // 生命
            List<SubjectChildType> HpList = new List<SubjectChildType>();
            HpList.Add(SubjectChildType.Yingyu);
            HpList.Add(SubjectChildType.Tiyu);
            HpList.Add(SubjectChildType.Daolaji);
            HpList.Add(SubjectChildType.Jiaohua);
            foreach (var v in HpList)
            {
                if (sctcache.Find(t => (t.SubType == v && t.Stage == stage)) == null)
                {
                    continue;
                }
                hp_ = 0;
                exp_ = 0;
                var list = sctcache.FindAll(t => (t.SubType == v && t.Stage == stage));

                foreach (var sub in list)
                {
                    string propertyName = string.Format("{0}", sub.id);
                    PropertyInfo pi = type.GetProperties().FirstOrDefault(x => x.Name == propertyName);
                    if (pi != null)
                        exp_ += pi.GetValue(ExpData).ToInt();
                }
                float factor = 0.5f;
                percent = (float)exp_ / rolegrade.BaseExp;
                hp_ += (int)(rolegrade.HP * (1 + percent) * factor);
                Hp += hp_;
            }
            // 攻击
            List<SubjectChildType> AtkList = new List<SubjectChildType>();
            AtkList.Add(SubjectChildType.Shuxue);
            AtkList.Add(SubjectChildType.Lizong);
            AtkList.Add(SubjectChildType.Saodi);
            foreach (var v in AtkList)
            {
                if (sctcache.Find(t => (t.SubType == v && t.Stage == stage)) == null)
                {
                    continue;
                }
                atk_ = 0;
                exp_ = 0;
                var list = new ShareCacheStruct<Config_SubjectExp>().FindAll(t => (t.SubType == v && t.Stage == stage));

                foreach (var sub in list)
                {
                    string propertyName = string.Format("{0}", sub.id);
                    PropertyInfo pi = type.GetProperties().FirstOrDefault(x => x.Name == propertyName);
                    if (pi != null)
                        exp_ += pi.GetValue(ExpData).ToInt();
                }
                float factor = v == SubjectChildType.Saodi ? 0.4f : 0.6f;
                percent = (float)exp_ / rolegrade.BaseExp;
                atk_ += (int)(rolegrade.Attack * (1 + percent) * factor);
                Attack += atk_;
            }

            // 防御
            List<SubjectChildType> DefList = new List<SubjectChildType>();
            DefList.Add(SubjectChildType.Yuwen);
            DefList.Add(SubjectChildType.Wenzong);
            DefList.Add(SubjectChildType.Cazhuozi);
            foreach (var v in DefList)
            {
                if (sctcache.Find(t => (t.SubType == v && t.Stage == stage)) == null)
                {
                    continue;
                }
                def_ = 0;
                exp_ = 0;
                var list = new ShareCacheStruct<Config_SubjectExp>().FindAll(t => (t.SubType == v && t.Stage == stage));

                foreach (var sub in list)
                {
                    string propertyName = string.Format("{0}", sub.id);
                    PropertyInfo pi = type.GetProperties().FirstOrDefault(x => x.Name == propertyName);
                    if (pi != null)
                        exp_ += pi.GetValue(ExpData).ToInt();
                }
                float factor = v == SubjectChildType.Cazhuozi ? 0.6f : 0.4f;
                percent = (float)exp_ / rolegrade.BaseExp;
                def_ += (int)(rolegrade.Defense * (1 + percent) * factor);
                Defense += def_;
            }

            FightingValue = Attack * 5 + Defense * 5 + Hp;
        }

        /// <summary>
        /// 获得竞技场加成后的战斗力
        /// </summary>
        public int GetCombatFightValue()
        {
  
            Attack = 0;
            Defense = 0;
            Hp = 0;

            var list = ItemDataList.FindAll(t => (t.ID >= 10028 && t.ID <= 10033));
            foreach (var item in list)
            {
                Config_Item cfgitem = new ShareCacheStruct<Config_Item>().Find(t => (t.ID == item.ID));
                if (cfgitem.Type == ItemType.Item)
                {
                    List<Config_ItemGrade> itemgradelist = new ShareCacheStruct<Config_ItemGrade>().FindAll(t => (t.ID == item.ID));
                    if (itemgradelist.Count > 0)
                    {
                        Attack += itemgradelist[itemgradelist.Count - 1].Attack;
                        Defense += itemgradelist[itemgradelist.Count - 1].Defense;
                        Hp += itemgradelist[itemgradelist.Count - 1].HP;
                    }
                }
            }

            return FightingValue + (Attack * 5 + Defense * 5 + Hp);
        }

        /// <summary>
        /// 是否好友人数满了
        /// </summary>
        /// <returns></returns>
        public bool IsFriendNumFull()
        {
            return FriendsData.FriendsList.Count >= DataHelper.FriendCountMax;
        }

        /// <summary>
        /// 是否有此好友
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFriend(int uid)
        {
            return FriendsData.FriendsList.Find(t => (t.UserId == uid)) != null;
        }

        /// <summary>
        /// 是否有此好友申请
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFriendApply(int uid)
        {
            return FriendsData.ApplyList.Find(t => (t.UserId == uid)) != null;
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <returns></returns>
        public void AddFriend(int uid)
        {
            FriendData fd = FriendsData.FriendsList.Find(t => (t.UserId == uid));
            if (fd == null)
            {
                fd = new FriendData()
                {
                    UserId = uid,
                    IsGiveAway = false
                };
                FriendsData.FriendsList.Add(fd);
            }
            
        }

        /// <summary>
        /// 查找好友
        /// </summary>
        /// <returns></returns>
        public FriendData FindFriend(int uid)
        {
            return FriendsData.FriendsList.Find(t => (t.UserId == uid));
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <returns></returns>
        public void RemoveFriend(int uid)
        {
            FriendData fd = FriendsData.FriendsList.Find(t => (t.UserId == uid));
            if (fd != null)
                FriendsData.FriendsList.Remove(fd);
        }

        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <returns></returns>
        public void AddFriendApply(int uid)
        {
            if (FriendsData.ApplyList.Count >= DataHelper.FriendApplyCountMax)
            {
                FriendsData.ApplyList.RemoveAt(FriendsData.ApplyList.Count - 1);
            }
            FriendApplyData apply = new FriendApplyData()
            {
                UserId = uid,
                ApplyDate = DateTime.Now
            };
            FriendsData.ApplyList.Add(apply);
        }

        /// <summary>
        /// 查找好友申请
        /// </summary>
        /// <returns></returns>
        public FriendApplyData FindFriendApply(int uid)
        {
            return FriendsData.ApplyList.Find(t => (t.UserId == uid));
        }

        /// <summary>
        /// 是否有对方的馈赠
        /// </summary>
        /// <returns></returns>
        public bool IsHaveFriendGiveAway(int uid)
        {
            FriendData fd = FindFriend(uid);
            return fd != null && FindFriend(uid).IsByGiveAway;
        }


        public void NextDailyQuest()
        {
            DailyQuestData.ID = TaskType.No;
            DailyQuestData.IsFinish = false;

            if (DailyQuestData.FinishCount >= 3)
                return;

            List<Config_Task> tasklist = new ShareCacheStruct<Config_Task>().FindAll();
            if (tasklist.Count > 0)
            {
                Random random = new Random();

                DailyQuestData.RefreshCount += 1;

                int randv = random.Next(tasklist.Count);
                var randtask = tasklist[randv];
                DailyQuestData.ID = randtask.id;
            }
            else
            {
                new BaseLog("NextDailyQuest").SaveFatalLog(string.Format("Task list is null!!!"));
            }
        }


        public void AddNewMail(ref MailData mail)
        {
            if (mail == null)
                return;

            MailBox.Add(mail);
            if (MailBox.Count > DataHelper.MaxMailNum)
            {
                MailData removemail = MailBox[0];
                if (removemail.ApppendDiamond > 0)
                {
                    GiveAwayDiamond = MathUtils.Addition(GiveAwayDiamond, removemail.ApppendDiamond, int.MaxValue / 2);
                }
                MailBox.Remove(removemail);
            }
            if (Callback != null && !IsRefreshing)
            {
                Callback.BeginInvoke("NewMail", UserID, 0, mail.ID, null, this);
            }
        }
    }
}