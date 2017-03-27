using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1110_掉落金币
    /// </summary>
    public class Action1110 : BaseAction
    {
        private bool receipt;

        private string goldNum;

        public Action1110(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1110, actionGetter)
        {
            IsNotRespond = true;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("GoldNum", ref goldNum))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            UserHelper.RewardsGold(Current.UserId, goldNum);
            receipt = true;
            return true;
        }
    }
}