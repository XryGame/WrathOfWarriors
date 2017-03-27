using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 请求加入公会
    /// </summary>
    public class Action1701 : BaseAction
    {
        private RequestGuildResult receipt;
        private string guildId;

        public Action1701(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1701, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("ID", ref guildId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var guild = new ShareCacheStruct<GuildsCache>().FindKey(guildId);
            if (guild == null)
            {
                receipt = RequestGuildResult.NoGuild;
                return true;
            }
            if (guild.MemberList.Count >= 50)
            {
                receipt = RequestGuildResult.Full;
                return true;
            }
            if (guild.FindRequest(Current.UserId) != null)
            {
                receipt = RequestGuildResult.HadRequest;
                return true;
            }

            GuildCharacter character = new GuildCharacter()
            {
                UserID = Current.UserId,
                Date = DateTime.Now
            };
            guild.AddNewRequest(character);

            foreach (var v in guild.MemberList)
            {
                PushMessageHelper.NewGuildRequestNotification(GameSession.Get(v.UserID), Current.UserId);
            }
            

            receipt = RequestGuildResult.Successfully;
            return true;
        }
    }
}