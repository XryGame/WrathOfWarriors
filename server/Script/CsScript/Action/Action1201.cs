using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.DataModel;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1201_查询本年级所有班级信息
    /// </summary>
    public class Action1201 : BaseAction
    {
        private JPClassesData receipt;

        public Action1201(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1201, actionGetter)
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

            int classlv = ContextUser.UserLv / 2 + 1;
            List<ClassDataCache> classeslist = new ShareCacheStruct<ClassDataCache>().FindAll(t => (t.Lv == classlv));
            if (classeslist.Count < 3)
            {
                ErrorInfo = string.Format(Language.Instance.DBTableError, "ClassDataCache");
                return true;
            }

            receipt = new JPClassesData();
            //ClassData classdata = new ClassData();
            foreach (ClassDataCache data in classeslist)
            {
                JPClassData classdata = new JPClassData();
                classdata.ClassID = data.ClassID;
                classdata.ClassName = data.Name;
                classdata.Num = data.MemberList.Count;
                GameUser MUser = UserHelper.FindUser(data.Monitor);
                if (MUser != null)
                {
                    classdata.Monitor = new JPClassMemberData()
                    {
                        Uid = data.Monitor,
                        Nickname = MUser.NickName,
                        UserLv = MUser.UserLv
                    };
                }
                
                receipt.ClassList.Add(classdata);
            }
            return true;
        }
    }
}