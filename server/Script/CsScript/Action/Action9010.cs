using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 9010_请求领取首周奖励
    /// </summary>
    public class Action9010 : BaseAction
    {
        private JPRequestSFOData receipt;
        private Random random = new Random();
        public Action9010(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action9010, actionGetter)
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
            receipt = new JPRequestSFOData();
            receipt.Result = EventStatus.Good;
            var list = new ShareCacheStruct<Config_FirstWeek>().FindAll();
            if (ContextUser.EventAwardData.IsTodayReceiveFirstWeek || ContextUser.EventAwardData.FirstWeekCount >= list.Count)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            var surface = list.Find(t => (
                t.ID == ContextUser.EventAwardData.FirstWeekCount + 1
            ));
            if (surface == null)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }

            ContextUser.EventAwardData.IsTodayReceiveFirstWeek = true;
            ContextUser.EventAwardData.FirstWeekCount++;

            
            
            switch (surface.AwardType)
            {
                case AwardType.Diamond:
                    {
                        UserHelper.GiveAwayDiamond(ContextUser.UserID, surface.AwardNum);
                        receipt.AwardDiamondNum = surface.AwardNum;
                        receipt.CurrDiamond = ContextUser.DiamondNum;
                    }
                    break;
                case AwardType.ItemSkillBook:
                    {
                        Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(surface.AwardID);
                        if (item != null)
                        {
                            ContextUser.UserAddItem(surface.AwardID, surface.AwardNum);

                            if (item.Type == ItemType.Skill)
                            {
                                ContextUser.CheckAddSkillBook(surface.AwardID, surface.AwardNum);
                            }
                            receipt.AwardItemList.Add(surface.AwardID);
                        }
                    }
                    break;
                case AwardType.RandItemSkillBook:
                    {
                        if (random.Next(1000) < 750)
                        {// 道具
                            receipt.AwardItemList = ContextUser.RandItem(surface.AwardNum);
                        }
                        else
                        {// 技能
                            receipt.AwardItemList = ContextUser.RandSkillBook(surface.AwardNum);
                        }
                    }
                    break;
            }
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.ItemList = ContextUser.ItemDataList;
            receipt.SkillList = ContextUser.SkillDataList;
            return true;
        }
    }
}