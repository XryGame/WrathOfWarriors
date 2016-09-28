using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1310_携带技能
    /// </summary>
    public class Action1310 : BaseAction
    {
        private CacheList<int> receipt;
        private int index; // 为0表示卸载
        private int skillid;

        public Action1310(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1310, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1310;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("Index", ref index)
                && httpGet.GetInt("SkillID", ref skillid))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            int CarrySkillNum = ConfigEnvSet.GetInt("User.CarrySkillNum");
            if (index < 0 || index > CarrySkillNum)
            {
                ErrorInfo = Language.Instance.UrlParamOutRange;
                return true;
            }
            SkillData skill = ContextUser.SkillDataList.Find(t => (t.ID == skillid));
            if (skill == null)
            {
                ErrorInfo = Language.Instance.RequestIDError;
                return true;
            }
            
            if (index > 0)
            {// 携带
                if (ContextUser.SkillCarryList.Find(t => (t == skillid)) != 0)
                {
                    ErrorInfo = Language.Instance.RequestIDError;
                    return true;
                }
                if (ContextUser.SkillCarryList.Count < index)
                {
                    ContextUser.SkillCarryList.Add(skillid);
                }
                else
                {
                    ContextUser.SkillCarryList[index - 1] = skillid;
                }
            }
            else
            {// 卸载
                if (ContextUser.SkillCarryList.Find(t => (t == skillid)) == 0)
                {
                    ErrorInfo = Language.Instance.RequestIDError;
                    return true;
                }
                ContextUser.SkillCarryList.Remove(skillid);
            }
            

            receipt = new CacheList<int>();
            receipt = ContextUser.SkillCarryList;
            return true;
        }
    }
}