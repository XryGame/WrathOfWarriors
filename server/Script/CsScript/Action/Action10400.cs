using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10400_扫荡CD
    /// </summary>
    public class Action10400 : BaseAction
    {
        private int plotId;
        public Action10400(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10400, actionGetter)
        {
            IsNotRespond = true;
        }


        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("PlotId", ref plotId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            ContextUser.PlotId = plotId;
            return true;
        }

    }
}