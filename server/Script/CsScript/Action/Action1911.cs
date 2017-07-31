﻿using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class RequestAccumulateConsume
    {
        public RequestAccumulateConsume()
        {
            AwardItemList = new List<ItemData>();
        }

        public int ReceiveId { get; set; }

        public ReceiveAccumulatePayResult Result { get; set; }


        public List<ItemData> AwardItemList { get; set; }


    }

    /// <summary>
    /// 领取累耗
    /// </summary>
    public class Action1911 : BaseAction
    {
        private RequestAccumulateConsume receipt;
        private Random random = new Random();
        private int receiveId;
        public Action1911(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1911, actionGetter)
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
            receipt = new RequestAccumulateConsume();
            receipt.ReceiveId = receiveId;
            receipt.Result = ReceiveAccumulatePayResult.Ok;

            if (GetPay.AccumulateConsumeList.Find(t => (t == receiveId)) != 0)
            {
                receipt.Result = ReceiveAccumulatePayResult.Received;
                return true;
            }
            var acc = new ShareCacheStruct<Config_AccumulatePay>().FindKey(receiveId);
            if (acc == null || acc.Type != AccumulatePayType.Consume)
            {
                return false;
            }
           
            if (GetBasis.UsedDiamond < acc.Time)
            {
                receipt.Result = ReceiveAccumulatePayResult.NoPay;
                return true;
            }

            GetPay.AccumulateConsumeList.Add(receiveId);


            if (acc.AAwardID > 0 && acc.AAwardN > 0)
                receipt.AwardItemList.Add(new ItemData() { ID = acc.AAwardID, Num = acc.AAwardN });
            if (acc.BAwardID > 0 && acc.BAwardN > 0)
                receipt.AwardItemList.Add(new ItemData() { ID = acc.BAwardID, Num = acc.BAwardN });
            if (acc.CAwardID > 0 && acc.CAwardN > 0)
                receipt.AwardItemList.Add(new ItemData() { ID = acc.CAwardID, Num = acc.CAwardN });
            if (acc.DAwardID > 0 && acc.DAwardN > 0)
                receipt.AwardItemList.Add(new ItemData() { ID = acc.DAwardID, Num = acc.DAwardN });


            UserHelper.RewardsItems(Current.UserId, receipt.AwardItemList);
            //foreach (var it in receipt.AwardItemList)
            //{
            //    Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(it.ID);
            //    if (item != null)
            //    {
            //        GetPackage.AddItem(it.ID, it.Num);
            //    }
            //}

            return true;
        }
    }
}