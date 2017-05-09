//using GameServer.CsScript.JsonProtocol;
//using GameServer.Script.CsScript.Action;
//using GameServer.Script.Model.Enum;
//using ZyGames.Framework.Game.Service;

//namespace GameServer.CsScript.Action
//{
//    public class DiamondNotificationData
//    {
//        public UpdateCoinOperate UpdateDiamondType { get; set; }

//        public int Diamond { get; set; }
//    }
//    /// <summary>
//    /// 1051_钻石数量改变通知接口
//    /// </summary>
//    public class Action1051 : BaseAction
//    {
//        private DiamondNotificationData receipt;
//        private UpdateCoinOperate _updateDiamondType;
//        public Action1051(ActionGetter actionGetter)
//            : base(ActionIDDefine.Cst_Action1051, actionGetter)
//        {

//        }

//        protected override string BuildJsonPack()
//        {
//            body = receipt;
//            return base.BuildJsonPack();
//        }

//        public override bool GetUrlElement()
//        {
//            if (httpGet.GetEnum("UpdateType", ref _updateDiamondType))
//            {
//                return true;
//            }
//            return false;
//        }

//        public override bool TakeAction()
//        {
//            receipt = new DiamondNotificationData();
//            receipt.UpdateDiamondType = _updateDiamondType;
//            receipt.Diamond = GetBasis.DiamondNum;
//            return true;
//        }
//    }
//}