using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
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
    /// 1110_掉落金币
    /// </summary>
    public class Action1110 : BaseAction
    {
        private bool receipt;

        private string goldNum;

        private string dropTime;

        public Action1110(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1110, actionGetter)
        {
            IsNotRespond = true;
        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("GoldNum", ref goldNum)
                && httpGet.GetString("Time", ref dropTime))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            
     
            var monster = new ShareCacheStruct<Config_Monster>().Find(t => t.Grade == GetBasis.UserLv);
            if (monster == null)
                return false;
            

            BigInteger dropGold = BigInteger.Parse(monster.DropoutGold);
            BigInteger uploadingGold = BigInteger.Parse(goldNum);


            bool isDataError = false;
            if (dropGold != uploadingGold)
            {
                if (GetBasis.UserLv % 5 == 0)
                {
                    var lastmonster = new ShareCacheStruct<Config_Monster>().Find(t => t.Grade == GetBasis.UserLv - 1);
                    if (lastmonster != null)
                    {
                        dropGold = BigInteger.Parse(lastmonster.DropoutGold);
                        if (dropGold != uploadingGold)
                        {
                            isDataError = true;
                        }
                    }
                }
                else
                {
                    isDataError = true;
                }
            }

            if (!isDataError)
            {
                DateTime dropDate = Util.GetTime(dropTime);
                if (GetBasis.LastDropGoldTime == DateTime.MinValue)
                {
                    GetBasis.LastDropGoldTime = dropDate;
                }
                else
                {
                    var timespan = dropDate.Subtract(GetBasis.LastDropGoldTime);
                    double sec = timespan.TotalSeconds;
                    if (sec >= 3)
                    {
                        GetBasis.LastDropGoldTime = dropDate;
                        GetBasis.DropGoldIntervalCount = 1;
                    }
                    else
                    {
                        GetBasis.DropGoldIntervalCount++;
                        if (GetBasis.DropGoldIntervalCount > 6)
                        {
                            isDataError = true;
                        }
                    }
                }
            }

            if (isDataError)
            {
                
                GetBasis.DropGoldIntervalCount = 0;
                PushMessageHelper.UserGameDataExceptionNotification(Current);
                return false;
            }


            UserHelper.RewardsGold(Current.UserId, monster.DropoutGold, UpdateCoinOperate.KillMonsterReward, true);
            receipt = true;
            return true;
        }
    }
}