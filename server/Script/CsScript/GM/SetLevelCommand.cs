
//using GameServer.CsScript.Com;
//using GameServer.Script.Model.Config;
//using GameServer.Script.Model.ConfigModel;
//using GameServer.Script.Model.DataModel;
//using System;
//using ZyGames.Framework.Cache.Generic;
//using ZyGames.Framework.Common;

//namespace GameServer.CsScript.GM
//{
//    public class SetLevelCommand : TryXGMCommand
//    {
//        public static readonly SetLevelCommand instance = new SetLevelCommand();

//        public override void ProcessCmd()
//        {
//            int lv = Args.Length > 2 ? Args[2].Trim().ToInt() : 0;
//            if (lv > 0)
//            {
//                SetLevel(UserId, lv);
//            }
//        }

//        /// <summary>
//        /// 设置等级
//        /// </summary>
//        /// <param name="userId"></param>
//        private void SetLevel(int userId, int lv)
//        {
//            UserBasisCache user = UserHelper.FindUserBasis(userId);
//            if (user == null)
//                return;

//            if (user.UserLv >= lv)
//                return;

//            var roleGradeCache = new ShareCacheStruct<Config_RoleInitial>();
//            if (lv > roleGradeCache.FindAll().Count)
//                return;

//            int count = lv - user.UserLv;
//            for (int i = 0; i < count; ++i)
//            {
//                user.UserLv++;
//                UserHelper.UserLevelUp(userId);
//            }
//            var grade = roleGradeCache.FindKey(user.UserLv - 1);
//            if (grade != null)
//            {
//                //user.BaseExp = grade.BaseExp;
//                //user.FightExp = grade.FightExp;
//            }
                

//        }
//    }
//}