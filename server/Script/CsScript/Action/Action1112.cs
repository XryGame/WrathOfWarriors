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
    /// 1112_再次挑战
    /// </summary>
    public class Action1112 : BaseAction
    {
        private bool receipt = false;

        private int _Diamond;
        public Action1112(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1112, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Diamond", ref _Diamond))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            if (GetBasis.DiamondNum < _Diamond)
            {
                return false;
            }
            UserHelper.ConsumeDiamond(Current.UserId, _Diamond);

            receipt = true;

            return true;
        }
    }
}