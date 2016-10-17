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
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10200_领取首充奖励
    /// </summary>
    public class Action10200 : BaseAction
    {
        private JPRequestSFOData receipt;
        
        public Action10200(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10200, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action10200;
            }
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            UserPayCache usepay = UserHelper.FindUserPay(ContextUser.UserID);
            if (usepay == null || usepay.PayMoney == 0)
                return false;
            receipt = new JPRequestSFOData();
            receipt.Result = EventStatus.Bad;
            if (usepay.IsReceiveFirstPay)
                return true;
            usepay.IsReceiveFirstPay = true;
            receipt.Result = EventStatus.Good;

            UserHelper.GiveAwayDiamond(ContextUser.UserID, 200);
            Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(10003);
            if (item != null)
            {
                ContextUser.UserAddItem(10003, 1);

                if (item.Type == ItemType.Skill)
                {
                    ItemData itemdata = ContextUser.ItemDataList.Find(t => (t.ID == 10003));
                    if (itemdata != null)
                    {
                        ContextUser.UserAddSkill(10003, 1);
                    }
                }
            }
            item = new ShareCacheStruct<Config_Item>().FindKey(20003);
            if (item != null)
            {
                ContextUser.UserAddItem(20003, 1);

                if (item.Type == ItemType.Skill)
                {
                    ItemData itemdata = ContextUser.ItemDataList.Find(t => (t.ID == 20003));
                    if (itemdata != null)
                    {
                        ContextUser.UserAddSkill(20003, 1);
                    }
                }
            }

            receipt.AwardDiamondNum = 200;
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.AwardItemList.Add(10003);
            receipt.AwardItemList.Add(20003);
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;

            return true;
        }
    }
}