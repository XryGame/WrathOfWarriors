using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
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
            
            var surface = new ShareCacheStruct<Config_Online>().FindKey(GetEventAward.OnlineAwardId);
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
            //GetEventAward.LastOnlineAwayReceiveTime = DateTime.Now;

            

            switch (surface.AwardType)
            {
                case TaskAwardType.Gold:
                    {
                        BigInteger resourceNum = BigInteger.Parse(surface.AwardNum);
                        BigInteger value = Math.Ceiling(GetBasis.UserLv / 50.0).ToInt() * resourceNum;
                        UserHelper.RewardsGold(Current.UserId, value);
                    }
                    break;
                case TaskAwardType.Diamond:
                    {
                        UserHelper.RewardsDiamond(Current.UserId, surface.AwardNum.ToInt());
                    }
                    break;
                case TaskAwardType.Item:
                    {
                        UserHelper.RewardsItem(Current.UserId, surface.AwardID, surface.AwardNum.ToInt());
                    }
                    break;
            }

            //receipt.CurrDiamond = GetBasis.DiamondNum;
            receipt = true;
            return true;
        }
    }
}