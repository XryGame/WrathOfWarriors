
using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ZyGames.Framework.Common;
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
        //private int mobileType = 0;
        //private int gameType = 0;
        //private string retailID = string.Empty;
        //private string clientAppVersion = string.Empty;
        //private int ScreenX = 0;
        //private int ScreenY = 0;

        public Action10020(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10020, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        //public override void BuildPacket()
        //{
        //    PushIntoStack(passport);
        //    PushIntoStack(password);
        //}

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
            }
            else
            {
                return false;
            }
            return true;
        }

        public override bool TakeAction()
        {
            try
            {
                try
                {
                    UnicodeEncoding ByteConverter = new UnicodeEncoding();

                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    RSA.FromXmlString("");

                    RSAParameters rsaParameters = new RSAParameters();

                    string publkey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDXdPs18RFj4XBEzbPNZ+58CsPC7AeVhZ3zWxVKPfozuwVCR1kDhYp / 5e2tMVoleayDpAq / 2FJUNTbTu5eYkow11Cho2RRGuMRRhl0RJ0lqItuwbe4a8 / D2cgqsw + BxrZLcWO0xpnE7NGTkMc7sRz60Muq5izhLYrDUn / KUd7qi / QIDAQAB";
                    string data = "j3GqT+15mgi8L6/vjksOUNOVj1X5kpL8RX6GiZPodbnEqnu8KAS6PnqG2c00WQDS891K15UPaisp7lW40hgCWNFjJrdxz6f8irOS6oBhWAfQcwgKkivqKP/lD0JmVIGB5UOMcUDyAJUVR3NMP5sto6dRfGGSGRX5YU5c7KL9hKg=";
                    rsaParameters.Exponent = Convert.FromBase64String("AQAB");
                    rsaParameters.Modulus =
                        Convert.FromBase64String(
                            publkey
                            );

                    byte[] encryptedData;
                    byte[] decryptedData;

                    encryptedData = Convert.FromBase64String(data);

                    decryptedData = Util.RSADeCrtypto(encryptedData, rsaParameters, true);
                    string ddd = ByteConverter.GetString(decryptedData);
                }
                catch (Exception)
                {
                    return false;
                }







                //string openId = "d4ddg555w222222ddg";
                //string[] userList = SnsManager.GetRegPassportByOpenId(openId);
                //passport = userList[0];
                //password = userList[1];

                //SnsUser snsuser = SnsManager.LoginByWeixin("d4ddg555w222222ddg");
                //if (snsuser == null)
                //{

                //}
                //passport = snsuser.PassportId;
                //password = snsuser.Password;


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