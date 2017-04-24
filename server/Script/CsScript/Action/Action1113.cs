﻿using GameServer.CsScript.Base;
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
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    public class OfflineEarningsInfo
    {
        public long OfflineTimeSec { get; set; }

        public string OfflineEarnings { get; set; }
    }
    /// <summary>
    /// 1113_进入关卡
    /// </summary>
    public class Action1113 : BaseAction
    {
        private OfflineEarningsInfo receipt;
        
        public Action1113(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1113, actionGetter)
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
            receipt = new OfflineEarningsInfo();
            if (GetBasis.IsReceiveOfflineEarnings)
            {
                receipt.OfflineEarnings = "0";
                receipt.OfflineTimeSec = 0;
            }
            else
            {
                // 离线收益
                var transscriptSet = new ShareCacheStruct<Config_TeneralTranscript>();
                var transscriptCfg = transscriptSet.FindKey(GetBasis.UserLv);
                if (transscriptCfg.limitTime > 0)
                    transscriptCfg = transscriptSet.FindKey(GetBasis.UserLv - 1);
                if (transscriptCfg != null)
                {
                    BigInteger transscriptEarnings = 0;
                    var monster = new ShareCacheStruct<Config_Monster>().Find(t => t.Grade == transscriptCfg.ID);

                    BigInteger bi = Util.ConvertGameCoin(monster.DropoutGold) * 30;
                    transscriptEarnings += bi;

                    double rate = Convert.ToDouble(GetBasis.OfflineTimeSec / 1800.0);
                    int tmp = Convert.ToInt32(rate * 100);

                    var vipcfg = new ShareCacheStruct<Config_Vip>().FindKey(GetBasis.VipLv);
                    if (vipcfg != null)
                    {
                        GetBasis.OfflineEarnings = (transscriptEarnings * tmp / 100 * vipcfg.Multiple).ToNotNullString();
                    }
                    
                }
                receipt.OfflineTimeSec = GetBasis.OfflineTimeSec;
                receipt.OfflineEarnings = GetBasis.OfflineEarnings;
            }
            
            return true;
        }
    }
}