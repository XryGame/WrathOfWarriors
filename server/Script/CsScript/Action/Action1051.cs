using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 体力改变通知接口
    /// </summary>
    public class Action1051 : BaseAction
    {
        private JPVitData receipt;
        public Action1051(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1051, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPVitData();
            receipt.RemainTimeSec = GetBasis.RestoreVitRemainTimeSec();
            receipt.Vit = GetBasis.Vit;
            return true;
        }
    }
}