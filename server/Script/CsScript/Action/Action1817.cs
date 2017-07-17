using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 
    /// </summary>
    public class Action1817 : BaseAction
    {

        public Action1817(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1817, actionGetter)
        {
            IsNotRespond = true;
        }

        protected override string BuildJsonPack()
        {
            body = null;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            GetEnemys.IsHaveNewLog = false;

            return true;
        }
    }
}