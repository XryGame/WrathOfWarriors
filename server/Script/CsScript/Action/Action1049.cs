using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    public class GoldNotificationData
    {
        public UpdateGoldType UpdateGoldType { get; set; }

        public string GoldString { get; set; }
    }
    /// <summary>
    /// 1049_金币数量改变通知接口
    /// </summary>
    public class Action1049 : BaseAction
    {
        private GoldNotificationData receipt;
        private UpdateGoldType _updateGoldType;
        public Action1049(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1049, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("UpdateType", ref _updateGoldType))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new GoldNotificationData();
            receipt.UpdateGoldType = _updateGoldType;
            receipt.GoldString = GetBasis.Gold;

            return true;
        }
    }
}