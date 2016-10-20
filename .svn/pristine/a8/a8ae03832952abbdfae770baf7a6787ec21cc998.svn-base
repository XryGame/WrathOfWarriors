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
    /// 9020_请求领取在线奖励
    /// </summary>
    public class Action9020 : BaseAction
    {
        private JPRequestSFOData receipt;
        private Random random = new Random();
        public Action9020(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action9020, actionGetter)
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


            var surface = new ShareCacheStruct<Config_OnlineReward>().FindKey(ContextUser.EventAwardData.OnlineAwardId);
            if (surface == null)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
           
            //DateTime startDate = ContextUser.EventAwardData.OnlineStartTime;
            //TimeSpan timeSpan = DateTime.Now.Subtract(startDate);
            //int sec = (int)Math.Floor(timeSpan.TotalSeconds);
            //int passsec = MathUtils.Addition(ContextUser.EventAwardData.TodayOnlineTime, sec, int.MaxValue);
            //if (passsec < surface.Time)
            //{
            //    receipt.Result = EventStatus.Bad;
            //    return true;
            //}


            ContextUser.EventAwardData.TodayOnlineTime = surface.Time;
            ContextUser.EventAwardData.OnlineAwardId++;
            ContextUser.EventAwardData.LastOnlineAwayReceiveTime = DateTime.Now;

            

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
                        if (random.Next(1000) < 500)
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