using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class GuildInfo
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public int Lv { get; set; }

        public int RankID { get; set; }

        public int MemberCount { get; set; }

        public int Atevent { get; set; }

        public string AteventName { get; set; }

    }

    /// <summary>
    /// 查看公会列表
    /// </summary>
    public class Action1700 : BaseAction
    {
        private List<GuildInfo> receipt;

        public Action1700(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1700, actionGetter)
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
            receipt = new List<GuildInfo>();
            var list = new ShareCacheStruct<GuildsCache>().FindAll();
            foreach (var v in list)
            {
                GuildInfo info = new GuildInfo()
                {
                    ID = v.GuildID,
                    Name = v.GuildName,
                    Lv = v.Lv,
                    RankID = v.RankID,
                    MemberCount = v.MemberList.Count
                };
                var atevent = v.FindAtevent();
                info.Atevent = atevent.UserID;
                var basis = UserHelper.FindUserBasis(atevent.UserID);
                info.AteventName = basis.NickName;

                receipt.Add(info);
            }
            return true;
        }
    }
}