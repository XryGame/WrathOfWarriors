///****************************************************************************
//Copyright (c) 2013-2015 scutgame.com

//http://www.scutgame.com

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.
//****************************************************************************/
//using ZyGames.Framework.Common;
//using ZyGames.Framework.Common.Log;
//using ZyGames.Framework.Cache.Generic;
//using GameServer.Script.Model.DataModel;

//namespace GameServer.Script.Model
//{
//    public static class UserCacheGlobal
//    {
//        private const int LoadMaxCount = 10000;

//        //private static int periodTime = 600;//秒

//        public static void ReLoad(string userId)
//        {
//            DoLoad(userId, true, false);
//        }

//        public static UserBasisCache LoadOffline(string userId)
//        {
//            var cacheSet = new PersonalCacheStruct<UserBasisCache>();
//            DoLoad(userId, false, true);
//            return cacheSet.FindKey(userId);
//        }

//        public static void Load(string userId)
//        {
//            DoLoad(userId, false, false);
//        }

//        public static UserBasisCache CheckLoadUser(string userId)
//        {
//            var cacheSet = new PersonalCacheStruct<UserBasisCache>();
//            UserBasisCache basis = cacheSet.FindKey(userId);
//            if (basis == null)
//            {
//                //bool isAuto = true;
//                //bool ignoreError = true;
//                //int periodTime = GameEnvironment.CacheUserPeriod;
//                //todo 注释掉，由底层自动加载
//                //GameLoadManager.Add(new GameUserDataLoader<UserBasisCache>(isAuto, "UserID", userId.ToInt(), 1, periodTime), ignoreError);
//                //GameLoadManager.Add(new GameUserDataLoader<UserGeneral>(isAuto, "UserID", userId.ToInt(), LoadMaxCount, periodTime), ignoreError);
//            }
//            return basis;
//        }

//        private static void DoLoad(string personalId, bool isReload, bool offline)
//        {
//            int userId = personalId.ToInt();
//            if (string.IsNullOrEmpty(personalId))
//            {
//                TraceLog.WriteInfo("Load userid:\"{0}\" is null", userId);
//                return;
//            }
//            lock (personalId)
//            {
//                //todo 注释掉，由底层自动加载
//                //bool isAuto = !isReload;
//                //bool ignore = true;
//                //string fieldName = "UserID";
//                //int periodTime = GameEnvironment.CacheUserPeriod;
//                //GameLoadManager.Add(new GameUserDataLoader<UserBasisCache>(isAuto, fieldName, userId.ToInt(), 1, periodTime), ignore);

//                var cacheSet = new PersonalCacheStruct<UserBasisCache>();
//                UserBasisCache basis = cacheSet.FindKey(personalId);
//                //string pid = basis != null ? basis.Pid : string.Empty;

//                if (basis == null)
//                {
//                    //新注册用户会为null
//                    return;
//                }
//                if (offline && basis != null)
//                {
//                    //basis.IsOnline = false;
//                    //basis.OnlineDate = DateTime.Now;
//                }
//                //new PersonalCacheStruct<UserShengJiTa>().AutoLoad(personalId);
//                //new PersonalCacheStruct<UserShengJiTa>().AutoLoad("1448620");
//                //new PersonalCacheStruct<UserShengJiTa>().AutoLoad("1449220");
//                //new PersonalCacheStruct<UserShengJiTa>().AutoLoad("1449182");

//                //basis.IsRefreshing = true;
//                //basis.IsRefreshing = false;
//            }
//        }

//        public static UserBasisCache GetGameUser(string userID)
//        {
//            var cacheSet = new PersonalCacheStruct<UserBasisCache>();
//            UserBasisCache userInfo = cacheSet.FindKey(userID);
//            if (userInfo == null)
//            {
//                Load(userID);//重新刷缓存
//                userInfo = cacheSet.FindKey(userID);
//            }
//            return userInfo;
//        }

//    }
//}