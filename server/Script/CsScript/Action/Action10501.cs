using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10501_领取体力
    /// </summary>
    public class Action10501 : BaseAction
    {
        private JPBuyData receipt;

        public Action10501(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10501, actionGetter)
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
            receipt = new JPBuyData();
            receipt.Result = EventStatus.Good;
            DateTime now = DateTime.Now;
            if (ContextUser.ReceiveVitStatus == ReceiveVitStatus.Eighteen)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }


            if (now.Hour >= 12 && now.Hour < 18)
            {
                if (ContextUser.ReceiveVitStatus != ReceiveVitStatus.No)
                {
                    receipt.Result = EventStatus.Bad;
                    return true;
                }

                ContextUser.ReceiveVitStatus = ReceiveVitStatus.Twelve;

            }
            else if (now.Hour >= 18 && now.Hour <= 23)
            {
                ContextUser.ReceiveVitStatus = ReceiveVitStatus.Eighteen;
            }
            else
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            ContextUser.Vit = MathUtils.Addition(ContextUser.Vit, ConfigEnvSet.GetInt("System.GiveawayVitValue"));

            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.Extend1 = ContextUser.Vit;
            return true;
        }
    }
}