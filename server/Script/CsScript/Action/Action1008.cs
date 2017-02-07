using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1008_用户角色详情接口
    /// </summary>
    public class Action1008 : BaseAction
    {
        private JPUserDetailsData receipt;
        private Random random = new Random();
        public Action1008(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1008, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1008;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            //GameUser gameUser = UserHelper.FindUser(UserId);
            //if (gameUser == null)
            //{
            //    ErrorInfo = Language.Instance.NoFoundUser;
            //    return true;
            //}
            //ContextUser.UserLv = ContextUser.ConvertExp2Level();
            //Config_RoleGrade rolegrade = new ShareCacheStruct<Config_RoleGrade>().FindKey(ContextUser.UserLv);
            //if (rolegrade == null)
            //{
            //    ErrorInfo = string.Format(Language.Instance.DBTableError, "RoleGrade");
            //    return true;
            //}
            //ContextUser.RefreshFightValue();
            short convertlv = ContextUser.ConvertExp2Level();
            if (convertlv != ContextUser.UserLv)
            {
                ContextUser.UserLv = convertlv;
                ContextUser.RefreshFightValue();
            }
                
            receipt = new JPUserDetailsData()
            {
                UserId = ContextUser.UserID,
                NickName = ContextUser.NickName,
                LooksId = ContextUser.LooksId,
                UserLv = ContextUser.UserLv,
                Diamond = ContextUser.DiamondNum,
                BuyDiamond = ContextUser.BuyDiamond,
                BaseExp = ContextUser.BaseExp,
                FightExp = ContextUser.FightExp,
                Vit = ContextUser.Vit,
                //VipLv = ContextUser.VipLv,
                CreateDate = ContextUser.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                LoginDate = ContextUser.LoginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Attack = ContextUser.Attack,
                Defense = ContextUser.Defense,
                HP = ContextUser.Hp,
                FightValue = ContextUser.FightingValue,
                ChallengeMonitorTimes = ContextUser.ChallengeMonitorTimes,
                InviteFightDiamondNum = ContextUser.InviteFightDiamondNum
            };
            

            receipt.StudyData.SubjectID = ContextUser.StudyTaskData.SubjectID;
            receipt.StudyData.StartTime = Util.ConvertDateTimeStamp(ContextUser.StudyTaskData.StartTime);
            receipt.StudyData.Count = ContextUser.StudyTaskData.Count;
            receipt.StudyData.SceneId = ContextUser.StudyTaskData.SceneId;

            receipt.ExerciseData.SubjectID = ContextUser.ExerciseTaskData.SubjectID;
            receipt.ExerciseData.StartTime = Util.ConvertDateTimeStamp(ContextUser.ExerciseTaskData.StartTime);
            receipt.ExerciseData.Count = ContextUser.ExerciseTaskData.Count;
            receipt.ExerciseData.SceneId = ContextUser.ExerciseTaskData.SceneId;

            SubjectStage stage = ContextUser.getSubjectStage();
            switch (stage)
            {
                case SubjectStage.PrimarySchool:
                    receipt.ExpData = new JPExpPrimarySchoolData()
                    {
                        id1 = ContextUser.ExpData.id1,
                        id2 = ContextUser.ExpData.id2,
                        id3 = ContextUser.ExpData.id3,
                        id4 = ContextUser.ExpData.id4,
                        id5 = ContextUser.ExpData.id5,
                        id6 = ContextUser.ExpData.id6,
                    };
                    break;
                case SubjectStage.MiddleSchool:
                    receipt.ExpData = new JPExpMiddleSchoolData()
                    {
                        id7 = ContextUser.ExpData.id7,
                        id8 = ContextUser.ExpData.id8,
                        id9 = ContextUser.ExpData.id9,
                        id10 = ContextUser.ExpData.id10,
                        id11 = ContextUser.ExpData.id11,
                        id12 = ContextUser.ExpData.id12,
                        id13 = ContextUser.ExpData.id13,
                        id14 = ContextUser.ExpData.id14,
                        id15 = ContextUser.ExpData.id15,
                        id16 = ContextUser.ExpData.id16,
                    };
                    break;
                case SubjectStage.SeniorHighSchool:
                    receipt.ExpData = new JPExpSeniorHighSchoolData()
                    {
                        id17 = ContextUser.ExpData.id17,
                        id18 = ContextUser.ExpData.id18,
                        id19 = ContextUser.ExpData.id19,
                        id20 = ContextUser.ExpData.id20,
                        id21 = ContextUser.ExpData.id21,
                        id22 = ContextUser.ExpData.id22,
                        id23 = ContextUser.ExpData.id23,
                        id24 = ContextUser.ExpData.id24,
                        id25 = ContextUser.ExpData.id25,
                        id26 = ContextUser.ExpData.id26,
                    };
                    break;
                case SubjectStage.University:
                    receipt.ExpData = new JPExpUniversityData()
                    {
                        id27 = ContextUser.ExpData.id27,
                        id28 = ContextUser.ExpData.id28,
                        id29 = ContextUser.ExpData.id29,
                        id30 = ContextUser.ExpData.id30,
                        id31 = ContextUser.ExpData.id31,
                        id32 = ContextUser.ExpData.id32,
                        id33 = ContextUser.ExpData.id33,
                        id34 = ContextUser.ExpData.id34,
                        id35 = ContextUser.ExpData.id35,
                        id36 = ContextUser.ExpData.id36,
                    };
                    break;
                default: throw new ArgumentException(string.Format("Action1008 stage[{0}] isn't exist.", stage));
            }
            receipt.ExtendExp = ContextUser.ExpData.Ext;

            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;
            receipt.SkillCarryList = ContextUser.SkillCarryList;

            // 好友信息
            foreach (FriendData fd in ContextUser.FriendsData.FriendsList)
            {
                GameUser friend = UserHelper.FindUser(fd.UserId);
                if (friend != null)
                {
                    JPFriendData jpfd = new JPFriendData();
                    jpfd.UserId = friend.UserID;
                    jpfd.NickName = friend.NickName;
                    jpfd.LooksId = friend.LooksId;
                    jpfd.UserLv = friend.UserLv;
                    jpfd.VipLv = friend.VipLv;
                    jpfd.FightValue = friend.FightingValue;
                    GameSession fsession = GameSession.Get(friend.UserID);
                    if (fsession != null && fsession.Connected)
                        jpfd.IsOnline = true;
                    jpfd.IsGiveAway = fd.IsGiveAway;
                    jpfd.IsByGiveAway = fd.IsByGiveAway;
                    jpfd.IsReceiveGiveAway = fd.IsReceiveGiveAway;
                    receipt.FriendList.Add(jpfd);
                }
            }
            foreach (FriendApplyData apply in ContextUser.FriendsData.ApplyList)
            {
                GameUser applyuser = UserHelper.FindUser(apply.UserId);
                if (applyuser != null)
                {
                    JPFriendApplyData data = new JPFriendApplyData();
                    data.UserId = applyuser.UserID;
                    data.NickName = applyuser.NickName;
                    data.LooksId = applyuser.LooksId;
                    data.UserLv = applyuser.UserLv;
                    data.VipLv = applyuser.VipLv;
                    data.FightValue = applyuser.FightingValue;
                    GameSession asession = GameSession.Get(applyuser.UserID);
                    if (asession != null && asession.Connected)
                        data.IsOnline = true;
                    receipt.FriendApplyList.Add(data);
                }
            }
            
            receipt.GiveAwayCount = ContextUser.FriendsData.GiveAwayCount;

            if (ContextUser.ClassData.ClassID != 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
                if (classdata == null || classdata.MemberList.Find(t => (t == ContextUser.UserID)) == 0)
                {
                    ContextUser.ClassData.ClassID = 0;
                }
            }
            receipt.ClassID = ContextUser.ClassData.ClassID;

            if (ContextUser.ClassData.ClassID != 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
                if (classdata != null)
                {
                    GameUser monitor = UserHelper.FindUser(classdata.Monitor);
                    if (monitor != null)
                    {
                        receipt.ClassMonitorData.UserId = monitor.UserID;
                        receipt.ClassMonitorData.NickName = monitor.NickName;
                        receipt.ClassMonitorData.LooksId = monitor.LooksId;
                        receipt.ClassMonitorData.FightValue = monitor.FightingValue;
                        receipt.ClassMonitorData.SkillCarryList = monitor.SkillCarryList;
                    }
                }
                var jobcache = new ShareCacheStruct<JobTitleDataCache>();
                for (JobTitleType i = JobTitleType.Class; i <= JobTitleType.Leader; ++i)
                {
                    var jobdata = jobcache.FindKey(i);
                    if (jobdata == null)
                        continue;
                    if (jobdata.Status != CampaignStatus.Runing && jobdata.ClassId == ContextUser.ClassData.ClassID)
                    {
                        ContextUser.AditionJobTitle = i;
                        if (jobdata.UserId == ContextUser.UserID)
                        {
                            ContextUser.IsHaveJobTitle = true;
                        }
                        break;
                    }
                }
            }


            //if (ContextUser.AditionJobTitle != JobTitleType.No)
            //{
                receipt.JobTitleAddValue = ContextUser.JobTitleAdditionValue();
            //}
            //receipt.AditionJobTitle = ContextUser.AditionJobTitle;
            //receipt.IsHaveJobTitle = ContextUser.IsHaveJobTitle;
            ContextUser.OccupyAddList.Clear();
            var occupycache = new ShareCacheStruct<OccupyDataCache>();
            for (SceneType i = SceneType.Piazza; i <= SceneType.MusicHall; ++i)
            {
                var os = occupycache.FindKey(i);
                if (os == null)
                    continue;

                if (ContextUser.ClassData.ClassID != 0)
                {
                    var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
                    if (classdata != null)
                    {
                        if (classdata.MemberList.Find(t => (t == os.UserId)) != 0)
                        {
                            ContextUser.OccupyAddList.Add(i);
                        }
                    }
                }
            }
            
            receipt.OccupyAddList = ContextUser.OccupyAddList;

            receipt.DailyQuestData.ID = ContextUser.DailyQuestData.ID;
            receipt.DailyQuestData.IsFinish = ContextUser.DailyQuestData.IsFinish;
            receipt.DailyQuestData.RefreshCount = ContextUser.DailyQuestData.RefreshCount;
            receipt.DailyQuestData.FinishCount = ContextUser.DailyQuestData.FinishCount;
            receipt.DailyQuestData.Count = ContextUser.DailyQuestData.Count;

            receipt.CampaignTicketNum = ContextUser.CampaignTicketNum;
            
            foreach (var ach in ContextUser.AchievementList)
            {
                if (ach.IsFinish && !ach.IsReceive)
                {
                    receipt.IsCanReceiveAchievement = true;
                    break;
                }
            }

            receipt.UnlockSceneMapList = ContextUser.UnlockSceneMapList;
            receipt.SelectedSceneMapId = ContextUser.SelectedSceneMapId;



            receipt.EventAwardData.SignCount = ContextUser.EventAwardData.SignCount;
            receipt.EventAwardData.IsTodaySign = ContextUser.EventAwardData.IsTodaySign;
            receipt.EventAwardData.FirstWeekCount = ContextUser.EventAwardData.FirstWeekCount;
            receipt.EventAwardData.IsTodayReceiveFirstWeek = ContextUser.EventAwardData.IsTodayReceiveFirstWeek;
            receipt.EventAwardData.OnlineStartTime = Util.ConvertDateTimeStamp(ContextUser.EventAwardData.OnlineStartTime);
            receipt.EventAwardData.OnlineAwardId = ContextUser.EventAwardData.OnlineAwardId;


            receipt.MailBox = ContextUser.MailBox;

            receipt.IsTodayLottery = ContextUser.IsTodayLottery;
            //if (!ContextUser.IsTodayLottery && ContextUser.RandomLotteryId == 0)
            //{
            //    var lottery = UserHelper.RandomLottery(ContextUser.UserID, ContextUser.UserLv);
            //    if (lottery != null)
            //    {
            //        ContextUser.RandomLotteryId = lottery.ID;
            //        receipt.LotteryAwardType = lottery.Type;
            //        receipt.LotteryId = lottery.Content;
            //    }
            //}
            //else
            //{
            //    var lottery = new ShareCacheStruct<Config_Lottery>().FindKey(ContextUser.RandomLotteryId);
            //    if (lottery != null)
            //    {
            //        receipt.LotteryAwardType = lottery.Type;
            //        receipt.LotteryId = lottery.Content;
            //    }

            //}
            

            // 支付模块
            UserPayCache userpay = UserHelper.FindUserPay(ContextUser.UserID);
            if (userpay == null)
            {
                userpay = new UserPayCache()
                {
                    UserID = ContextUser.UserID,
                    PayMoney = 0,
                    IsReceiveFirstPay = false,
                    WeekCardDays = -1,
                    MonthCardDays = -1,
                    WeekCardAwardDate = DateTime.Now,
                    MonthCardAwardDate = DateTime.Now,
                };
                var payCacheSet = new PersonalCacheStruct<UserPayCache>();
                payCacheSet.Add(userpay);
                payCacheSet.Update();
            }
                
            if (userpay.PayMoney > 0 && !userpay.IsReceiveFirstPay)
            {
                receipt.IsCanReceiveFirstPay = true;
            }
             
            //ContextUser.VipLv = userpay.ConvertPayVipLevel();
            receipt.VipLv = ContextUser.VipLv;
            receipt.PayMoney = userpay.PayMoney;

            receipt.WeekCardDays = userpay.WeekCardDays;
            receipt.MonthCardDays = userpay.MonthCardDays;

            receipt.ChallengeRoleList = ContextUser.ChallengeRoleList;
            receipt.SweepingRoleId = ContextUser.SweepingRoleId;
            receipt.SweepTimes = ContextUser.SweepTimes;
            receipt.StartSweepTime = Util.ConvertDateTimeStamp(ContextUser.StartSweepTime);

            receipt.PlotId = ContextUser.PlotId;

            receipt.BuyVitCount = ContextUser.BuyVitCount;
            receipt.ReceiveVitStatus = ContextUser.ReceiveVitStatus;

            receipt.AccumulatePayList = ContextUser.AccumulatePayList;

            receipt.CombatData = ContextUser.CombatData;

            receipt.VipGiftProgress = ContextUser.VipGiftProgress;

            var cacache = new ShareCacheStruct<CompetitionApply>();
            var findv = cacache.FindKey(ContextUser.UserID);
            if (findv != null)
            {
                receipt.IsCompetitionAply = true;
            }
            receipt.IsReceivedRedPacket = ContextUser.IsReceivedRedPacket;
            return true;
        }




        public override void TakeActionAffter(bool state)
        {

            for (ChatType type = ChatType.World; type < ChatType.System; ++type)
            {
                var parameters = new Parameters();
                parameters["ChatType"] = type;
                var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, Current, parameters, OpCode.Text, null);
                ActionFactory.SendAction(Current, ActionIDDefine.Cst_Action3002, packet, (session, asyncResult) => { }, 0);
            }
            string context = "";
            RankType ranktype = RankType.No;
            int rankid = 0;
            //UserRank combatrank = UserHelper.FindCombatRankUser(ContextUser.UserID);
            //UserRank fightvaluerank = UserHelper.FindFightValueRankUser(ContextUser.UserID);
            UserRank levelrank = UserHelper.FindLevelRankUser(ContextUser.UserID);
            
            if (ContextUser.CombatData.RankID != 0 && ContextUser.CombatData.RankID < 10)
            {
                ranktype = RankType.Combat;
                rankid = ContextUser.CombatData.RankID;
            }
            if (levelrank != null && levelrank.RankId != 0 && levelrank.RankId < 10)
            {
                if (ranktype == RankType.No || levelrank.RankId < rankid)
                {
                    ranktype = RankType.Level;
                    rankid = levelrank.RankId;
                }
            }

            if (ranktype != RankType.No)
            {
                switch (ranktype)
                {
                    case RankType.Combat:
                        context = string.Format("名人榜排名第{0}名的 {1} 上线了！", rankid, ContextUser.NickName);
                        break;
                    case RankType.Level:
                        context = string.Format("排行榜排名第{0}名的 {1} 上线了！", rankid, ContextUser.NickName);
                        break;
                    //case RankType.FightValue:
                    //    context = string.Format("战斗力排行第 {0} 名的 {1} 上线了！", rankid, ContextUser.NickName);
                    //    break;
                }

                if (ContextUser.UserLv >= DataHelper.OpenRankSystemUserLevel)
                {
                    PushMessageHelper.SendNoticeToOnlineUser(NoticeType.Game, context);

                    var chatService = new TryXChatService();
                    chatService.SystemSend(context);
                    PushMessageHelper.SendSystemChatToOnlineUser();
                }

            }

            // 通知好友上线
            //foreach (FriendData fd in ContextUser.FriendsData.FriendsList)
            //{
            //    GameSession session = GameSession.Get(fd.UserId);
            //    if (session != null)
            //    {
            //        PushMessageHelper.FriendOnlineNotification(session, ContextUser.UserID);
            //    }
            //}
            context = "欢迎进入创想学院！";
            var chatServices = new TryXChatService();
            chatServices.SystemSendWhisper(ContextUser, context);
            PushMessageHelper.SendSystemChatToUser(Current);

            ContextUser.IsRefreshing = false;

            base.TakeActionAffter(state);
        }
    }
}