using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 20400_剧情
    /// </summary>
    public class Action20400 : BaseAction
    {
        private int plotId;
        public Action20400(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action20400, actionGetter)
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