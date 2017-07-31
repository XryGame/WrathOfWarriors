using GameServer.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPUserDetailsData
    {
        public JPUserDetailsData()
        {
            Friends = new JPFriendsData();
            GuildData = new JPGuildData();
            Enemys = new JPEnemysData();
            VitData = new JPVitData();
            RankAwardData = new JPRankAwardData();
        }
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int UserLv { get; set; }

        public int Diamond { get; set; }

        public int BuyDiamond { get; set; }

        public int UsedDiamond { get; set; }

        public string Gold { get; set; }

        public int VipLv { get; set; }

        public string AvatarUrl { get; set; }
        
        public int CombatRankID { get; set; }

        public int LevelRankID { get; set; }

        public int LotteryTimes { get; set; }

        public int SignStartID { get; set; }

        public int ShareCount { get; set; }

        public long ShareDate { get; set; }

        public int InviteCount { get; set; }

        public List<int> ReceiveInviteList { get; set; }

        public List<int> ReceiveLevelAwardList { get; set; }

        public List<int> ReceiveRankingAwardList { get; set; }

        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserPackageCache Package { get; set; }

        public UserSoulCache Soul { get; set; }

        public UserSkillCache Skill { get; set; }

        public UserTaskCache Task { get; set; }

        public UserAchievementCache Achievement { get; set; }

        public UserGuildCache Guild { get; set; }

        public JPGuildData GuildData { get; set; }

        public JPFriendsData Friends { get; set; }

        public UserMailBoxCache MailBox { get; set; }

        public UserEventAwardCache EventAward { get; set; }

        public UserPayCache Pay { get; set; }

        public UserCombatCache Combat { get; set; }

        public UserElfCache Elf { get; set; }

        public UserTransferItemCache Transfer { get; set; }

        public JPEnemysData Enemys { get; set; }

        public UserLotteryCache Lottery { get; set; }

        public JPVitData VitData { get; set; }

        public JPRankAwardData RankAwardData { get; set; }

        public long OfflineTimeSec { get; set; }

        public string OfflineEarnings { get; set; }

        public long LastMatchFightFailedDate { get; set; }

        public int ComboNum { get; set; }

        public long OpenServiceDateSec { get; set; }


    }
}
