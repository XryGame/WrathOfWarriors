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
    /// 9020_请求领取在线奖励
    /// </summary>
    public class Action9020 : BaseAction
    {
        private bool receipt;
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
            
            var surface = new ShareCacheStruct<Config_OnlineReward>().FindKey(GetEventAward.OnlineAwardId);
            if (surface == null)
            {
                return true;
            }

            //DateTime startDate = GetEventAward.OnlineStartTime;
            //TimeSpan timeSpan = DateTime.Now.Subtract(startDate);
            //int sec = (int)Math.Floor(timeSpan.TotalSeconds);
            //int passsec = MathUtils.Addition(GetEventAward.TodayOnlineTime, sec, int.MaxValue);
            //if (passsec < surface.Time)
            //{
            //    receipt.Result = EventStatus.Bad;
            //    return true;
            //}

            //GetEventAward.OnlineStartTime = DateTime.Now;
            //GetEventAward.TodayOnlineTime = surface.Time;
            GetEventAward.OnlineAwardId++;
            GetEventAward.LastOnlineAwayReceiveTime = DateTime.Now;

            

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
                //                receipt.AwardItemList.AddRange(GetBasis.RandItem(surface.AwardNum));
                //            }
                //            else
                //            {// 技能
                //                receipt.AwardItemList.AddRange(GetBasis.RandSkillBook(surface.AwardNum));
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