using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 领取累充
    /// </summary>
    public class Action1910 : BaseAction
    {
        private JPRequestAccumulatePay receipt;
        private Random random = new Random();
        private int receiveId;
        public Action1910(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1910, actionGetter)
        {

        }
        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ReceiveId", ref receiveId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPRequestAccumulatePay();
            receipt.ReceiveId = receiveId;
            receipt.Result = ReceiveAccumulatePayResult.Ok;

            if (GetPay.AccumulatePayList.Find(t => (t == receiveId)) != 0)
            {
                receipt.Result = ReceiveAccumulatePayResult.Received;
                return true;
            }
            var acc = new ShareCacheStruct<Config_AccumulatePay>().FindKey(receiveId);
            if (acc == null)
            {
                return false;
            }
           
            if (GetBasis.BuyDiamond < acc.Time)
            {
                receipt.Result = ReceiveAccumulatePayResult.NoDiamond;
                return true;
            }

            GetPay.AccumulatePayList.Add(receiveId);
            int randcount = 0;
            List<int> itemlist = new List<int>();
            int diamond = 0;
            if (acc.AwardA == 1) randcount++;
            else if (acc.AwardA >= 10000) itemlist.Add(acc.AwardA);
            else diamond += acc.AwardA;
            if (acc.AwardB == 1) randcount++;
            else if (acc.AwardB >= 10000) itemlist.Add(acc.AwardB);
            else diamond += acc.AwardB;
            if (acc.AwardC == 1) randcount++;
            else if (acc.AwardC >= 10000) itemlist.Add(acc.AwardC);
            else diamond += acc.AwardC;
            if (acc.AwardD == 1) randcount++;
            else if (acc.AwardD >= 10000) itemlist.Add(acc.AwardD);
            else diamond += acc.AwardD;

            //for (int i = 0; i < randcount; ++i)
            //{
            //    receipt.AwardItemList.AddRange(GetBasis.RandItem(1));
            //}

            //foreach (var it in itemlist)
            //{
            //    Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(it);
            //    if (item != null)
            //    {
            //        GetPackage.AddItem(it, 1);

            //        receipt.AwardItemList.Add(it);
            //    }
            //}

            //if (diamond > 0)
            //{
            //    UserHelper.RewardsDiamond(GetBasis.UserID, diamond);
            //    receipt.AwardDiamondNum = diamond;
                
            //}
            //receipt.CurrDiamond = GetBasis.DiamondNum;
            //receipt.ItemList = GetBasis.ItemDataList;
            //receipt.SkillList = GetBasis.SkillDataList;

            return true;
        }
    }
}