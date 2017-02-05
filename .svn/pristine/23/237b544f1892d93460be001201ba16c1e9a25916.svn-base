
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



        private class JsonBaseData
        {
            public string openid { get; set; }

            public string RetailID { get; set; }
            

        }

        private class JsonGuanWang : JsonBaseData
        {
            public string code { get; set; }

        }

        private class JsonXiaoHuoBan : JsonBaseData
        {
            public string PassportId{ get; set; }

            public string Password { get; set; }

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
                UserCenterPassport UCP = null;
                JsonBaseData data = JsonUtils.Deserialize<JsonBaseData>(logindata);
                if (data.openid.IsEmpty())
                {
                    return false;
                }

                if (data.RetailID == "WeiXin")
                {
                    JsonGuanWang guanwang = JsonUtils.Deserialize<JsonGuanWang>(logindata);
                    string sign = CryptoHelper.MD5_Encrypt(guanwang.openid + md5key);
                    if (guanwang.code.CompareTo(sign) != 0)
                    {
                        return false;
                    }
                }
                else if (data.RetailID == "XiaoHuoBan")
                {
                    JsonXiaoHuoBan xiaohuoban = JsonUtils.Deserialize<JsonXiaoHuoBan>(logindata);

                    UCP = Util.FindAccountByOpenId(data.openid, data.RetailID);
                    if (UCP == null)
                    {
                        UCP = new ShareCacheStruct<UserCenterPassport>().FindKey(xiaohuoban.PassportId);
                        if (UCP != null)
                        {
                            if (UCP.Password.CompareTo(xiaohuoban.Password) != 0)
                            {
                                return false;
                            }

                            // 绑定正式账号
                            if (UCP.BindOpenId.IsEmpty() && UCP.OpenId != xiaohuoban.openid)
                            {
                                UCP.BindOpenId = xiaohuoban.openid;
                            }
                            UCP = null;
                        }
                    }

                }
                if (UCP == null)
                {
                    UCP = Util.FindAccountByOpenId(data.openid, data.RetailID);
                }
                
                if (UCP == null)
                {
                    Util.CrateAccountByOpenId(data.openid, data.RetailID, out passport, out password);
                }
                else
                {
                    passport = UCP.PassportID;
                    password = UCP.Password;
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