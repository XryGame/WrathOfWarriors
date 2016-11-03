using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10400_扫荡CD
    /// </summary>
    public class Action10400 : BaseAction
    {
        private int plotId;
        public Action10400(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10400, actionGetter)
        {
            IsNotRespond = true;
        }


        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("PlotId", ref plotId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            ContextUser.PlotId = plotId;
            return true;
        }

    }
}