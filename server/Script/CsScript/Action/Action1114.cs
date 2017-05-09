using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1114_领取离线收益
    /// </summary>
    public class Action1114 : BaseAction
    {
        private bool receipt =false;
        
        public Action1114(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1114, actionGetter)
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
            if (GetBasis.IsReceiveOfflineEarnings)
            {
                return false;
            }

            BigInteger bi = BigInteger.Parse(GetBasis.OfflineEarnings);
            UserHelper.RewardsGold(Current.UserId, bi, UpdateCoinOperate.OffineReward);
            GetBasis.OfflineEarnings = "0";
            GetBasis.IsReceiveOfflineEarnings = true;
            GetBasis.OfflineTimeSec = 0;
            receipt = true;
            return true;
        }
    }
}