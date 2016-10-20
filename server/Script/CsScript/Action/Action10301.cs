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
    /// 10301_领取扫荡
    /// </summary>
    public class Action10301 : BaseAction
    {
        private JPReceiveTaskAwardData receipt;

        public Action10301(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10300, actionGetter)
        {

        }

        public override bool GetUrlElement()
        {
            return true;
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
            Config_Role role = new ShareCacheStruct<Config_Role>().FindKey(ContextUser.SweepingRoleId);
            if (role == null || ContextUser.SweepTimes <= 0)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }
            

            int addvalue = ContextUser.AdditionFightExpValue(role.Exp * ContextUser.SweepTimes);

            receipt = new JPReceiveTaskAwardData()
            {
                AwardExp = addvalue,
                CurrFightExp = ContextUser.FightExp,
                CurrLv = ContextUser.UserLv,
                CurrFightValue = ContextUser.FightingValue
            };
            object outexpdata;
            UserHelper.buildBaseExpData(ContextUser, out outexpdata);
            receipt.CurrBaseExp = outexpdata;
            
            ContextUser.SweepingRoleId = 0;
            ContextUser.SweepTimes = 0;

            return true;
        }
    }
}