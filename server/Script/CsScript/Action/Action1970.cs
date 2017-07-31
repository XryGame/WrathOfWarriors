using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 领取等级奖励 
    /// </summary>
    public class Action1970 : BaseAction
    {
        private bool receipt;
        private int id;
        public Action1970(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1970, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref id))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = false;
            if (GetBasis.ReceiveLevelAwardList.Find( t => t == id) != 0)
            {
                return true;
            }

            var gradeSet = new ShareCacheStruct<Config_Grade>();
            var gradecfg = gradeSet.FindKey(id);
            if (gradecfg == null)
            {
                return false;
            }
            if (GetBasis.UserLv < gradecfg.Grade)
            {
                return true;
            }
            GetBasis.ReceiveLevelAwardList.Add(id);

            List<ItemData> itemlist = new List<ItemData>();
            itemlist.Add(new ItemData() { ID = gradecfg.AAwardID, Num = gradecfg.AAwardN });
            itemlist.Add(new ItemData() { ID = gradecfg.BAwardID, Num = gradecfg.BAwardN });
            itemlist.Add(new ItemData() { ID = gradecfg.CAwardID, Num = gradecfg.CAwardN });
            itemlist.Add(new ItemData() { ID = gradecfg.DAwardID, Num = gradecfg.DAwardN });

            UserHelper.RewardsItems(Current.UserId, itemlist);

            receipt = true;
            return true;
        }
    }
}