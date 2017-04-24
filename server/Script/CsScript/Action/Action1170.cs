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
    /// 精灵出战
    /// </summary>
    public class Action1170 : BaseAction
    {
        private bool receipt;
        private int elfId;

        public Action1170(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1170, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ElfID", ref elfId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var elf = GetElf.FindElf(elfId);
            if (elf == null)
            {
                return false;
            }

            var elfSet = new ShareCacheStruct<Config_Elves>();
            var elfcfg = elfSet.Find(t => (t.ElvesID == elf.ID && t.ElvesGrade == elf.Lv));
            if (elfcfg == null)
            {
                return false;
            }



            var elfNextCfg = elfSet.Find(t => (t.ElvesID == elf.ID && t.ElvesGrade == elf.Lv + 1));
            if (elfNextCfg == null)
            {
                return false;
            }
            
            BigInteger consumeNumber = Util.ConvertGameCoin(elfNextCfg.GradeConsume);
            if (GetBasis.GoldNum < consumeNumber)
            {
                return false;
            }
            else
            {
                UserHelper.ConsumeGold(Current.UserId, consumeNumber);
            }


            elf.Lv = elfNextCfg.ElvesGrade;

            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.UpgradeElf, 1);

            // 成就
            UserHelper.AchievementProcess(Current.UserId, AchievementType.UpgradeElf);

            receipt = true;
            return true;
        }
    }
}