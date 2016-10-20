using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10300_请求扫荡
    /// </summary>
    public class Action10300 : BaseAction
    {
        private int monsterId;
        private int count;
        private JPRequestSweepData receipt;

        public Action10300(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10300, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("MonsterId", ref monsterId)
                && httpGet.GetInt("Count", ref count))
            {
                return true;
            }
            return false;
        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action10300;
            }

            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestSweepData();
            receipt.Result = EventStatus.Bad;
        
            Config_Role role = new ShareCacheStruct<Config_Role>().FindKey(monsterId);

            if (role == null || count <= 0)
            {
                return true;
            }

            if (ContextUser.Vit < role.Time * count)
            {
                return true;
            }

            ContextUser.Vit = MathUtils.Subtraction(ContextUser.Vit, role.Time * count, 0);
            ContextUser.SweepingRoleId = monsterId;
            ContextUser.SweepTimes = count;
            ContextUser.StartSweepTime = DateTime.Now;

            receipt.Result = EventStatus.Good;
            receipt.CurrVit = ContextUser.Vit;
            receipt.SweepingRoleId = ContextUser.SweepingRoleId;
            receipt.SweepTimes = ContextUser.SweepTimes;
            receipt.StartSweepTime = Util.ConvertDateTimeStamp(ContextUser.StartSweepTime);
            return true;
        }
    }
}