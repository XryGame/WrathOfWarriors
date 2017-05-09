using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 兑换CDK
    /// </summary>
    public class Action1930 : BaseAction
    {
        private ReceiveCdKeyResult receipt;
        //private Random random = new Random();
        private string _CDKey;
        public Action1930(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1930, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("CDKey", ref _CDKey))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = ReceiveCdKeyResult.OK;


            var acc = new ShareCacheStruct<Config_CdKey>().Find(t => t.Key == _CDKey);
            if (acc == null)
            {
                receipt = ReceiveCdKeyResult.Invalid;
                return true;
            }
            var cdkscache = new ShareCacheStruct<CDKeyCache>();
            if (cdkscache.FindKey(_CDKey) != null)
            {
                receipt = ReceiveCdKeyResult.Received;
                return true;
            }

            CDKeyCache cdk = new CDKeyCache()
            {
                CDKey = _CDKey,
                UsedTime = DateTime.Now
            };

            cdkscache.Add(cdk);
            cdkscache.Update();
            
            List<ItemData> itemlist = new List<ItemData>();
            itemlist.Add(new ItemData() { ID = acc.AAwardID, Num = acc.AAwardN });
            itemlist.Add(new ItemData() { ID = acc.BAwardID, Num = acc.BAwardN });
            itemlist.Add(new ItemData() { ID = acc.CAwardID, Num = acc.CAwardN });
            itemlist.Add(new ItemData() { ID = acc.DAwardID, Num = acc.DAwardN });

            UserHelper.RewardsItems(Current.UserId, itemlist);
            
            return true;
        }
    }
}