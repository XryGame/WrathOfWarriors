﻿using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.LogModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Configuration;
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
        private int profession;
        private string unid = string.Empty;

        private int inviterUserId = 0;
        public Action1005(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1005, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        protected override bool GetActionParam()
        {
            if (httpGet.GetInt("Profession", ref profession))
            {
                httpGet.GetString("Unid", ref unid);
                return true;
            }
            return false;
        }

        protected override bool CreateUserRole(out IUser user)
        {
            user = null;
            UserBasisCache basis = UserHelper.FindUserBasis(UserId);
            if (basis == null)
            {
                var nickNameCheck = new NickNameCheck();
                var KeyWordCheck = new KeyWordCheck();
                string msg;

                if (nickNameCheck.VerifyRange(UserName, out msg) ||
                    KeyWordCheck.VerifyKeyword(UserName, out msg) ||
                    nickNameCheck.IsExistNickName(UserName, out msg))
                {
                    ErrorCode = Language.Instance.ErrorCode;
                    ErrorInfo = msg;
                    return false;
                }

                
                basis = CreateRole(UserId, Sid, ServerID, Pid, RetailID, UserName, profession, HeadID);
                if (basis != null)
                {
                    /// 邀请处理
                    if (!string.IsNullOrEmpty(unid))
                    {
                        var selflist = Util.FindUserCenterUser(Pid, RetailID, ServerID);
                        var inviterlist = Util.FindUserCenterUser(unid, RetailID, ServerID);
                        if (inviterlist.Count > 0 && inviterlist[0].UserID != 0 && Pid != unid)
                        {
                            if (selflist.Count > 0 && string.IsNullOrEmpty(selflist[0].Unid))
                            {
                                inviterUserId = inviterlist[0].UserID;
                                selflist[0].Unid = unid;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }

                nickNameCheck.OnCreateAfter(basis);
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
            userLoginLog.Pid = basis.Pid;
            userLoginLog.UserLv = basis.UserLv;
            var sender = DataSyncManager.GetDataSender();
            sender.Send(new[] { userLoginLog });

            user = new SessionUser(basis);



            // 记录删档测试登录老用户
            GameRunStatus gameRunStatus = ConfigUtils.GetSetting("Game.RunStatus").ToEnum<GameRunStatus>();
            if (gameRunStatus == GameRunStatus.DeleteFileTest)
            {
                OldUserLog record = new OldUserLog()
                {
                    UserID = basis.UserID,
                    OpenID = basis.Pid,
                    NickName = basis.NickName,
                    AvatarUrl = basis.AvatarUrl,
                    CreateDate = DateTime.Now,
                };
                //var oldUserSet = new ShareCacheStruct<OldUserRecord>();
                //oldUserSet.AddOrUpdate(record);
                sender.Send(new[] { record });

                MailData mail = new MailData()
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = "《勇者之怒》删档测试通知",
                    Sender = "系统",
                    Date = DateTime.Now,
                    Context = "尊敬的玩家，您好，欢迎您进入我们的游戏，目前游戏正在删档封测阶段，如果您在游戏中遇到任何问题，请及时联系qq 2602611792处理。\n" +
                                "  本次测试时间为7月11日~7月18日，测试结束后，我们将清空所有玩家数据，请玩家牢记角色ID以及昵称，及时联系平台客服兑换奖励，没有获奖的玩家也可以在游戏正式上线时"+
                                "使用测试时的账号登录游戏可以享受充值3倍钻石优惠以及领取老玩家大礼包一份，千万不要错过哟！",
                };
                UserHelper.AddNewMail(basis.UserID, mail, false);
            }
            else if (gameRunStatus == GameRunStatus.OfficialOperation)
            {
                if (new MemoryCacheStruct<OldUserCache>().Find(t => (t.OpenID == basis.Pid)) != null)
                {// 是删档测试老用户发放奖励
                    //MailData mail = new MailData()
                    //{
                    //    ID = Guid.NewGuid().ToString(),
                    //    Title = "老用户回归奖励",
                    //    Sender = "系统",
                    //    Date = DateTime.Now,
                    //    Context = "感谢您再次登录勇者之怒，这是您的回归奖励！",
                    //};
                    //UserHelper.AddNewMail(basis.UserID, mail, false);
                }
            }

            return true;
        }

        public static  UserBasisCache CreateRole(int _UserId, string _Sid, int _ServerID, string _Pid, string _RetailID, 
            string _UserName, int _profession, string _HeadID)
        {
            // Basis初始化
            UserBasisCache basis = new UserBasisCache(_UserId);
            basis.IsRefreshing = true;
            basis.SessionID = _Sid;
            basis.ServerID = _ServerID;
            basis.Pid = _Pid;
            basis.RetailID = _RetailID;
            basis.NickName = _UserName;
            basis.UserLv = (short)ConfigEnvSet.GetInt("User.Level");
            basis.RewardsDiamond = ConfigEnvSet.GetInt("User.InitDiamond");
            //bisis.Vit = DataHelper.InitVit;
            basis.VipLv = ConfigEnvSet.GetInt("User.VipLv");
            basis.Profession = _profession;
            basis.AvatarUrl = _HeadID;
            basis.UserStatus = UserStatus.MainUi;
            basis.LoginDate = DateTime.Now;
            basis.CreateDate = DateTime.Now;
            basis.OfflineDate = DateTime.Now;
            basis.Gold = ConfigEnvSet.GetString("User.InitGold");
            basis.OfflineEarnings = "0";

            var cacheSet = new PersonalCacheStruct<UserBasisCache>();
            cacheSet.Add(basis);
            cacheSet.Update();

            // 属性初始化
            UserAttributeCache attcache = new UserAttributeCache();
            attcache.UserID = basis.UserID;
            var attributeSet = new PersonalCacheStruct<UserAttributeCache>();
            attributeSet.Add(attcache);
            attributeSet.Update();

            // 装备初始化
            UserEquipsCache equipcache = new UserEquipsCache();
            equipcache.UserID = basis.UserID;
            equipcache.ResetCache();
            var equipsSet = new PersonalCacheStruct<UserEquipsCache>();
            equipsSet.Add(equipcache);
            equipsSet.Update();
            
            // 背包初始化
            UserPackageCache packagecache = new UserPackageCache();
            packagecache.UserID = basis.UserID;
            packagecache.ResetCache();
            var packageSet = new PersonalCacheStruct<UserPackageCache>();


            packagecache.AddItem(20001, 1);
            packagecache.AddItem(20012, 1);
            packagecache.AddItem(20023, 1);
            packagecache.AddItem(20034, 1);
            packagecache.AddItem(20045, 1);
            packagecache.AddItem(20056, 1);
            packagecache.AddItem(20067, 1);
            //for (int i = 20001; i < 20077; ++i)
            //    packagecache.AddItem(i, 10);
            //for (int i = 30001; i < 30005; ++i)
            //    packagecache.AddItem(i, 9999);
            //for (int i = 40001; i < 40009; ++i)
            //    packagecache.AddItem(i, 1);

            packageSet.Add(packagecache);
            packageSet.Update();

            // 战魂初始化
            UserSoulCache soulcache = new UserSoulCache();
            soulcache.UserID = basis.UserID;
            soulcache.ResetCache();
            var soulSet = new PersonalCacheStruct<UserSoulCache>();
            soulSet.Add(soulcache);
            soulSet.Update();

            

            // 技能初始化
            UserSkillCache skillcache = new UserSkillCache();
            skillcache.UserID = basis.UserID;
            skillcache.ResetCache(_profession);
            var skillSet = new PersonalCacheStruct<UserSkillCache>();
            skillSet.Add(skillcache);
            skillSet.Update();

            // 好友初始化
            UserFriendsCache friendscache = new UserFriendsCache();
            friendscache.UserID = basis.UserID;
            friendscache.ResetCache();
            var friendsSet = new PersonalCacheStruct<UserFriendsCache>();
            friendsSet.Add(friendscache);
            friendsSet.Update();

            // 成就初始化
            UserAchievementCache achievecache = new UserAchievementCache();
            achievecache.UserID = basis.UserID;
            achievecache.ResetCache();
            var achieveSet = new PersonalCacheStruct<UserAchievementCache>();
            achieveSet.Add(achievecache);
            achieveSet.Update();

            // 充值初始化
            UserPayCache paycache = new UserPayCache();
            paycache.UserID = basis.UserID;
            paycache.ResetCache();
            var paySet = new PersonalCacheStruct<UserPayCache>();
            paySet.Add(paycache);
            paySet.Update();

            // 邮箱初始化
            UserMailBoxCache mailcache = new UserMailBoxCache();
            mailcache.UserID = basis.UserID;
            mailcache.ResetCache();
            var mailSet = new PersonalCacheStruct<UserMailBoxCache>();
            MailData mail = new MailData()
            {
                ID = Guid.NewGuid().ToString(),
                Title = "恭喜您已获得月卡免费体验资格",
                Sender = "系统",
                Date = DateTime.Now,
                Context = "恭喜您已获得月卡免费体验资格，月卡有效期为1天，为了您能获得更好的游戏体验，您可以在充值页面续费成为我们正式的月卡用户！",
            };
            UserHelper.AddNewMail(basis.UserID, mail, false);
            mailSet.Add(mailcache);
            mailSet.Update();

            // 任务初始化
            UserTaskCache taskcache = new UserTaskCache();
            taskcache.UserID = basis.UserID;
            taskcache.ResetCache();
            var taskSet = new PersonalCacheStruct<UserTaskCache>();
            taskSet.Add(taskcache);
            taskSet.Update();
            

            // 竞技场初始化
            UserCombatCache combatcache = new UserCombatCache();
            combatcache.UserID = basis.UserID;
            combatcache.ResetCache();
            var combatSet = new PersonalCacheStruct<UserCombatCache>();
            combatSet.Add(combatcache);
            combatSet.Update();

            // 活动相关初始化
            UserEventAwardCache eventawardcache = new UserEventAwardCache();
            eventawardcache.UserID = basis.UserID;
            eventawardcache.ResetCache();
            var eventAwardSet = new PersonalCacheStruct<UserEventAwardCache>();
            eventAwardSet.Add(eventawardcache);
            eventAwardSet.Update();

            // 公会初始化
            UserGuildCache guildcache = new UserGuildCache();
            guildcache.UserID = basis.UserID;
            guildcache.ResetCache();
            var guildSet = new PersonalCacheStruct<UserGuildCache>();
            guildSet.Add(guildcache);
            guildSet.Update();

            // 精灵初始化
            UserElfCache elfcache = new UserElfCache();
            elfcache.UserID = basis.UserID;
            elfcache.ResetCache();
            var elfSet = new PersonalCacheStruct<UserElfCache>();
            elfSet.Add(elfcache);
            elfSet.Update();

            // 赠送物品初始化
            UserTransferItemCache transfercache = new UserTransferItemCache();
            transfercache.UserID = basis.UserID;
            transfercache.ResetCache();
            var transferSet = new PersonalCacheStruct<UserTransferItemCache>();
            transferSet.Add(transfercache);
            transferSet.Update();

            // 仇人数据初始化
            UserEnemysCache enemy = new UserEnemysCache();
            enemy.UserID = basis.UserID;
            enemy.ResetCache();
            var enemySet = new PersonalCacheStruct<UserEnemysCache>();
            enemySet.Add(enemy);
            enemySet.Update();

            // 抽奖数据初始化
            UserLotteryCache lottery = new UserLotteryCache();
            lottery.UserID = basis.UserID;
            lottery.ResetCache();
            var lotterySet = new PersonalCacheStruct<UserLotteryCache>();
            lotterySet.Add(lottery);
            lotterySet.Update();

            UserHelper.RefreshUserFightValue(basis.UserID, false);

            // 排行榜初始化
            UserRank combatRank = new UserRank()
            {
                UserID = basis.UserID,
                NickName = basis.NickName,
                Profession = basis.Profession,
                UserLv = basis.UserLv,
                AvatarUrl = basis.AvatarUrl,
                VipLv = basis.VipLv,
                FightValue = attcache.FightValue,
                RankId = int.MaxValue,
                RankDate = DateTime.Now,
            };
            Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            var combat = combatranking as CombatRanking;
            combat.TryAppend(combatRank);
            combat.rankList.Add(combatRank);

            UserRank levelRank = new UserRank(combatRank);
            Ranking<UserRank> levelranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
            var level = levelranking as LevelRanking;
            level.TryAppend(levelRank);
            level.rankList.Add(levelRank);

            UserRank fightRank = new UserRank(combatRank);
            Ranking<UserRank> fightranking = RankingFactory.Get<UserRank>(FightValueRanking.RankingKey);
            var fight = fightranking as FightValueRanking;
            fight.TryAppend(fightRank);
            fight.rankList.Add(fightRank);

            UserRank comboRank = new UserRank(combatRank);
            Ranking<UserRank> comboranking = RankingFactory.Get<UserRank>(ComboRanking.RankingKey);
            var combo = comboranking as ComboRanking;
            combo.TryAppend(comboRank);
            combo.rankList.Add(comboRank);


            UserHelper.RestoreUserData(basis.UserID);
            UserHelper.EveryDayTaskProcess(basis.UserID, TaskType.Login, 1, false);
            //UserHelper.AddMouthCardMail(basis.UserID);


            return basis;
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

        public override void TakeActionAffter(bool state)
        {
            //if (inviterUserId != 0)
            //{
            //    PushMessageHelper.NewInviteSucceedNotification(GameSession.Get(inviterUserId));
            //}
            
        }
    }
}
