
using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.Model.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Security;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Sns;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 通行证
    /// </summary>
    public class Action10020 : BaseStruct
    {
        private string passport = string.Empty;
        private string password = string.Empty;
        private string logindata = string.Empty;
        private string md5key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDXdPs18RFj4XBEzbPNZ+58CsPC7AeVhZ3zWxVKPfozuwVCR1kDhYp/5e2tMVoleayDpAq/2FJUNTbTu5eYkow11Cho2RRGuMRRhl0RJ0lqItuwbe4a8/D2cgqsw+BxrZLcWO0xpnE7NGTkMc7sRz60Muq5izhLYrDUn/KUd7qi/QIDAQAB";



        private class JsonData
        {
            public string openid { get; set; }
            public string nickname { get; set; }
            public int sex { get; set; }
            public string language { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string headimgurl { get; set; }
            public List<string> privilege { get; set; }
            public string unionid { get; set; }
            public string code { get; set; }
        }


        public Action10020(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10020, actionGetter)
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
                Data = new JPPassData()
                {
                    PassportId = passport,
                    Password = password
                }
            };
            return MathUtils.ToJson(resultData);
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("LoginData", ref logindata))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            try
            {
                JsonData data = JsonUtils.Deserialize<JsonData>(logindata);
                    
                string sign = CryptoHelper.MD5_Encrypt(data.openid + md5key);
                if (data.code.CompareTo(sign) != 0)
                {
                    return false;
                }

                var ucpcache = new ShareCacheStruct<UserCenterPassport>();
                var ucp = ucpcache.Find(t => (t.OpenId.CompareTo(data.openid) == 0));
                if (ucp == null)
                {
                    Util.CrateAccountByOpenId(data.openid, out passport, out password);
                }
                else
                {
                    passport = ucp.PassportID;
                    password = ucp.Password;
                }
                return true;
            }
            catch (Exception ex)
            {
                SaveLog(ex);
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.St1002_GetRegisterPassportIDError;
                return false;
            }
        }
    }
}