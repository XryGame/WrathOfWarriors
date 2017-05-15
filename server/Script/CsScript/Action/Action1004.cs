using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.LogModel;
using ZyGames.Framework.Net;
using ZyGames.Framework.Game.Lang;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Common;
using ZyGames.Framework.RPC.Sockets;
using GameServer.CsScript.JsonProtocol;
using ZyGames.Framework.Game.Contract;
using GameServer.CsScript.Base;
using System.Configuration;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// login
    /// </summary>
    public class Action1004 : BaseStruct
    {

        private bool isCreated = false;

        private string logindata = string.Empty;
       // private string md5key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDXdPs18RFj4XBEzbPNZ+58CsPC7AeVhZ3zWxVKPfozuwVCR1kDhYp/5e2tMVoleayDpAq/2FJUNTbTu5eYkow11Cho2RRGuMRRhl0RJ0lqItuwbe4a8/D2cgqsw+BxrZLcWO0xpnE7NGTkMc7sRz60Muq5izhLYrDUn/KUd7qi/QIDAQAB";

        private string OpenID = string.Empty;
        private string RetailID = string.Empty;
        public int ServerID = 0;

        public Action1004(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1004, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        public override bool GetUrlElement()
        {
            if (actionGetter.GetString("OpenID", ref OpenID)
                && !string.IsNullOrEmpty(OpenID)
                && actionGetter.GetString("RetailID", ref RetailID)
                && !string.IsNullOrEmpty(RetailID)
                && actionGetter.GetInt("ServerID", ref ServerID))
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
                    isCreated = isCreated,
                    GlobalServerUrl = ConfigurationManager.AppSettings["GlobalServiceAddr"]
                }
            };
            return MathUtils.ToJson(resultData);
        }

        
        public override bool TakeAction()
        {
            SessionUser user = null;
            try
            {

                var cache = new ShareCacheStruct<UserCenterUser>();
                var uculist = Util.FindUserCenterUser(OpenID, RetailID, ServerID);

                if (uculist.Count <= 0)
                {
                    UserCenterUser ucu = Util.CreateUserCenterUser(OpenID, RetailID, ServerID);
                    user = new SessionUser() { PassportId = OpenID, UserId = ucu.UserID };
                    Current.Bind(user);
                    return true;
                }
                
                UserBasisCache basis = UserHelper.FindUserBasis(uculist[0].UserID);
                if (basis == null)
                {
                    user = new SessionUser() { PassportId = OpenID, UserId = uculist[0].UserID };
                    Current.Bind(user);
                    return true;
                }
                uculist[0].LoginNum++;
                isCreated = true;

                user = new SessionUser(basis);
                Current.Bind(user);
                if (basis.UserStatus == UserStatus.Lock)
                {
                    ErrorCode = Language.Instance.TimeoutCode;
                    ErrorInfo = Language.Instance.AcountIsLocked;
                    return false;
                }
                basis.SessionID = Sid;
                //basis.ServerID = this.ServerID;

                UserHelper.UserOnline(basis.UserID);


                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    //登录日志
                    UserLoginLog userLoginLog = new UserLoginLog();
                    userLoginLog.UserId = basis.UserID.ToString();
                    userLoginLog.SessionID = Sid;
                    userLoginLog.AddTime = DateTime.Now;
                    userLoginLog.State = LoginStatus.Logined;
                    userLoginLog.Ip = this.GetRealIP();
                    userLoginLog.Pid = basis.Pid;
                    userLoginLog.UserLv = basis.UserLv;
                    var sender = DataSyncManager.GetDataSender();
                    sender.Send(new[] { userLoginLog });
                });

                return true;
            }
            catch (Exception ex)
            {
                SaveLog(ex);
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.ValidateError;
                return false;
            }
        }


    }
}
