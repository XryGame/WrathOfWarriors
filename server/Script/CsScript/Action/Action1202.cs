using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
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
            if (newcalss == null || ContextUser.ClassData.ClassID != 0)
            {
                ErrorInfo = string.Format(Language.Instance.RequestIDError, ClassId);
                return true;
            }

            if (newcalss.MemberList.Find(t => (t == ContextUser.UserID)) != 0
                || newcalss.MemberList.Count >= ConfigEnvSet.GetInt("Class.MaxNum"))
            {
                ErrorInfo = Language.Instance.ClassMemberFullError;
                return true;
            }
            // 将用户从原有班级中剔除
            var classlist = new ShareCacheStruct<ClassDataCache>().FindAll();
            foreach (var cl in classeslist)
            {
                if (cl.MemberList.Find(t => (t == ContextUser.UserID)) != 0)
                {
                    cl.MemberList.Remove(ContextUser.UserID);
                    if (cl.Monitor == ContextUser.UserID)
                    {
                        cl.Monitor = cl.MemberList.Count > 0 ? cl.MemberList[0] : 0;
                    }
                }
            }

            // 将用户加入到新班级中
            ContextUser.ClassData.ClassID = ClassId;
            newcalss.MemberList.Add(ContextUser.UserID);
            if (newcalss.MemberList.Count == 1 || newcalss.Monitor == 0)
            {// 设置班长
                newcalss.Monitor = ContextUser.UserID;
            }




            receipt = new JPClassData();
            receipt.ClassID = ClassId;
            receipt.Num = newcalss.MemberList.Count;
            GameUser MUser = UserHelper.FindUser(newcalss.Monitor);
            if (MUser != null)
            { 
                receipt.Monitor = new JPClassMemberData()
                {
                    UserId = newcalss.Monitor,
                    NickName = MUser.NickName,
                    LooksId = MUser.LooksId,
                    UserLv = MUser.UserLv,
                    SkillCarryList = MUser.SkillCarryList
                };
            }
            
            foreach (int uid in newcalss.MemberList)
            {
                JPClassMemberData tmp = new JPClassMemberData();
                tmp.UserId = uid;
                receipt.MemberList.Add(tmp);
            }
            // 占领加成处理
            var occupylist = new ShareCacheStruct<OccupyDataCache>().FindAll();
            foreach (var v in occupylist)
            {
                if (v.UserId == ContextUser.UserID)
                {
                    //foreach (int id in newcalss.MemberList)
                    //{
                    //    GameUser mem = UserHelper.FindUser(id);
                    //    if (mem == null)
                    //        continue;
                    //    if (mem.OccupyAddList.Find(t => (t == v.SceneId)) != v.SceneId)
                    //        mem.OccupyAddList.Add(v.SceneId);

                    //}
                    PushMessageHelper.ClassOccupyAddChangeNotification(ContextUser.ClassData.ClassID);
                }
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
                if (tmpcache.Lv == 1) tmpcache.Name = string.Format("学前（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 2) tmpcache.Name = string.Format("一年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 3) tmpcache.Name = string.Format("二年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 4) tmpcache.Name = string.Format("三年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 5) tmpcache.Name = string.Format("四年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 6) tmpcache.Name = string.Format("五年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 7) tmpcache.Name = string.Format("六年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 8) tmpcache.Name = string.Format("七年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 9) tmpcache.Name = string.Format("八年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 10) tmpcache.Name = string.Format("九年（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 11) tmpcache.Name = string.Format("高一（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 12) tmpcache.Name = string.Format("高二（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 13) tmpcache.Name = string.Format("高三（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 14) tmpcache.Name = string.Format("大一（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 15) tmpcache.Name = string.Format("大二（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 16) tmpcache.Name = string.Format("大三（{0}）班", classeslist.Count + 1);
                else if (tmpcache.Lv == 17) tmpcache.Name = string.Format("大四（{0}）班", classeslist.Count + 1);

                classcache.Add(tmpcache);
                classcache.Update();
            }
            return true;
        }
    }
}