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
using ZyGames.Framework.Common.Log;
using GameServer.Script.CsScript.Action;
using ZyGames.Framework.Common.Security;
using System.Text;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// login
    /// </summary>
    public class Action1004 : BaseStruct
    {
        private bool isCreated = false;
        //private MobileType MobileType;
        private string PassportId;
        private string Password;
        private int ServerID;
        public Action1004(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1004, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        public override bool GetUrlElement()
        {
            if (actionGetter.GetString("Pid", ref PassportId) &&
                actionGetter.GetString("Pwd", ref Password) &&
                actionGetter.GetInt("ServerID", ref ServerID))
            {
                return true;
            }
            return false;
        }

        protected override string BuildJsonPack()
        {
            ResultData resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = ErrorCode,
                ErrorInfo = ErrorInfo,
                Data = new JPLoginUserData()
                {
                    UserId = Current.UserId,
                    SessionId = Current.SessionId,
                    isCreated = isCreated
                }
            };
            return MathUtils.ToJson(resultData);
        }




        public override bool TakeAction()
        {
            var ucpcache = new ShareCacheStruct<UserCenterPassport>();
            var ucp = ucpcache.FindKey(PassportId);
            if (ucp == null)
            {
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.PasswordError;
                return false;
            }

            try
            {
                if (ucp.Password.CompareTo(Password) != 0)
                {
                    ErrorCode = Language.Instance.ErrorCode;
                    ErrorInfo = Language.Instance.PasswordError;
                    return false;
                }
            }
            catch (Exception ex)
            {
                new BaseLog().SaveLog(ex);
                return false;
            }

            
            var cache = new ShareCacheStruct<UserCenterUser>();
            var ucu = cache.Find(t => (t.PassportID == PassportId && t.ServerId == ServerID));
            if (ucu == null)
            {
                //not user create it.
                ucu = new UserCenterUser()
                {
                    UserId = (int)RedisConnectionPool.GetNextNo(typeof(UserCenterUser).FullName),
                    PassportID = PassportId,
                    ServerId = ServerID,
                    AccessTime = DateTime.Now,
                    LoginNum = 0
                };
                cache.Add(ucu);
                cache.Update();
            }
            ucu.AccessTime = DateTime.Now;
            ucu.LoginNum++;

            GameUser gameUser = UserHelper.FindUser(ucu.UserId);

            SessionUser user = null;
            if (gameUser == null)
            {
                user = new SessionUser() { PassportId = PassportId, UserId = ucu.UserId };
                Current.Bind(user);
                return true;
            }
            isCreated = true;
            user = new SessionUser(gameUser);
            Current.Bind(user);
            if (gameUser.UserStatus == UserStatus.Lock)
            {
                ErrorCode = Language.Instance.TimeoutCode;
                ErrorInfo = Language.Instance.AcountIsLocked;
                return false;
            }
            gameUser.SessionID = Sid;
            gameUser.ServerId = this.ServerID;
            
            UserHelper.UserOnline(ucu.UserId);
            

            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                //登录日志
                UserLoginLog userLoginLog = new UserLoginLog();
                userLoginLog.UserId = gameUser.UserID.ToString();
                userLoginLog.SessionID = Sid;
                userLoginLog.AddTime = DateTime.Now;
                userLoginLog.State = LoginStatus.Logined;
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
