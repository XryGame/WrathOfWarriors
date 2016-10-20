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
    /// 10100_请求抽奖
    /// </summary>
    public class Action10100 : BaseAction
    {
        private JPLotteryData receipt;
        
        public Action10100(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10100, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action10100;
            }
            return base.BuildJsonPack();
        }

        public override bool TakeAction()
        {
            if (ContextUser.IsTodayLottery || ContextUser.RandomLotteryId == 0)
                return false;

            Config_Lottery lott = new ShareCacheStruct<Config_Lottery>().FindKey(ContextUser.RandomLotteryId);

            if (lott == null)
                return false;

            ContextUser.IsTodayLottery = true;

            receipt = new JPLotteryData();
            receipt.Type = lott.Type;
            switch (lott.Type)
            {
                case LotteryAwardType.Diamond:
                    {
                        UserHelper.GiveAwayDiamond(ContextUser.UserID, lott.Content);
                        receipt.AwardNum = lott.Content;
                    }
                    break;
                case LotteryAwardType.Item:
                    {
                        Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(lott.Content);
                        if (item != null)
                        {
                            ContextUser.UserAddItem(lott.Content, 1);

                            if (item.Type == ItemType.Skill)
                            {
                                ContextUser.CheckAddSkillBook(lott.Content, 1);
                            }
                            receipt.AwardItemId = lott.Content;
                            receipt.AwardNum = 1;
                        }
                    }
                    break;
            }
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;

            return true;
        }
    }
}