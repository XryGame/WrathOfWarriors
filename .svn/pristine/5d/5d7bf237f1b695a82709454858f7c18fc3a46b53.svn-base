using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 4000_查询班长信息
    /// </summary>
    public class Action4000 : BaseAction
    {
        private JPQueryClassMonitorData receipt;


        public Action4000(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action4000, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action4001;
            }
                
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            receipt = new JPQueryClassMonitorData();
            receipt.result = EventStatus.Good;
            if (ContextUser.ClassData.ClassID == 0)
            {
                receipt.result = EventStatus.Bad;
            }
            if (receipt.result == EventStatus.Good)
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
                    else
                    {
                        receipt.result = EventStatus.Bad;
                    }
                }
                else
                {
                    receipt.result = EventStatus.Bad;
                }
            }
            return true;
        }
    }
}