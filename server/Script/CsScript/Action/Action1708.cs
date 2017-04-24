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
    /// 移除公会成员
    /// </summary>
    public class Action1708 : BaseAction
    {
        private RequestGuildResult receipt;
        private int _DestUid;

        public Action1708(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1708, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("DestUid", ref _DestUid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var guildData = new ShareCacheStruct<GuildsCache>().FindKey(GetGuild.GuildID);
            if (guildData == null)
            {
                return false;
            }


            if (Current.UserId == _DestUid
                || (guildData.FindAtevent().UserID != Current.UserId  && guildData.FindVice(Current.UserId) == null))
            {
                receipt = RequestGuildResult.NoAuthority;
                return true;
            }

            var basis = UserHelper.FindUserBasis(_DestUid);
            var guild = UserHelper.FindUserGuild(_DestUid);
            var member = guildData.FindMember(_DestUid);
            if (basis == null || member == null)
            {
                return false;
            }


            GuildLogData log = new GuildLogData()
            {
                UserId = basis.UserID,
                UserName = basis.NickName,
                LogTime = DateTime.Now,
                Content = string.Format("玩家 {0} 被移出公会。", basis.NickName),
            };
            guildData.NewLog(log);

            foreach (var v in guildData.MemberList)
            {
                PushMessageHelper.GuildMemberRemoveNotification(GameSession.Get(v.UserID), _DestUid);
                PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
            }

            guildData.RemoveMember(member);
            guild.GuildID = string.Empty;
            GlobalRemoteService.SendUserData(basis, guild.GuildID);

            receipt = RequestGuildResult.Successfully;
            return true;
        }
    }
}