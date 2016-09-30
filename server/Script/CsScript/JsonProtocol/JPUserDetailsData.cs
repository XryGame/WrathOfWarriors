using GameServer.Script.Model.Config;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPUserDetailsData
    {
        public JPUserDetailsData()
        {
            FriendList = new CacheList<JPFriendData>();
            FriendApplyList = new CacheList<JPFriendApplyData>();
            ClassMonitorData = new JPQueryClassMonitorData();
            DailyQuestData = new JPDailyQuestData();
            StudyData = new JPStudyData();
            ExerciseData = new JPExerciseData();
            EventAwardData = new JPEventAwardData();
        }
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int LooksId { get; set; }

        public int UserLv { get; set; }

        public int Diamond { get; set; }

        public int BaseExp { get; set; }

        public int FightExp { get; set; }

        public int Vit { get; set; }

        public int VipLv { get; set; }

        public string CreateDate { get; set; }

        public string LoginDate { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int HP { get; set; }

        public int FightValue { get; set; }

        public JPStudyData StudyData { get; set; }

        public JPExerciseData ExerciseData { get; set; }

        public int ClassID { get; set; }

        public object ExpData { get; set; }

        public int ExtendExp { get; set; }

        public CacheList<ItemData> ItemList { get; set; }

        public CacheList<SkillData> SkillList { get; set; }

        public CacheList<int> SkillCarryList { get; set; }

        public CacheList<JPFriendData> FriendList { get; set; }

        public CacheList<JPFriendApplyData> FriendApplyList { get; set; }
        
        public int GiveAwayCount { get; set; }

        public int ChallengeMonitorTimes { get; set; }

        public JPQueryClassMonitorData ClassMonitorData { get; set; }

        public JobTitleType AditionJobTitle { get; set; }

        public bool IsHaveJobTitle { get; set; }

        public JPDailyQuestData DailyQuestData { get; set; }

        public int CampaignTicketNum { get; set; }

        public bool IsCanReceiveAchievement { get; set; }

        public CacheList<int> UnlockSceneMapList { get; set; }

        public int SelectedSceneMapId { get; set; }

        public JPEventAwardData EventAwardData { get; set; }

        public CacheList<MailData> MailBox { get; set; }
    }
}
