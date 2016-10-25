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
    /// 10302_扫荡CD
    /// </summary>
    public class Action10302 : BaseAction
    {
        private EventStatus receipt = EventStatus.Bad;
        public Action10302(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10302, actionGetter)
        {

        }


        public override bool GetUrlElement()
        {
            return true;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {


            if (ContextUser.SweepingRoleId <= 0
                || ContextUser.SweepTimes <= 0 
                || DateTime.Now < ContextUser.StartSweepTime)
            {
                return false;
            }

            Config_Role role = new ShareCacheStruct<Config_Role>().FindKey(ContextUser.SweepingRoleId);
            if (role == null || ContextUser.SweepTimes <= 0)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }

            int mins = 0;
            int sec = ConfigEnvSet.GetInt("User.SweepCD") * ContextUser.SweepTimes;
            DateTime endtime = ContextUser.StartSweepTime.AddSeconds(sec);
            if (DateTime.Now >= endtime)
            {
                mins = (int)endtime.Subtract(ContextUser.StartSweepTime).TotalMinutes;
            }
            else
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ContextUser.StartSweepTime);
                int tmpmin = sec / 60;
                mins = (int)MathUtils.Subtraction(tmpmin, timeSpan.TotalMinutes, 0);
            }

            if (mins == 0)
                return false;

            if (ContextUser.DiamondNum < mins)
                return false;

            ContextUser.StartSweepTime = DateTime.MinValue;
            receipt = EventStatus.Good;

            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, (int)mins);
            return true;
        }

    }
}