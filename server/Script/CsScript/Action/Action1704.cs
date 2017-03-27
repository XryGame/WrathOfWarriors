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
    /// 公会签到
    /// </summary>
    public class Action1704 : BaseAction
    {
        private RequestGuildResult receipt;

        public Action1704(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1704, actionGetter)
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
            var guildsSet = new ShareCacheStruct<GuildsCache>();

            var guild = guildsSet.FindKey(GetGuild.GuildID);
            if (guild == null)
            {
                return false;
            }

            var member = guild.FindMember(Current.UserId);
            if (member == null)
            {
                return false;
            }

            if (GetGuild.IsSignIn)
            {
                receipt = RequestGuildResult.HadSignIn;  
                return true;
            }
            int addLiveness = ConfigEnvSet.GetInt("Guild.SignInLiveness");
            member.Liveness = MathUtils.Addition(member.Liveness, addLiveness, int.MaxValue);
            guild.Liveness = MathUtils.Addition(guild.Liveness, addLiveness, int.MaxValue);

            GuildLogData log = new GuildLogData()
            {
                UserId = Current.UserId,
                UserName = GetBasis.NickName,
                LogTime = DateTime.Now,
                Content = string.Format("玩家 {0} 进行了签到。", GetBasis.NickName),
            };
            guild.NewLog(log);

            foreach (var v in guild.MemberList)
            {
                PushMessageHelper.GuildMemberChangeNotification(GameSession.Get(v.UserID), Current.UserId);
                PushMessageHelper.NewGuildLogNotification(GameSession.Get(v.UserID));
            }

            int newlv = guild.ConvertLevel();
            if (guild.Lv < newlv)
            {
                guild.Lv = newlv;
                foreach (var v in guild.MemberList)
                {
                    PushMessageHelper.GuildBasisChangeNotification(GameSession.Get(v.UserID));
                }
            }


            receipt = RequestGuildResult.Successfully;
            return true;
        }
    }
}