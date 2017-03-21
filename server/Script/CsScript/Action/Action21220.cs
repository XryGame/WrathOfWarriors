using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 21220_争霸赛下注
    /// </summary>
    public class Action21220 : BaseAction
    {
        private JPCompetition64BetData receipt;
        private int diamond;
        private int destUserId;
        public Action21220(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action21210, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Count", ref diamond)
                && httpGet.GetInt("DestUserId", ref destUserId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPCompetition64BetData();

            if (SystemGlobal.competition64.Stage != Com.CompetitionStage.F4)
            {
                receipt.result = Competition64BetReset.NoBetTime;
                return true;
            }

            if (GetBasis.DiamondNum < diamond)
            {
                receipt.result = Competition64BetReset.NoDiamond;
                return true;
            }

            if (SystemGlobal.competition64.findBetData(GetBasis.UserID) != null)
            {
                receipt.result = Competition64BetReset.HadBet;
                return true;
            }
            
            UserHelper.ConsumeDiamond(Current.UserId, diamond);

            SystemGlobal.competition64.NewBetData(GetBasis.UserID, diamond, destUserId);

            receipt.result = Competition64BetReset.OK;
            receipt.info = SystemGlobal.competition64.findBetData(GetBasis.UserID);
            return true;
        }
    }
}