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

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 微信公众号 login
    /// </summary>
    public class Action1020 : LoginAction
    {
        private bool isCreated = false;
        public Action1020(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1020, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
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


        protected override bool DoSuccess(int userId, out IUser user)
        {
            user = null;

            GameUser gameUser = UserHelper.FindUser(userId);

            var nowTime = DateTime.Now;
            if (gameUser == null)
            {
                user = new SessionUser() { PassportId = PassportId, UserId = userId };
                return true;
            }
            isCreated = true;
            user = new SessionUser(gameUser);
            //Current.Bind(user);
            if (gameUser.UserStatus == UserStatus.Lock)
            {
                ErrorCode = Language.Instance.TimeoutCode;
                ErrorInfo = Language.Instance.AcountIsLocked;
                return false;
            }
            gameUser.SessionID = Sid;
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
