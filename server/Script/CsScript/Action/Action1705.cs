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
    /// 设置公告
    /// </summary>
    public class Action1705 : BaseAction
    {
        private RequestGuildResult receipt;
        private string _content;

        public Action1705(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1705, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("Content", ref _content))
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

            var member = guild.FindMember(Current.UserId);
            if (member == null || member.JobTitle == GuildJobTitle.Normal)
            {
                receipt = RequestGuildResult.NoAuthority;
                return true;
            }

            guild.Notice = _content;
            GuildLogData log = new GuildLogData()
            {
                UserId = Current.UserId,
                UserName = GetBasis.NickName,
                LogTime = DateTime.Now,
                Content = string.Format("{0} 设置了新公告。", GetBasis.NickName),
            };
            guild.NewLog(log);

            foreach (var v in guild.MemberList)
            {
                PushMessageHelper.GuildNoticeChangeNotification(GameSession.Get(v.UserID));
                PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
            }
            

            receipt = RequestGuildResult.Successfully;
            return true;
        }
    }
}