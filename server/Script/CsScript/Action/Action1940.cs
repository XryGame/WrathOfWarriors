using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class ReceiveVitReceipt
    {
        public bool Result { get; set; }

        public ReceiveVitStatus ReceiveVitStatusFlag { get; set; }

        public int ReceiveNum { get; set; }
    }

    /// <summary>
    /// 领取体力
    /// </summary>
    public class Action1940 : BaseAction
    {
        private ReceiveVitReceipt receipt;
        private int comboNum;
        public Action1940(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1940, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
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
            receipt = new ReceiveVitReceipt();
            receipt.Result = true;
            DateTime now = DateTime.Now;
            if (GetEventAward.ReceiveVitStatusFlag == ReceiveVitStatus.Eighteen)
            {
                receipt.Result = false;
                return true;
            }


            if (now.Hour >= 12 && now.Hour < 17)
            {
                if (GetEventAward.ReceiveVitStatusFlag != ReceiveVitStatus.No)
                {
                    receipt.Result = false;
                    return true;
                }

                GetEventAward.ReceiveVitStatusFlag = ReceiveVitStatus.Twelve;

            }
            else if (now.Hour >= 18 && now.Hour <= 23)
            {
                GetEventAward.ReceiveVitStatusFlag = ReceiveVitStatus.Eighteen;
            }
            else
            {
                receipt.Result = false;
                return true;
            }

            if (comboNum > GetBasis.ComboNum)
            {
                GetBasis.ComboNum = comboNum;
                var combo = UserHelper.FindRankUser(Current.UserId, RankType.Combo);
                if (combo != null) combo.ComboNum = comboNum;
            }

            int receiveNum = Math.Max(comboNum / 2, DataHelper.VitRestore);
            receiveNum = Math.Min(receiveNum, 40);
            UserHelper.RewardsVit(Current.UserId, receiveNum);

            receipt.ReceiveVitStatusFlag = GetEventAward.ReceiveVitStatusFlag;
            receipt.ReceiveNum = receiveNum;
            return true;
        }
    }
}