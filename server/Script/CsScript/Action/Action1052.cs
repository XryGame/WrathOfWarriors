//using GameServer.CsScript.JsonProtocol;
//using GameServer.Script.CsScript.Action;
//using ZyGames.Framework.Game.Service;

//namespace GameServer.CsScript.Action
//{

//    /// <summary>
//    /// 1052_经验等级改变通知接口
//    /// </summary>
//    public class Action1052 : BaseAction
//    {
//        private object receipt;
//        public Action1052(ActionGetter actionGetter)
//            : base(ActionIDDefine.Cst_Action1052, actionGetter)
//        {

//        }

//        protected override string BuildJsonPack()
//        {
//            body = receipt;
//            return base.BuildJsonPack();
//        }

//        public override bool GetUrlElement()
//        {
//            return true;
//        }

//        public override bool TakeAction()
//        {
//            receipt = ContextUser.DiamondNum;
//            return true;
//        }
//    }
//}