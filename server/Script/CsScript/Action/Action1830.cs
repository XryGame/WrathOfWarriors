using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 剧情
    /// </summary>
    public class Action1830 : BaseAction
    {
        private int plotId;
        public Action1830(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1830, actionGetter)
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
            GetTask.PlotId = plotId;
            return true;
        }

    }
}