using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1120_宝石镶嵌
    /// </summary>
    public class Action1120 : BaseAction
    {
        private bool receipt;
        private EquipID equipID;
        private int gemID;

        public Action1120(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1120, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("EquipID", ref equipID)
                && httpGet.GetInt("GemID", ref gemID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(gemID);
            if (itemcfg.ItemType != ItemType.Gem)
                return false;
            EquipData equip = GetEquips.FindEquipData(equipID);


            switch ((GemType)itemcfg.Species)
            {
                case GemType.Attack:
                    {
                        if (equip.AtkGem != 0)
                        {
                            return false;
                        }
                        equip.AtkGem = gemID;
                    }
                    break;
                case GemType.Defense:
                    {
                        if (equip.DefGem != 0)
                        {
                            return false;
                        }
                        equip.DefGem = gemID;
                    }
                    break;
                case GemType.Hp:
                    {
                        if (equip.HpGem != 0)
                        {
                            return false;
                        }
                        equip.HpGem = gemID;
                    }
                    break;
                case GemType.Crit:
                    {
                        if (equip.CritGem != 0)
                        {
                            return false;
                        }
                        equip.CritGem = gemID;
                    }
                    break;
                case GemType.Hit:
                    {
                        if (equip.HitGem != 0)
                        {
                            return false;
                        }
                        equip.HitGem = gemID;
                    }
                    break;
                case GemType.Dodge:
                    {
                        if (equip.DodgeGem != 0)
                        {
                            return false;
                        }
                        equip.DodgeGem = gemID;
                    }
                    break;
                case GemType.Tenacity:
                    {
                        if (equip.TenacityGem != 0)
                        {
                            return false;
                        }
                        equip.TenacityGem = gemID;
                    }
                    break;
            }
            GetPackage.RemoveItem(gemID, 1);

            UserHelper.RefreshUserFightValue(Current.UserId);

            // 成就
            UserHelper.AchievementProcess(GetBasis.UserID, AchievementType.InlayGem, "1", gemID);

            receipt = true;
            return true;
        }
    }
}