using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 精灵卸载
    /// </summary>
    public class Action1172 : BaseAction
    {
        private bool receipt;

        public Action1172(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1172, actionGetter)
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
            GetElf.SelectID = 0;
            receipt = true;
            return true;
        }
    }
}