using GameServer.CsScript.Remote;
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
    /// 请求退出公会
    /// </summary>
    public class Action1709 : BaseAction
    {
        private bool receipt;

        public Action1709(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1709, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            bool isAtevent = false;
            var guildsSet = new ShareCacheStruct<GuildsCache>();
            var guildData = guildsSet.FindKey(GetGuild.GuildID);
            if (guildData == null)
            {
                return false;
            }

            
            var member = guildData.FindMember(Current.UserId);
            if (member == null)
            {
                return false;
            }

            isAtevent = member.JobTitle == GuildJobTitle.Atevent;

            
            guildData.RemoveMember(member);
            GetGuild.GuildID = string.Empty;
            
            if (guildData.MemberList.Count > 0)
            {
                if (isAtevent)
                {
                    var ac = guildData.FindAteventCandidate();
                    if (ac != null)
                    {
                        ac.JobTitle = GuildJobTitle.Atevent;
                        foreach (var v in guildData.MemberList)
                        {
                            PushMessageHelper.GuildMemberChangeNotification(GameSession.Get(v.UserID), ac.UserID);
                        }
                    }

                }

                GuildLogData log = new GuildLogData()
                {
                    UserId = Current.UserId,
                    UserName = GetBasis.NickName,
                    LogTime = DateTime.Now,
                    Content = string.Format("玩家 {0} 退出了公会。", GetBasis.NickName),
                };
                guildData.NewLog(log);

                foreach (var v in guildData.MemberList)
                {
                    PushMessageHelper.GuildMemberRemoveNotification(GameSession.Get(v.UserID), Current.UserId);
                    PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
                }
            }


            GlobalRemoteService.SendUserData(GetBasis, GetGuild.GuildID);

            receipt = true;
            return true;
        }
    }
}