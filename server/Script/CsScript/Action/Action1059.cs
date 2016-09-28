using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1059_通知班长变更通知
    /// </summary>
    public class Action1059 : BaseAction
    {
        private JPQueryClassMonitorData receipt;
        private int Uid;
        public Action1059(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1059, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1059;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Uid", ref Uid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            receipt = new JPQueryClassMonitorData();
            if (ContextUser.ClassData.ClassID != 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
                if (classdata != null)
                {
                    GameUser monitor = UserHelper.FindUser(classdata.Monitor);
                    if (monitor != null)
                    {
                        receipt.UserId = monitor.UserID;
                        receipt.NickName = monitor.NickName;
                        receipt.LooksId = monitor.LooksId;
                        receipt.FightValue = monitor.FightingValue;
                        receipt.SkillCarryList = monitor.SkillCarryList;
                    }
                }
            }

            return true;
        }
    }
}