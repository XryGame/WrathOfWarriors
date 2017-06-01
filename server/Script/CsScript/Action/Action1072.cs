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
    /// 1072_新成员加入公会通知
    /// </summary>
    public class Action1072 : BaseAction
    {

        private JPGuildMemberData receipt;
        private int _userId;
        public Action1072(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1072, actionGetter)
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
            receipt = new JPGuildMemberData()
            {
                UserID = basis.UserID,
                NickName = basis.NickName,
                Profession = basis.Profession,
                AvatarUrl = basis.AvatarUrl,
                UserLv = basis.UserLv,
                CombatRankID = basis.CombatRankID,
                JobTitle = GuildJobTitle.Normal,
                Liveness = 0,
            };
            var session = GameSession.Get(_userId);
            receipt.IsOnline = session != null && session.Connected;
            return true;
        }
    }
}