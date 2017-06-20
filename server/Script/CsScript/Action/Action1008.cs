using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.CsScript.Remote;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
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

            receipt = new JPUserDetailsData()
            {
                UserId = Current.UserId,
                NickName = GetBasis.NickName,
                Profession = GetBasis.Profession,
                UserLv = GetBasis.UserLv,
                Diamond = GetBasis.DiamondNum,
                BuyDiamond = GetBasis.BuyDiamond,
                VipLv = GetBasis.VipLv,
                AvatarUrl = GetBasis.AvatarUrl,
                Gold = GetBasis.Gold,
                CombatRankID = GetBasis.CombatRankID,
                LevelRankID = GetBasis.LevelRankID,
                LotteryTimes = GetBasis.LotteryTimes,
                SignStartID = DataHelper.SignStartID,
                ShareCount = GetBasis.ShareCount,
                ShareDate = GetBasis.ShareDate,
                InviteCount = GetBasis.InviteCount,
                ReceiveInviteList = GetBasis.ReceiveInviteList.ToList(),
            };
            receipt.Attribute = GetAttribute;
            receipt.Equips = GetEquips;
            receipt.Package = GetPackage;
            receipt.Soul = GetSoul;
            receipt.Skill = GetSkill;
            receipt.Task = GetTask;
            receipt.Achievement = GetAchievement;
            receipt.Guild = GetGuild;
            receipt.MailBox = GetMailBox;
            receipt.EventAward = GetEventAward;
            receipt.Pay = GetPay;
            receipt.Combat = GetCombat;

            UserHelper.ElfExperienceExpireCheck(Current.UserId);
            receipt.Elf = GetElf;

            UserHelper.TransferExpireCheck(Current.UserId);
            receipt.Transfer = GetTransfer;

            /// 好友
            {
                receipt.Friends.GiveAwayCount = GetFriends.GiveAwayCount;
                foreach (var v in GetFriends.FriendsList)
                {
                    var basis = UserHelper.FindUserBasis(v.UserId);
                    JPFriendData friend = new JPFriendData()
                    {
                        UserId = v.UserId,
                        NickName = basis.NickName,
                        Profession = basis.Profession,
                        AvatarUrl = basis.AvatarUrl,
                        UserLv = basis.UserLv,
                        VipLv = basis.VipLv,
                        IsGiveAway = v.IsGiveAway,
                        IsByGiveAway = v.IsByGiveAway,
                        IsReceiveGiveAway = v.IsReceiveGiveAway,
                    };
                    var gameSession = GameSession.Get(v.UserId);
                    friend.IsOnline = gameSession != null && gameSession.Connected;
                    receipt.Friends.FriendsList.Add(friend);
                }
                foreach (var v in GetFriends.ApplyList)
                {
                    var basis = UserHelper.FindUserBasis(v.UserId);
                    JPFriendApplyData apply = new JPFriendApplyData()
                    {
                        UserId = v.UserId,
                        NickName = basis.NickName,
                        Profession = basis.Profession,
                        AvatarUrl = basis.AvatarUrl,
                        UserLv = basis.UserLv,
                        VipLv = basis.VipLv,
                        ApplyTime = v.ApplyDate
                    };
                    var gameSession = GameSession.Get(v.UserId);
                    apply.IsOnline = gameSession != null && gameSession.Connected;
                    receipt.Friends.ApplyList.Add(apply);
                }
            }

            /// 公会
            if (!receipt.Guild.GuildID.IsEmpty())
            {
                var guildData = new ShareCacheStruct<GuildsCache>().FindKey(receipt.Guild.GuildID);
                if (guildData != null && guildData.FindMember(Current.UserId) != null)
                {
                    UserHelper.BulidJPGuildData(GetGuild.GuildID, receipt.GuildData);
                }
   
            }


            if (GetBasis.IsReceiveOfflineEarnings)
            {
                receipt.OfflineEarnings = "0";
                receipt.OfflineTimeSec = 0;
            }
            else
            {
                // 离线收益

                BigInteger transscriptEarnings = 0;
                var monster = new ShareCacheStruct<Config_Monster>().Find(t => t.Grade == GetBasis.UserLv);

                BigInteger bi = BigInteger.Parse(monster.DropoutGold) * 30;
                transscriptEarnings += bi;

                double rate = Convert.ToDouble(GetBasis.OfflineTimeSec / 1800.0);
                int tmp = Convert.ToInt32(rate * 100);

                //var vipcfg = new ShareCacheStruct<Config_Vip>().FindKey(GetBasis.VipLv);
                //if (vipcfg != null)
                {
                    int skillAddition = 0;
                    var elfcfg = new ShareCacheStruct<Config_Elves>().Find(t => t.ElvesID == GetElf.SelectID);
                    if (elfcfg != null && elfcfg.ElvesType == ElfSkillType.OffineGold)
                    {
                        skillAddition = elfcfg.ElvesNum;
                    }
                    BigInteger sum = transscriptEarnings * tmp / 100;
                    //BigInteger earning = sum + sum / 100 * (vipcfg.Multiple);
                    //BigInteger earning2 = earning + earning / 1000 * skillAddition;
                    BigInteger earning2 = sum + sum / 1000 * skillAddition;
                    if (GetPay.QuarterCardDays >= 0)
                    {
                        earning2 = earning2 * 2;
                    }

                    GetBasis.OfflineEarnings = earning2.ToNotNullString("0");
                }

            }
            receipt.OfflineTimeSec = GetBasis.OfflineTimeSec;
            receipt.OfflineEarnings = GetBasis.OfflineEarnings;

            UserHelper.AchievementProcess(Current.UserId, AchievementType.CombatRandID, "0", 0, false);

            return true;
        }




        public override void TakeActionAffter(bool state)
        {

            for (ChatType type = ChatType.World; type < ChatType.System; ++type)
            {
                //var parameters = new Parameters();
                //parameters["ChatType"] = type;
                //var packet = ActionFactory.GetResponsePackage(ActionIDDefine.Cst_Action3002, Current, parameters, OpCode.Text, null);
                //ActionFactory.SendAction(Current, ActionIDDefine.Cst_Action3002, packet, (session, asyncResult) => { }, 0);
            }
            string context = "";
            RankType ranktype = RankType.No;
            int rankid = 0;

            if (GetBasis.LevelRankID != 0 && GetBasis.LevelRankID < 10)
            {
                ranktype = RankType.Level;
                rankid = GetBasis.LevelRankID;
                context = string.Format("排行榜第{0}名的 {1} 上线了！", rankid, GetBasis.NickName);
            }
            if (GetBasis.CombatRankID != 0 && GetBasis.CombatRankID <= rankid)
            {
                ranktype = RankType.Combat;
                rankid = GetBasis.CombatRankID;
                context = string.Format("通天塔排名第{0}名的 {1} 上线了！", rankid, GetBasis.NickName);
            }

            if (ranktype != RankType.No)
            {
                if (GetBasis.UserLv >= DataHelper.OpenRankSystemUserLevel)
                {
                    GlobalRemoteService.SendNotice(NoticeMode.World, context);
                }

            }

            // 通知好友上线
            foreach (FriendData fd in GetFriends.FriendsList)
            {
                PushMessageHelper.FriendOnlineNotification(GameSession.Get(fd.UserId), Current.UserId);
            }


            // 通知公会成员下线
            if (!GetGuild.GuildID.IsEmpty())
            {
                var guildData = new ShareCacheStruct<GuildsCache>().FindKey(GetGuild.GuildID);
                foreach (var v in guildData.MemberList)
                {
                    if (v.UserID != Current.UserId)
                        PushMessageHelper.GuildMemberOnlineNotification(GameSession.Get(v.UserID), Current.UserId);
                }
            }

            //context = "欢迎进入创想学院！";
            //RemoteClient.SendSystemChat(Current.UserId, context);


            GetBasis.IsRefreshing = false;

            base.TakeActionAffter(state);
        }
    }
}