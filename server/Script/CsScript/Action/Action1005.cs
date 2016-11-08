using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.LogModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1005_创建角色
    /// </summary>
    public class Action1005 : RegisterAction
    {
        private int looksid;
        public Action1005(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1005, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        protected override bool GetActionParam()
        {
            if (httpGet.GetInt("LooksId", ref looksid))
            {
                return true;
            }
            return false;
        }

        protected override bool CreateUserRole(out IUser user)
        {
            user = null;
            GameUser gameUser = UserHelper.FindUser(UserId);
            if (gameUser == null)
            {
                var roleFunc = new RoleFunc();
                string msg;

                if (roleFunc.VerifyRange(UserName, out msg) ||
                    roleFunc.VerifyKeyword(UserName, out msg) ||
                    roleFunc.IsExistNickName(UserName, out msg))
                {
                    ErrorCode = Language.Instance.ErrorCode;
                    ErrorInfo = msg;
                    return false;
                }
                gameUser = CreateRole();
                roleFunc.OnCreateAfter(gameUser);
            }
            else
            {
                return false;
            }
            user = new SessionUser(gameUser);
            //Current.Bind(user);
            UserLoginLog userLoginLog = new UserLoginLog();
            userLoginLog.UserId = UserId.ToString();
            userLoginLog.SessionID = Sid;
            userLoginLog.MobileType = MobileType;
            userLoginLog.ScreenX = ScreenX;
            userLoginLog.ScreenY = ScreenY;
            userLoginLog.RetailId = RetailID;
            userLoginLog.AddTime = DateTime.Now;
            userLoginLog.State = LoginStatus.Logined;
            userLoginLog.DeviceID = DeviceID;
            userLoginLog.Ip = GetRealIP();
            userLoginLog.Pid = gameUser.Pid;
            userLoginLog.UserLv = gameUser.UserLv;
            var sender = DataSyncManager.GetDataSender();
            sender.Send(new[] { userLoginLog });

            return true;
        }

        private GameUser CreateRole()
        {
            GameUser user = new GameUser(UserId);
            user.IsRefreshing = true;
            user.SessionID = Sid;
            user.EnterServerId = ServerID;
            user.Pid = Pid;
            user.RetailID = RetailID;
            user.NickName = UserName;
            user.UserLv = (short)ConfigEnvSet.GetInt("User.Level");
            user.UserStage = SubjectStage.PreschoolSchool;
            user.GiveAwayDiamond = ConfigEnvSet.GetInt("User.InitDiamond");
            user.Vit = ConfigEnvSet.GetInt("User.InitVit");
            user.VipLv = ConfigEnvSet.GetInt("User.VipLv");
            user.LooksId = looksid;
            user.UserStatus = UserStatus.MainUi;
            user.LoginDate = DateTime.Now;
            user.CreateDate = DateTime.Now;
            user.OfflineDate = DateTime.Now;
            //user.ClassData = new UserClassData();
            //user.StudyTaskData = new UserStudyTaskData();
            //user.ExerciseTaskData = new UserExerciseTaskData();
            //user.ExpData = new UserExpData();
            //user.CombatData = new UserCombatData();
            user.CombatData.CombatTimes = ConfigEnvSet.GetInt("User.CombatInitTimes");
            user.CampaignTicketNum = ConfigEnvSet.GetInt("User.RestoreCampaignTicketNum");
            user.EventAwardData.OnlineStartTime = DateTime.Now;
            user.PlotId = 0;

            //user.FriendsData = new UserFriendsData();
            user.RefreshFightValue();


            // 默认场景地图
            var scelemaps = new ShareCacheStruct<Config_SceneMap>().FindAll();
            foreach (var scenecfg in scelemaps)
            {
                if (scenecfg.IfLock)
                    user.UnlockSceneMapList.Add(scenecfg.ID);
            }
            if (user.UnlockSceneMapList.Count > 0)
                user.SelectedSceneMapId = user.UnlockSceneMapList[0];

            // 构建个人成就系统
            for (AchievementType type = AchievementType.LevelCount; type <= AchievementType.AwardDiamondCount; ++type)
            {
                var achievement = new ShareCacheStruct<Config_Achievement>().Find(t => (t.AchievementType == type));
                if (achievement == null)
                    continue;
                AchievementData achdata = new AchievementData();
                achdata.Type = achievement.AchievementType;
                achdata.ID = achievement.id;
                if (type == AchievementType.LevelCount)
                    achdata.Count = user.UserLv;
                user.AchievementList.Add(achdata);
            }

            // 邮箱
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "欢迎进入学生战纪",
                Sender = "系统",
                Date = DateTime.Now,
                Context = "欢迎进入学生战纪",
                ApppendDiamond = 0
            };
            //mail.AppendItem.Add(new ItemData() { ID = 10001, Num = 1 });

            user.AddNewMail(ref mail);

            var cacheSet = new PersonalCacheStruct<GameUser>();
            cacheSet.Add(user);
            cacheSet.Update();

            UserHelper.RestoreUserData(UserId);


            // 加入排行榜
            Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            //Ranking<UserRank> levelranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            //Ranking<UserRank> fightvalueranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
            UserRank rankInfo = new UserRank()
            {
                UserID = user.UserID,
                NickName = user.NickName,
                LooksId = user.LooksId,
                UserLv = user.UserLv,
                IsOnline = user.IsOnline,
                RankId = int.MaxValue,
                Exp = user.TotalExp,
                FightingValue = user.FightingValue,
                RankDate = DateTime.Now,
            };
            combatranking.TryAppend(rankInfo);
            var combat = combatranking as CombatRanking;
            combat.rankList.Add(rankInfo);
            //levelranking.TryAppend(rankInfo);
            //fightvalueranking.TryAppend(rankInfo);


            // 充值数据
            UserPayCache paycache = new UserPayCache()
            {
                UserID = user.UserID,
                PayMoney = 0,
                IsReceiveFirstPay = false,
                WeekCardDays = 0,
                MonthCardDays = 0,
                WeekCardAwardDate = DateTime.MinValue,
                MonthCardAwardDate = DateTime.MinValue,
            };
            var payCacheSet = new PersonalCacheStruct<UserPayCache>();
            payCacheSet.Add(paycache);
            payCacheSet.Update();


            return user;
        }

        protected override string BuildJsonPack()
        {
            ResultData resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorInfo = "",
            };
            return MathUtils.ToJson(resultData);
        }

    }
}
