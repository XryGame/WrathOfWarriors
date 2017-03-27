﻿using GameServer.CsScript.Base;
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
        private int profession;
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
                basis = CreateRole();
                if (basis == null)
                    return false;
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
            //Current.Bind(user);

            return true;
        }

        private UserBasisCache CreateRole()
        {
            // Basis初始化
            UserBasisCache basis = new UserBasisCache(UserId);
            basis.IsRefreshing = true;
            basis.SessionID = Sid;
            basis.ServerID = ServerID;
            basis.Pid = Pid;
            basis.RetailID = RetailID;
            basis.NickName = UserName;
            basis.UserLv = (short)ConfigEnvSet.GetInt("User.Level");
            basis.RewardsDiamond = ConfigEnvSet.GetInt("User.InitDiamond");
            //bisis.Vit = DataHelper.InitVit;
            basis.VipLv = ConfigEnvSet.GetInt("User.VipLv");
            basis.Profession = profession;
            basis.UserStatus = UserStatus.MainUi;
            basis.LoginDate = DateTime.Now;
            basis.CreateDate = DateTime.Now;
            basis.OfflineDate = DateTime.Now;
            basis.IsOnline = true;
            basis.InviteFightDiamondNum = 0;
            basis.ResetInviteFightDiamondDate = DateTime.Now;
            basis.Gold = "10000";
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

            UserHelper.RefreshUserFightValue(basis.UserID, false);

            // 背包初始化
            UserPackageCache packagecache = new UserPackageCache();
            packagecache.UserID = basis.UserID;
            packagecache.ResetCache();
            var packageSet = new PersonalCacheStruct<UserPackageCache>();

            packagecache.AddItem(10001, 99, false);
            packagecache.AddItem(10006, 99, false);
            for (int i = 10011; i < 10017; ++i)
                packagecache.AddItem(i, 999, false);
            for (int i = 10053; i < 10097; ++i)
                packagecache.AddItem(i, 10, false);

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
            skillcache.ResetCache(profession);
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
                Context = "恭喜您已获得月卡免费体验资格，月卡有效期为3天，为了您能获得更好的游戏体验，您可以在充值页面续费成为我们正式的月卡用户！",
                ApppendDiamond = 0
            };
            UserHelper.AddNewMail(basis.UserID, mail);
            mailSet.Add(mailcache);
            mailSet.Update();

            // 任务初始化
            UserTaskCache taskcache = new UserTaskCache();
            taskcache.UserID = basis.UserID;
            taskcache.ResetCache();
            var taskSet = new PersonalCacheStruct<UserTaskCache>();
            taskSet.Add(taskcache);
            taskSet.Update();
            UserHelper.EveryDayTaskProcess(basis.UserID, TaskType.Login, 1, false);

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

            // 排行榜初始化
            UserRank rankInfo = new UserRank()
            {
                UserID = basis.UserID,
                NickName = basis.NickName,
                Profession = basis.Profession,
                UserLv = basis.UserLv,
                VipLv = basis.VipLv,
                RankId = int.MaxValue,
                RankDate = DateTime.Now,
            };
            Ranking<UserRank> combatranking = RankingFactory.Get<UserRank>(CombatRanking.RankingKey);
            var combat = combatranking as CombatRanking;
            combat.TryAppend(rankInfo);
            combat.rankList.Add(rankInfo);

            UserHelper.RestoreUserData(basis.UserID);
            UserHelper.AddMouthCardMail(basis.UserID);
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

    }
}
