using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1121_宝石拆卸
    /// </summary>
    public class Action1121 : BaseAction
    {
        private bool receipt;
        private EquipID equipID;
        private GemType gemType;

        public Action1121(ActionGetter actionGetter)
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
                && httpGet.GetEnum("GemType", ref gemType))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            EquipData equip = GetEquips.FindEquipData(equipID);


            switch (gemType)
            {
                case GemType.Attack:
                    {
                        if (equip.AtkGem != 0)
                        {
                            GetPackage.AddItem(equip.AtkGem, 1);
                            equip.AtkGem = 0;
                        }
                    }
                    break;
                case GemType.Defense:
                    {
                        if (equip.DefGem != 0)
                        {
                            GetPackage.AddItem(equip.DefGem, 1);
                            equip.DefGem = 0;
                        }
                    }
                    break;
                case GemType.Hp:
                    {
                        if (equip.HpGem != 0)
                        {
                            GetPackage.AddItem(equip.HpGem, 1);
                            equip.HpGem = 0;
                        }
                    }
                    break;
                case GemType.Crit:
                    {
                        if (equip.CritGem != 0)
                        {
                            GetPackage.AddItem(equip.CritGem, 1);
                            equip.CritGem = 0;
                        }
                    }
                    break;
                case GemType.Hit:
                    {
                        if (equip.HitGem != 0)
                        {
                            GetPackage.AddItem(equip.HitGem, 1);
                            equip.HitGem = 0;
                        }
                    }
                    break;
                case GemType.Dodge:
                    {
                        if (equip.DodgeGem != 0)
                        {
                            GetPackage.AddItem(equip.DodgeGem, 1);
                            equip.DodgeGem = 0;
                        }
                    }
                    break;
                case GemType.Tenacity:
                    {
                        if (equip.TenacityGem != 0)
                        {
                            GetPackage.AddItem(equip.TenacityGem, 1);
                            equip.TenacityGem = 0;
                        }
                    }
                    break;
            }
            UserHelper.RefreshUserFightValue(Current.UserId);
            receipt = true;
            return true;
        }
    }
}