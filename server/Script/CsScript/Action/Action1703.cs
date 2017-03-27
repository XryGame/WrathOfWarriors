using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 设置职位
    /// </summary>
    public class Action1703 : BaseAction
    {
        private RequestGuildResult receipt;
        private int _destUid;
        private GuildJobTitle _jobTitle;

        public Action1703(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1703, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUId", ref _destUid)
                && httpGet.GetEnum("JobTitle", ref _jobTitle))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var guildsSet = new ShareCacheStruct<GuildsCache>();

            var guild = guildsSet.FindKey(GetGuild.GuildID);
            if (guild == null)
            {
                return false;
            }

            int needDiamond = ConfigEnvSet.GetInt("User.CreateGuildNeedDiamond");
            if (GetBasis.DiamondNum < needDiamond)
            {
                receipt = RequestGuildResult.NoDiamond;
                return true;
            }

            var basis = UserHelper.FindUserBasis(_destUid);

            var self = guild.FindMember(Current.UserId);
            var destMember = guild.FindMember(_destUid);
            if (Current.UserId == _destUid 
                || guild.FindAtevent().UserID != Current.UserId 
                || destMember == null)
            {
                receipt = RequestGuildResult.NoAuthority;
                return true;
            }


            switch (_jobTitle)
            {
                case GuildJobTitle.Atevent:
                    {
                        destMember.JobTitle = GuildJobTitle.Atevent;
                        self.JobTitle = GuildJobTitle.Normal;

                        GuildLogData log = new GuildLogData()
                        {
                            UserId = basis.UserID,
                            UserName = basis.NickName,
                            LogTime = DateTime.Now,
                            Content = string.Format("玩家 {0} 被提升为会长。", basis.NickName),
                        };
                        guild.NewLog(log);


                        foreach (var v in guild.MemberList)
                        {
                            PushMessageHelper.GuildMemberChangeNotification(GameSession.Get(v.UserID), _destUid);
                            PushMessageHelper.GuildMemberChangeNotification(GameSession.Get(v.UserID), Current.UserId);
                            PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
                        }

                    }
                    break;
                case GuildJobTitle.Vice:
                    {
                        if (guild.FindVice(_destUid) != null)
                        {
                            receipt = RequestGuildResult.NoAuthority;
                            return true;
                        }
                        if (guild.FindVice().Count >= 2)
                        {
                            return false;
                        }
                        destMember.JobTitle = GuildJobTitle.Vice;
                        GuildLogData log = new GuildLogData()
                        {
                            UserId = basis.UserID,
                            UserName = basis.NickName,
                            LogTime = DateTime.Now,
                            Content = string.Format("玩家 {0} 被提升为副会长。", basis.NickName),
                        };
                        guild.NewLog(log);
                        foreach (var v in guild.MemberList)
                        {
                            PushMessageHelper.GuildMemberChangeNotification(GameSession.Get(v.UserID), _destUid);
                            PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
                        }
                    }
                    break;
                case GuildJobTitle.Normal:
                    {
                        if (guild.FindVice(_destUid) == null)
                        {
                            receipt = RequestGuildResult.NoAuthority;
                            return true;
                        }
                        destMember.JobTitle = GuildJobTitle.Normal;
                        GuildLogData log = new GuildLogData()
                        {
                            UserId = basis.UserID,
                            UserName = basis.NickName,
                            LogTime = DateTime.Now,
                            Content = string.Format("玩家 {0} 被降为普通成员。", basis.NickName),
                        };
                        guild.NewLog(log);
                        foreach (var v in guild.MemberList)
                        {
                            PushMessageHelper.GuildMemberChangeNotification(GameSession.Get(v.UserID), _destUid);
                            PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
                        }
                    }
                    break;
            }

            
            receipt = RequestGuildResult.Successfully;
            return true;
        }
    }
}