using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4002_请求挑战班长结果
    /// </summary>
    public class Action4002 : BaseAction
    {
        private object receipt;
        private EventStatus result;

        public Action4002(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4002, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action4002;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetEnum("Result", ref result))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            var classdata = new ShareCacheStruct<ClassDataCache>().Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
            if (classdata != null && classdata.IsChallenging && classdata.ChallengeUserId == ContextUser.UserID)
            {
                classdata.IsChallenging = false;
                classdata.ChallengeUserId = 0;
            }
            if (result == EventStatus.Good)
            {// 挑战成功处理
                classdata.Monitor = ContextUser.UserID;

                PushMessageHelper.ClassMonitorChangeNotification(ContextUser.ClassData.ClassID);
            }
            ContextUser.UserStatus = UserStatus.MainUi;
            ContextUser.ChallengeMonitorTimes = 1;
            receipt = result;
            return true;
        }
    }
}