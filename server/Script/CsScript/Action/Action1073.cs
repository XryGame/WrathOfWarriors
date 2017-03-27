using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1073_更新公会成员信息
    /// </summary>
    public class Action1073 : BaseAction
    {

        private JPGuildMemberData receipt;
        private int _userId;
        public Action1073(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1073, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("UserId", ref _userId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var guild = new ShareCacheStruct<GuildsCache>().FindKey(GetGuild.GuildID);
            if (guild == null)
                return false;
            var basis = UserHelper.FindUserBasis(_userId);
            var member = guild.FindMember(_userId);
            receipt = new JPGuildMemberData()
            {
                UserID = basis.UserID,
                NickName = basis.NickName,
                Profession = basis.Profession,
                UserLv = basis.UserLv,
                CombatRankID = basis.CombatRankID,
                JobTitle = member.JobTitle,
                Liveness = 0,
            };
            var session = GameSession.Get(_userId);
            receipt.IsOnline = session != null && session.Connected;
            return true;
        }
    }
}