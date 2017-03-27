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
    /// 1075_公会公告修改通知
    /// </summary>
    public class Action1075 : BaseAction
    {

        private string receipt;
        public Action1075(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1075, actionGetter)
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
            var guild = new ShareCacheStruct<GuildsCache>().FindKey(GetGuild.GuildID);
            if (guild == null)
                return false;

            receipt = guild.Notice;
            return true;
        }
    }
}