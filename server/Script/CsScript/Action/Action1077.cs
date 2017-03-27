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

    public class GuildBasis
    {
        public int Liveness { get; set; }

        public int Lv { get; set; }

        public int RankID { get; set; }
    }

    /// <summary>
    /// 更新公会属性通知
    /// </summary>
    public class Action1077 : BaseAction
    {

        private GuildBasis receipt;
        public Action1077(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1077, actionGetter)
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

            receipt = new GuildBasis()
            {
                Liveness = guild.Liveness,
                Lv = guild.Lv,
                RankID = guild.RankID,
            };
            return true;
        }
    }
}