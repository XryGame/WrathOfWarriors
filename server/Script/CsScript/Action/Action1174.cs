using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 精灵查看标记
    /// </summary>
    public class Action1174 : BaseAction
    {
        private bool receipt;

        public Action1174(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1174, actionGetter)
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
            return true;
        }

        public override bool TakeAction()
        {
            var list = GetElf.ElfList.FindAll(t => t.IsNew == true);
            foreach (var v in list)
            {
                v.IsNew = false;
            }
            receipt = true;
            return true;
        }
    }
}