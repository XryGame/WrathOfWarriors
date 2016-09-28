using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Sns;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.LogModel;
using ZyGames.Framework.Net;
using ZyGames.Framework.Game.Lang;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Redis;
using ZyGames.Framework.Common;
using ZyGames.Framework.RPC.Sockets;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Contract;

namespace GameServer.CsScript.Action
{
    //public class MyLogin : ILogin
    //{
    //    public MyLogin(string pid)
    //    {
    //        PassportID = pid;
    //        Password = string.Empty;
    //    }

    //    public string GetRegPassport()
    //    {
    //        return PassportID;
    //    }

    //    public bool CheckLogin()
    //    {
    //        var cache = new ShareCacheStruct<TUser>();
    //        TUser tUser = cache.Find(t => t.UserName == PassportID);
    //        if (tUser != null)
    //        {
    //            UserID = tUser.UserId.ToString();
    //            return true;
    //        }
    //        //not user create it.
    //        tUser = new TUser()
    //        {
    //            UserId = (int)RedisConnectionPool.GetNextNo(typeof(TUser).FullName),
    //            UserName = PassportID,
    //            AccessTime = DateTime.Now
    //        };
    //        if (cache.Add(tUser))
    //        {
    //            UserID = tUser.UserId.ToString();
    //            return true;
    //        }
    //        return false;
    //    }

    //    public string PassportID { get; private set; }
    //    public string UserID { get; private set; }
    //    public int UserType { get; private set; }
    //    public string Password { get; set; }
    //    public string SessionID { get; private set; }
    //}
    /// <summary>
    /// login
    /// </summary>
    public class Action1004 : LoginAction
    {
        //private string _userName;
        private bool isCreated = false;
        public Action1004(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1004, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }
        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        //public override bool GetUrlElement()
        //{
        //    //if (httpGet.GetString("UserName", ref _userName))
        //    //{
        //        return true;
        //    //}
        //    //return false;
        //}
        //protected override ILogin CreateLogin()
        //{
        //    //return new MyLogin(_userName);
        //    return new MyLogin(PassportId);
        //}

        protected override string BuildJsonPack()
        {
            ResultData resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = ErrorCode,
                ErrorInfo = ErrorInfo,
                Data = new JPUserData()
                {
                    UserId = Current.UserId,
                    SessionId = Current.SessionId,
                    isCreated = isCreated
                }
            };
            return MathUtils.ToJson(resultData);
        }

        protected override bool DoSuccess(int userId, out IUser user)
        {
            user = null;
            //var cache = new MemoryCacheStruct<TUser>();
            //TUser chatUser = cache.Find(t => t.UserName == _userName);
            //if (chatUser != null)
            //{
            //    user = chatUser;
            //    return true;
            //}

            var cacheSet = new PersonalCacheStruct<GameUser>();
            GameUser gameUser = cacheSet.FindKey(userId.ToString());
            if (gameUser == null ||
                string.IsNullOrEmpty(gameUser.SessionID) ||
                (Current.User != null && !Current.User.IsOnlining))
            {
                gameUser = cacheSet.FindKey(userId.ToString());
            }

            if (gameUser != null)
            {
                //原因：还在加载中时，返回
                if (gameUser.UserStatus != UserStatus.Onfine)
                {
                    ErrorCode = Language.Instance.ErrorCode;
                    ErrorInfo = Language.Instance.ServerLoading;
                    return false;
                }

            }

            //TUser tUser = new ShareCacheStruct<TUser>().FindKey(userId);
            //if (tUser == null)
            //{
            //    return false;
            //}
            //user = tUser;
            var nowTime = DateTime.Now;
            if (gameUser == null)
            {
                user = new SessionUser() { PassportId = PassportId, UserId = userId };
                ////ErrorCode = 1005;
                return true;
            }
            isCreated = true;
            //user = new SessionUser(gameUser);
            if (gameUser.UserStatus == UserStatus.Lock)
            {
                ErrorCode = Language.Instance.TimeoutCode;
                ErrorInfo = Language.Instance.AcountIsLocked;
                return false;
            }
            gameUser.SessionID = Sid;
            gameUser.LoginDate = nowTime;
            gameUser.GameId = this.GameType;
            gameUser.ServerId = this.ServerID;

            UserHelper.UserOnline(userId);


            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                //登录日志
                UserLoginLog userLoginLog = new UserLoginLog();
                userLoginLog.UserId = gameUser.UserID.ToString();
                userLoginLog.SessionID = Sid;
                userLoginLog.MobileType = MobileType;
                userLoginLog.ScreenX = this.ScreenX;
                userLoginLog.ScreenY = this.ScreenY;
                userLoginLog.RetailId = this.RetailID;
                userLoginLog.AddTime = nowTime;
                userLoginLog.State = LoginStatus.Logined;
                userLoginLog.DeviceID = this.DeviceID;
                userLoginLog.Ip = this.GetRealIP();
                userLoginLog.Pid = gameUser.Pid;
                userLoginLog.UserLv = gameUser.UserLv;
                var sender = DataSyncManager.GetDataSender();
                sender.Send(new[] { userLoginLog });
            });
            return true;
        }


    }
}
