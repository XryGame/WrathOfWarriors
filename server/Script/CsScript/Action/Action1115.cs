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
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 请求通关关卡
    /// </summary>
    public class Action1115 : BaseAction
    {
        private bool receipt;
        public Action1115(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1115, actionGetter)
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
            if (GetBasis.Vit > 0)
            {
                receipt = true;
                GetBasis.Vit--;
            }
            else
            {
                receipt = false;
            }
            PushMessageHelper.UserVitChangedNotification(Current);
            return true;
        }
    }
}