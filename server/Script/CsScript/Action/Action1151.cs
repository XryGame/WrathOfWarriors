using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1151_领取活跃度宝箱
    /// </summary>
    public class Action1151 : BaseAction
    {
        private bool receipt;
        private int id;
        private Random random = new Random();

        public Action1151(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1151, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("ID", ref id))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var livenesscfg = new ShareCacheStruct<Config_Liveness>().FindKey(id);
            if (livenesscfg == null)
            {
                return false;
            }
            
            if (GetTask.Liveness < livenesscfg.Liveness
                || GetTask.ReceiveBoxList.Find(t => t == id) != 0)
            {
                return false;
            }

            UserHelper.RewardsItem(Current.UserId, livenesscfg.ItemID, 10);
            GetTask.ReceiveBoxList.Add(id);

            receipt = true;
            return true;
            
        }
    }
}