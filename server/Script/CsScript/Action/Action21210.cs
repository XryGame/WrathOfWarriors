//using GameServer.CsScript.Base;
//using GameServer.CsScript.JsonProtocol;
//using GameServer.Script.CsScript.Action;
//using GameServer.Script.Model.ConfigModel;
//using GameServer.Script.Model.DataModel;
//using GameServer.Script.Model.Enum;
//using System;
//using System.Collections.Generic;
//using ZyGames.Framework.Cache.Generic;
//using ZyGames.Framework.Game.Service;

//namespace GameServer.CsScript.Action
//{

//    /// <summary>
//    /// 21210_争霸赛信息
//    /// </summary>
//    public class Action21210 : BaseAction
//    {
//        private JPCompetition64Data receipt;

//        public Action21210(ActionGetter actionGetter)
//            : base(ActionIDDefine.Cst_Action21210, actionGetter)
//        {

//        }
//        protected override string BuildJsonPack()
//        {
//            if (receipt != null)
//            {
//                body = receipt;
//            }
//            else
//            {
//                ErrorCode = ActionIDDefine.Cst_Action21210;
//            }
//            return base.BuildJsonPack();
//        }

//        public override bool GetUrlElement()
//        {
//            return true;
//        }

//        public override bool TakeAction()
//        {
//            receipt = SystemGlobal.competition64.getReceipt();
//            return true;
//        }
//    }
//}