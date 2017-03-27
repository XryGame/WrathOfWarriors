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
    /// 1074_新公会日志通知
    /// </summary>
    public class Action1074 : BaseAction
    {

        private GuildLogData receipt;
        public Action1074(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1074, actionGetter)
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
            var newlog = guild.LogList.Find(t => true);
            receipt = new GuildLogData()
            {
                LogTime = newlog.LogTime,
                UserId = newlog.UserId,
                UserName = newlog.UserName,
                Content = newlog.Content,
            };
            return true;
        }
    }
}