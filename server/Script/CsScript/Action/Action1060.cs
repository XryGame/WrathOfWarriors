using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 1060_每日任务刷新
    /// </summary>
    public class Action1060 : BaseAction
    {

        private UserDailyQuestData receipt;
        private TaskType type;
        public Action1060(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1060, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {

            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("ID", ref type))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = GetTask.FindTask(type);
            return true;
        }
    }
}