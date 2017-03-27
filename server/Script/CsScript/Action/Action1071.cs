using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1071_新的加入工会邀请通知
    /// </summary>
    public class Action1071 : BaseAction
    {

        private JPGuildApplyData receipt;
        private int _userId;
        public Action1071(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1071, actionGetter)
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
            var request = guild.FindRequest(_userId);
            receipt = new JPGuildApplyData()
            {
                UserID = basis.UserID,
                NickName = basis.NickName,
                Profession = basis.Profession,
                UserLv = basis.UserLv,
                CombatRankID = basis.CombatRankID,
                ApplyTime = request.Date
            };
            return true;
        }
    }
}