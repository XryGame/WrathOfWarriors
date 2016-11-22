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
    /// 10700_领取累充
    /// </summary>
    public class Action10700 : BaseAction
    {
        private JPRequestAccumulatePay receipt;
        private Random random = new Random();
        private int receiveId;
        public Action10700(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10700, actionGetter)
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

            if (ContextUser.AccumulatePayList.Find(t => (t == receiveId)) == receiveId)
            {
                receipt.Result = ReceiveAccumulatePayResult.Received;
                return true;
            }
            var acc = new ShareCacheStruct<Config_AccumulatePay>().FindKey(receiveId);
            if (acc == null)
            {
                return false;
            }
           
            if (ContextUser.BuyDiamond < acc.Time)
            {
                receipt.Result = ReceiveAccumulatePayResult.NoDiamond;
                return true;
            }

            ContextUser.AccumulatePayList.Add(receiveId);
            int randcount = 0;
            List<int> itemlist = new List<int>();
            if (acc.AwardA == 1) randcount++;
            else if (acc.AwardA >= 10000) itemlist.Add(acc.AwardA);
            if (acc.AwardB == 1) randcount++;
            else if (acc.AwardB >= 10000) itemlist.Add(acc.AwardB);
            if (acc.AwardC == 1) randcount++;
            else if (acc.AwardC >= 10000) itemlist.Add(acc.AwardC);
            if (acc.AwardD == 1) randcount++;
            else if (acc.AwardD >= 10000) itemlist.Add(acc.AwardD);

            for (int i = 0; i < randcount; ++i)
            {
                if (random.Next(1000) < 500)
                {// 道具
                    receipt.AwardItemList.AddRange(ContextUser.RandItem(1));
                }
                else
                {// 技能
                    receipt.AwardItemList.AddRange(ContextUser.RandSkillBook(1));
                }
            }

            foreach (var it in itemlist)
            {
                Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(it);
                if (item != null)
                {
                    ContextUser.UserAddItem(it, 1);

                    if (item.Type == ItemType.Skill)
                    {
                        ContextUser.CheckAddSkillBook(it, 1);
                    }
                    receipt.AwardItemList.Add(it);
                }
            }
            
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;

            return true;
        }
    }
}