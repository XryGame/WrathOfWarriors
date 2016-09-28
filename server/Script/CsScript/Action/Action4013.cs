using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4013_购买选票
    /// </summary>
    public class Action4013 : BaseAction
    {
        private JPBuyData receipt;
        private int votecount;

        public Action4013(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4013, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Count", ref votecount))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            
            if (ContextUser.DiamondNum < votecount)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            int canbuynum = MathUtils.Subtraction(
                ConfigEnvSet.GetInt("User.BuyCampaignTicketNumMax"), ContextUser.BuyCampaignTicketNum, 0
                );
            if (canbuynum < votecount)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
                    
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, votecount);
            ContextUser.BuyCampaignTicketNum = MathUtils.Addition(ContextUser.BuyCampaignTicketNum, votecount);
            ContextUser.CampaignTicketNum = MathUtils.Addition(ContextUser.CampaignTicketNum, votecount);

            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.Extend1 = ContextUser.CampaignTicketNum;
            receipt.Extend2 = MathUtils.Subtraction(
                ConfigEnvSet.GetInt("User.BuyCampaignTicketNumMax"), ContextUser.BuyCampaignTicketNum, 0
                );
            return true;
        }
    }
}