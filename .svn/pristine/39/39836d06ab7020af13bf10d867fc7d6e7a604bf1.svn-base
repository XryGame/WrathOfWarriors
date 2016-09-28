using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1202_申请加入班级
    /// </summary>
    public class Action1202 : BaseAction
    {
        private int ClassId;
        private JPClassData receipt;

        public Action1202(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1202, actionGetter)
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
                ErrorCode = ActionIDDefine.Cst_Action1202;
            }
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ClassID", ref ClassId))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            int classlv = ContextUser.UserLv / 2 + 1;
            List<ClassDataCache> classeslist = new ShareCacheStruct<ClassDataCache>().FindAll(t => (t.Lv == classlv));
            ClassDataCache newcalss = classeslist.Find(t => (t.ClassID == ClassId));
            if (newcalss == null)
            {
                ErrorInfo = ErrorInfo = string.Format(Language.Instance.RequestIDError, ClassId);
                return true;
            }

            if (newcalss.MemberList.Find(t => (t == UserId)) != 0
                || newcalss.MemberList.Count >= ConfigEnvSet.GetInt("Class.MaxNum"))
            {
                ErrorInfo = ErrorInfo = Language.Instance.ClassMemberFullError;
                return true;
            }
            // 将用户从原有班级中剔除
            if (ContextUser.ClassData.ClassID != 0)
            {
                ClassDataCache oldclass = classeslist.Find(t => (t.ClassID == ContextUser.ClassData.ClassID));
                if (oldclass != null)
                {
                    if (oldclass.MemberList.Find(t => (t == UserId)) != 0)
                    {
                        oldclass.MemberList.Remove(UserId);
                        if (oldclass.Monitor == UserId)
                        {
                            oldclass.Monitor = oldclass.MemberList.Count > 0 ? oldclass.MemberList[0] : 0;
                        }
                    }
                }
            }
            // 将用户加入到新班级中
            ContextUser.ClassData.ClassID = ClassId;
            newcalss.MemberList.Add(UserId);
            if (newcalss.MemberList.Count == 1 || newcalss.Monitor == 0)
            {// 设置班长
                newcalss.Monitor = UserId;
            }




            receipt = new JPClassData();
            receipt.ClassID = ClassId;
            receipt.Num = newcalss.MemberList.Count;
            GameUser MUser = UserHelper.FindUser(newcalss.Monitor);
            if (MUser != null)
            { 
                receipt.Monitor = new JPClassMemberData()
                {
                    Uid = newcalss.Monitor,
                    Nickname = MUser.NickName,
                    UserLv = MUser.UserLv
                };
            }
            
            foreach (int uid in newcalss.MemberList)
            {
                JPClassMemberData tmp = new JPClassMemberData();
                tmp.Uid = uid;
                receipt.MemberList.Add(tmp);
            }


            // 扩充班级
            List<ClassDataCache> findclasseslist = new ShareCacheStruct<ClassDataCache>().FindAll(
                t => (t.Lv == classlv) && t.MemberList.Count < ConfigEnvSet.GetInt("Class.OpenNum")
                );
            if (findclasseslist.Count == 0)
            {
                var classcache = new ShareCacheStruct<ClassDataCache>();
                
                ClassDataCache tmpcache = new ClassDataCache()
                {
                    ClassID = newcalss.ClassID + 1,
                    Lv = newcalss.Lv
                };
                classcache.Add(tmpcache);
                classcache.Update();
            }
            return true;
        }
    }
}