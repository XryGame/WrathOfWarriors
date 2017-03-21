using GameServer.CsScript.Base;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1130_开启战魂
    /// </summary>
    public class Action1130 : BaseAction
    {
        private int receipt;
        private int openId;

        public Action1130(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1130, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("OpenID", ref openId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var soulSet = new ShareCacheStruct<Config_Soulstrong>();
            var soulcfg = soulSet.FindKey(openId);
            if (soulcfg == null || soulcfg.Soulid != GetSoul.SoulID)
            {
                return false;
            }

            if (GetSoul.OpenList.Find(t => t == openId) != 0)
            {
                return false;
            }

            
            switch (soulcfg.ConsumeType)
            {
                case ConsumeType.Gold:
                    {
                        BigInteger consumeNum = Util.ConvertGameCoin(soulcfg.ConsumeNum);
                        if (GetBasis.GoldNum < consumeNum)
                        {
                            return false;
                        }
                        else
                        {
                            UserHelper.ConsumeGold(Current.UserId, consumeNum);
                        }
                    }
                    break;
                case ConsumeType.Diamond:
                    {
                        if (GetBasis.DiamondNum < Convert.ToInt32(soulcfg.ConsumeNum))
                        {
                            return false;
                        }
                        else
                        {
                            UserHelper.ConsumeDiamond(Current.UserId, Convert.ToInt32(soulcfg.ConsumeNum));
                        }
                    }
                    break;
            }


            GetSoul.OpenList.Add(openId);


            UserHelper.RefreshUserFightValue(Current.UserId);

            if (soulSet.Find(t => (t.Soulid == (GetSoul.SoulID + 1))) != null)
            {
                var list = soulSet.FindAll(t => (t.Soulid == GetSoul.SoulID));
                if (GetSoul.OpenList.Count >= list.Count)
                {
                    GetSoul.SoulID++;
                    GetSoul.OpenList.Clear();
                }
            }

            receipt = GetSoul.SoulID;

            // 每日
            UserHelper.EveryDayTaskProcess(GetBasis.UserID, TaskType.OpenSoul, 1);

            // 成就
            UserHelper.AchievementProcess(GetBasis.UserID, AchievementType.OpenSoul);

            return true;
        }
    }
}