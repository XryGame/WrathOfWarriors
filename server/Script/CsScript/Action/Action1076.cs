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
    /// 加入了公会
    /// </summary>
    public class Action1076 : BaseAction
    {

        private JPGuildData receipt = null;
        public Action1076(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1076, actionGetter)
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
            receipt = new JPGuildData();
            var guildData = new ShareCacheStruct<GuildsCache>().FindKey(GetGuild.GuildID);
            if (guildData != null && guildData.FindMember(Current.UserId) != null)
            {
                UserHelper.BulidJPGuildData(GetGuild.GuildID, receipt);
            }
            return true;
        }
            
    }
}