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
        private bool receipt;
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

            var list = new ShareCacheStruct<Config_FirstWeek>().FindAll();
            if (GetEventAward.IsTodayReceiveFirstWeek || GetEventAward.FirstWeekCount >= list.Count)
            {
                return true;
            }

            var surface = list.Find(t => (
                t.ID == GetEventAward.FirstWeekCount + 1
            ));
            if (surface == null)
            {
                return true;
            }

            GetEventAward.IsTodayReceiveFirstWeek = true;
            GetEventAward.FirstWeekCount++;

            
            
            switch (surface.AwardType)
            {
                case TaskAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, surface.AwardNum);
                        //receipt.AwardDiamondNum = surface.AwardNum;
                        //receipt.CurrDiamond = GetBasis.DiamondNum;
                    }
                    break;
                //case AwardType.ItemSkillBook:
                //    {
                //        Config_Item item = new ShareCacheStruct<Config_Item>().FindKey(surface.AwardID);
                //        if (item != null)
                //        {
                //            GetPackage.AddItem(surface.AwardID, surface.AwardNum);
                            
                //            receipt.AwardItemList.Add(surface.AwardID);
                //        }
                //    }
                //    break;
                //case AwardType.RandItemSkillBook:
                //    {
                //        int count = surface.AwardNum;
                //        while (count > 0)
                //        {
                //            count--;
                //            if (random.Next(1000) < 750)
                //            {// 道具
                //                receipt.AwardItemList.AddRange(GetBasis.RandItem(1));
                //            }
                //            else
                //            {// 技能
                //                receipt.AwardItemList.AddRange(GetBasis.RandSkillBook(1));
                //            }
                //        }
                //    }
                //    break;
            }
            //receipt.CurrDiamond = GetBasis.DiamondNum;
            receipt = true;
            return true;
        }
    }
}