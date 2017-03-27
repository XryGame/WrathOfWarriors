using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1123_一键宝石镶嵌
    /// </summary>
    public class Action1122 : BaseAction
    {
        private bool receipt;
        private EquipID equipID;
        private int atkGem, defGem, hpGem, critGem, hitGem, dodgeGem, tenacityGem;

        public Action1122(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1121, actionGetter)
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
                && httpGet.GetInt("AtkGem", ref atkGem)
                && httpGet.GetInt("DefGem", ref defGem)
                && httpGet.GetInt("HpGem", ref hpGem)
                && httpGet.GetInt("CritGem", ref critGem)
                && httpGet.GetInt("HitGem", ref hitGem)
                && httpGet.GetInt("DodgeGem", ref dodgeGem)
                && httpGet.GetInt("TenacityGem", ref tenacityGem))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            EquipData equip = GetEquips.FindEquipData(equipID);

            List<int> gemList = new List<int>();

            if (atkGem != 0) gemList.Add(atkGem);
            if (defGem != 0) gemList.Add(defGem);
            if (hpGem != 0) gemList.Add(hpGem);
            if (critGem != 0) gemList.Add(critGem);
            if (hitGem != 0) gemList.Add(hitGem);
            if (dodgeGem != 0) gemList.Add(dodgeGem);
            if (tenacityGem != 0) gemList.Add(tenacityGem);
            
            foreach (var v in gemList)
            {
                var itemcfg = new ShareCacheStruct<Config_Item>().FindKey(v);
                if (itemcfg.ItemType != ItemType.Gem)
                    return false;

                switch ((GemType)itemcfg.Species)
                {
                    case GemType.Attack:
                        {
                            if (equip.AtkGem != 0)
                            {
                                return false;
                            }
                            equip.AtkGem = v;
                        }
                        break;
                    case GemType.Defense:
                        {
                            if (equip.DefGem != 0)
                            {
                                return false;
                            }
                            equip.DefGem = v;
                        }
                        break;
                    case GemType.Hp:
                        {
                            if (equip.HpGem != 0)
                            {
                                return false;
                            }
                            equip.HpGem = v;
                        }
                        break;
                    case GemType.Crit:
                        {
                            if (equip.CritGem != 0)
                            {
                                return false;
                            }
                            equip.CritGem = v;
                        }
                        break;
                    case GemType.Hit:
                        {
                            if (equip.HitGem != 0)
                            {
                                return false;
                            }
                            equip.HitGem = v;
                        }
                        break;
                    case GemType.Dodge:
                        {
                            if (equip.DodgeGem != 0)
                            {
                                return false;
                            }
                            equip.DodgeGem = v;
                        }
                        break;
                    case GemType.Tenacity:
                        {
                            if (equip.TenacityGem != 0)
                            {
                                return false;
                            }
                            equip.TenacityGem = v;
                        }
                        break;
                }

                // 成就
                UserHelper.AchievementProcess(GetBasis.UserID, AchievementType.InlayGem, "1", v);

                GetPackage.RemoveItem(v, 1);
            }
            UserHelper.RefreshUserFightValue(Current.UserId);


            receipt = true;
            return true;
        }
    }
}