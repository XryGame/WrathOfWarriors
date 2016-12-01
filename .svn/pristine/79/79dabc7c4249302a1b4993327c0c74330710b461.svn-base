using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1203_查询同伴同学
    /// </summary>
    public class Action1203 : BaseAction
    {
        private List<JPClassMemberData> receipt;

        public Action1203(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1203, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1201;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            if (ContextUser.ClassData.ClassID == 0)
                return false;


            ClassDataCache classdata = new ShareCacheStruct<ClassDataCache>().FindKey(ContextUser.ClassData.ClassID);
            if (classdata == null)
            {
                return false;
            }
            receipt = new List<JPClassMemberData>();
            foreach (var member in classdata.MemberList)
            {
                GameUser MUser = UserHelper.FindUser(member);
                if (MUser != null)
                {
                    JPClassMemberData data = new JPClassMemberData()
                    {
                        UserId = member,
                        NickName = MUser.NickName,
                        UserLv = MUser.UserLv,
                        LooksId = MUser.LooksId,
                        FightValue = MUser.FightingValue,
                        SkillCarryList = MUser.SkillCarryList
                    };
                    receipt.Add(data);
                }
            }
            return true;
        }
    }
}