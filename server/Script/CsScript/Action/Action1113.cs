using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 连击结束
    /// </summary>
    public class Action1113 : BaseAction
    {
        private int comboNum;

        public Action1113(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1113, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = true;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ComboNum", ref comboNum))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            if (comboNum > GetBasis.ComboNum)
            {
                GetBasis.ComboNum = comboNum;
                var combo = UserHelper.FindRankUser(Current.UserId, RankType.Combo);
                if (combo != null) combo.ComboNum = comboNum;
            }
            return true;
        }
    }
}