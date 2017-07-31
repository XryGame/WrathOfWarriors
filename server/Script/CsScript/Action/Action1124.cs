
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Numerics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1124_装备升级
    /// </summary>
    public class Action1124 : BaseAction
    {
        private bool receipt;
        private EquipID equipID;

        public Action1124(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1124, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("EquipID", ref equipID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            EquipData equip = GetEquips.FindEquipData(equipID);

            

            if (GetBasis.UserLv <= equip.Lv)
            {
                return false;
            }

            if (equip.Lv - GetEquips.Weapon.Lv >= 3
                || equip.Lv - GetEquips.Coat.Lv >= 3
                || equip.Lv - GetEquips.Ring.Lv >= 3
                || equip.Lv - GetEquips.Shoe.Lv >= 3
                || equip.Lv - GetEquips.Accessory.Lv >= 3)
            {
                return false;
            }

            var equipcfg = new ShareCacheStruct<Config_Equip>().Find(t => (t.EquipID == equip.ID && t.Grade == (equip.Lv + 1)));
            if (equipcfg == null)
            {
                return false;
            }
            BigInteger gradeConsumeGold = BigInteger.Parse(equipcfg.GradeConsumeGold);
            if (GetBasis.GoldNum < gradeConsumeGold || GetBasis.DiamondNum < equipcfg.GradeConsumediamond)
            {
                return false;
            }
            //var nextEquipcfg = new ShareCacheStruct<Config_Equip>().Find(t => (t.EquipID == equip.ID && t.Grade == equip.Lv+1));
            //if (nextEquipcfg == null)
            //{
            //    return false;
            //}

            if (gradeConsumeGold > 0)
            {
                UserHelper.ConsumeGold(Current.UserId, equipcfg.GradeConsumeGold);
            }

            if (equipcfg.GradeConsumediamond > 0)
            {
                UserHelper.ConsumeDiamond(Current.UserId, equipcfg.GradeConsumediamond);
            }

            equip.Lv++;
            UserHelper.RefreshUserFightValue(Current.UserId);

            // 每日
            UserHelper.EveryDayTaskProcess(Current.UserId, TaskType.UpgradeEquip, 1);

            // 成就
            UserHelper.AchievementProcess(Current.UserId, AchievementType.UpgradeEquip);

            receipt = true;
            return true;
        }
    }
}