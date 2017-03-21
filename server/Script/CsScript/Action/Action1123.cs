using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1123_一键宝石拆卸
    /// </summary>
    public class Action1123 : BaseAction
    {
        private bool receipt;
        private EquipID equipID;

        public Action1123(ActionGetter actionGetter)
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
            if (httpGet.GetEnum("EquipID", ref equipID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            EquipData equip = GetEquips.FindEquipData(equipID);
            GetPackage.AddItem(equip.AtkGem, 1);
            GetPackage.AddItem(equip.DefGem, 1);
            GetPackage.AddItem(equip.HpGem, 1);
            GetPackage.AddItem(equip.CritGem, 1);
            GetPackage.AddItem(equip.HitGem, 1);
            GetPackage.AddItem(equip.DodgeGem, 1);
            GetPackage.AddItem(equip.TenacityGem, 1);
            equip.AtkGem = 0;
            equip.DefGem = 0;
            equip.HpGem = 0;
            equip.CritGem = 0;
            equip.HitGem = 0;
            equip.DodgeGem = 0;
            equip.TenacityGem = 0;

            UserHelper.RefreshUserFightValue(Current.UserId);
            receipt = true;
            return true;
        }
    }
}