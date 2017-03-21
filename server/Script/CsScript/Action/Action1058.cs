using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using System.Collections.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1058_获得新物品
    /// </summary>
    public class Action1058 : BaseAction
    {
        private List<ItemData> receipt;
        public Action1058(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1058, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1058;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = GetPackage.NewItemCache.ToList();
            GetPackage.NewItemCache.Clear();

            return true;
        }
    }
}