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
    /// 请求创建公会
    /// </summary>
    public class Action1702 : BaseAction
    {
        private RequestGuildResult receipt;
        private string _guildName;

        public Action1702(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1702, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("Name", ref _guildName))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var guildsSet = new ShareCacheStruct<GuildsCache>();
            if (!GetGuild.GuildID.IsEmpty())
            {
                var oldguild = guildsSet.FindKey(GetGuild.GuildID);
                if (oldguild != null && oldguild.FindMember(Current.UserId) != null)
                {
                    return false;
                }
            }

            int needDiamond = ConfigEnvSet.GetInt("User.CreateGuildNeedDiamond");
            if (GetBasis.DiamondNum < needDiamond)
            {
                receipt = RequestGuildResult.NoDiamond;
                return true;
            }
            if (guildsSet.Find(t => t.GuildName == _guildName) != null)
            {
                receipt = RequestGuildResult.HadName;
                return true;
            }

            GuildsCache guild = new GuildsCache()
            {
                GuildID = Guid.NewGuid().ToString(),
                GuildName = _guildName,
                Liveness = 0,
                RankID = 0,
                CreateDate = DateTime.Now,
                Lv = 1
            };

            GuildCharacter member = new GuildCharacter()
            {
                UserID = Current.UserId,
                JobTitle = GuildJobTitle.Atevent,
                Liveness = 0
            };
            guild.AddNewMember(member);

            GuildLogData log = new GuildLogData()
            {
                UserId = Current.UserId,
                UserName = GetBasis.NickName,
                LogTime = DateTime.Now,
                Content = string.Format("玩家 {0} 创建了公会。", GetBasis.NickName),
            };
            guild.NewLog(log);

            guildsSet.Add(guild);
            guildsSet.Update();
            GetGuild.GuildID = guild.GuildID;
            PushMessageHelper.JoinGuildNotification(GameSession.Get(Current.UserId));

            ChatRemoteService.SendUserData(GetBasis, GetGuild.GuildID);

            receipt = RequestGuildResult.Successfully;
            return true;
        }
    }
}