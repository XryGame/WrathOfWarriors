using GameServer.CsScript.Base;
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
using ZyGames.Framework.Common.Log;
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
                if (gameUser == null)
                    return false;
                roleFunc.OnCreateAfter(gameUser);
            }
            else
            {
                return false;
            }

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

            user = new SessionUser(gameUser);
            //Current.Bind(user);

            return true;
        }

        private GameUser CreateRole()
        {
            Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            Ranking<UserRank> levelranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            var combat = combatranking as CombatRanking;
            var level = levelranking as LevelRanking;
            
            //Ranking<UserRank> fightvalueranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
            if (combat == null || level == null)
            {
                new BaseLog().SaveLog("排行榜错误!!!");
                return null;
            }
               


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
            user.Vit = DataHelper.InitVit;
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
            //user.EventAwardData.OnlineStartTime = DateTime.Now;
            user.PlotId = 0;
            user.IsOnline = true;
            user.InviteFightDiamondNum = 0;
            user.ResetInviteFightDiamondDate = DateTime.Now;
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
                Title = "恭喜您已获得月卡免费体验资格",
                Sender = "系统",
                Date = DateTime.Now,
                Context = "恭喜您已获得月卡免费体验资格，月卡有效期为3天，为了您能获得更好的游戏体验，您可以在充值页面续费成为我们正式的月卡用户！",
                ApppendDiamond = 0
            };

            user.AddNewMail(ref mail);
            

            var cacheSet = new PersonalCacheStruct<GameUser>();
            cacheSet.Add(user);
            cacheSet.Update();

            UserHelper.RestoreUserData(UserId);


            // 加入排行榜
            UserRank rankInfo = new UserRank()
            {
                UserID = user.UserID,
                NickName = user.NickName,
                LooksId = user.LooksId,
                UserLv = user.UserLv,
                VipLv = user.VipLv,
                IsOnline = true,
                RankId = int.MaxValue,
                Exp = user.TotalExp,
                FightingValue = user.FightingValue,
                RankDate = DateTime.Now,
            };
            combat.TryAppend(rankInfo);
            combat.rankList.Add(rankInfo);
            UserRank lvUserRank = new UserRank(rankInfo);
            level.TryAppend(lvUserRank);
            level.rankList.Add(lvUserRank);
            //fightvalueranking.TryAppend(rankInfo);


            // 充值数据
            UserPayCache paycache = new UserPayCache()
            {
                UserID = user.UserID,
                PayMoney = 0,
                IsReceiveFirstPay = false,
                WeekCardDays = -1,
                MonthCardDays = 2,
                WeekCardAwardDate = DateTime.Now,
                MonthCardAwardDate = DateTime.Now,
            };
            var payCacheSet = new PersonalCacheStruct<UserPayCache>();
            payCacheSet.Add(paycache);
            payCacheSet.Update();

            UserHelper.AddMouthCardMail(user, paycache);
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
