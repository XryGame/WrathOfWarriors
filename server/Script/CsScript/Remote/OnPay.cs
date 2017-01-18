using GameServer.CsScript.Base;
using GameServer.CsScript.GM;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Security;
using ZyGames.Framework.Game.Contract;

namespace GameServer.CsScript.Remote
{

    public class OnPay : HttpMessageInterface
    {
        public static readonly OnPay instance = new OnPay();

        private string _data;
        private JsonResult receipt;

        private readonly string md5key = "6sd5744smnye33pkr1b15678fdvsfgqutw";

        public class JsonOrderInfo
        {
            // 订单ID
            public string OrderId { get; set; }
            // CP订单ID
            public string CpOrderId { get; set; }
            // OpenId
            public string OpenId { get; set; }
            // 用户角色Id(优先使用OpenId + RetailID + ServerID作为充值目标的唯一标示符)
            public int UserId { get; set; }
            // 平台自定义支付ID
            public int RcId { get; set; }
            // 支付金额
            public int Amount { get; set; }
            // 自定义数据
            public string CustomData { get; set; }

            public string Sign { get; set; }
        }

        public class JsonCustomData
        {
            //  区服ID
            public int ServerID { get; set; }

            // 渠道ID
            public string RetailID { get; set; }

            // 支付表支付ID
            public int PayId { get; set; }
        }


        public class JsonResult
        {
            public int ResultCode { get; set; }

            public string ResultString { get; set; }

        }

        public void ActiveHttp(NewHttpResponse client, Dictionary<string, string> parms)
        {
            JsonResult receipt = verifyDeliver(parms);
            string responseData = HttpUtility.UrlEncode(MathUtils.ToJson(receipt));
            client.OutputStream.WriteLine(responseData);
            client.Close();
        }

        private JsonResult verifyDeliver(Dictionary<string, string> parms)
        {
            receipt = new JsonResult();
            receipt.ResultCode = 0;
            JsonOrderInfo jsonorder = null;
            JsonCustomData jsoncustom = null;
            try
            {
                parms.TryGetValue("DATA", out _data);

                jsonorder = MathUtils.ParseJson<JsonOrderInfo>(_data);
                if (jsonorder == null)
                {
                    receipt.ResultString = "DATA 数据解析错误";
                    return receipt;
                }
                jsonorder.CustomData = CryptoHelper.HttpBase64Decode(jsonorder.CustomData);
                jsonorder.CustomData = HttpUtility.UrlDecode(jsonorder.CustomData);
                jsoncustom = MathUtils.ParseJson<JsonCustomData>(jsonorder.CustomData);
                if (jsoncustom == null)
                {
                    receipt.ResultString = "CustomData 自定义数据解析错误";
                    return receipt;
                }
            }
            catch (Exception e)
            {
                receipt.ResultString = "解析失败！JSON格式错误";
                TraceLog.WriteError(string.Format("{0}\n {1}\n {2}", receipt.ResultString, _data, e));

                return receipt;
            }

            try
            {
                // MD5
                string signParameter = md5key + jsonorder.OrderId + jsonorder.CpOrderId + jsonorder.Amount + jsoncustom.PayId;
                string sign = CryptoHelper.MD5_Encrypt(signParameter, Encoding.UTF8).ToLower();
                if (sign.CompareTo(jsonorder.Sign) != 0)
                {
                    receipt.ResultString = "MD5验证失败";
                    return receipt;
                }

                var orderInfoCache = new ShareCacheStruct<OrderInfoCache>();
                var orderinfo = orderInfoCache.FindKey(jsonorder.OrderId);
                if (orderinfo != null)
                {// 如果是已经发货了，返回成功
                    receipt.ResultCode = 1;
                    receipt.ResultString = "该订单已经发货";
                    return receipt;
                }

                GameUser user = UserHelper.FindUserOfRetail(jsoncustom.RetailID, jsonorder.OpenId, jsoncustom.ServerID);
                if (user == null)
                {// 优先使用OpenId + RetailID + ServerID来获取充值角色
                    user = UserHelper.FindUser(jsonorder.UserId);
                    if (user == null)
                    {
                        receipt.ResultString = "没有找到该玩家";
                        return receipt;
                    }
                }

                UserPayCache userpay = UserHelper.FindUserPay(user.UserID);
                if (userpay == null)
                {
                    receipt.ResultString = "没有找到该玩家充值信息表";
                    return receipt;
                }


                var paycfg = new ShareCacheStruct<Config_Pay>().FindKey(jsoncustom.PayId);
                if (paycfg == null)
                {
                    receipt.ResultString = "PayId 错误";
                    return receipt;
                }

                int deliverNum = paycfg.AcquisitionDiamond + paycfg.PresentedDiamond;

                if (!new PayMoneyCommand().PayMoney(user.UserID, paycfg.PaySum))
                {
                    receipt.ResultString = "发货Money失败";
                    return receipt;
                }

                if (!new DiamondCommand().AddUserDiamond(deliverNum, user.UserID))
                {
                    receipt.ResultString = "发货Diamond失败";
                    return receipt;
                }

                if (paycfg.id == 101)
                {// 是否周卡
                    new PayWeekCardCommand().PayWeekCard(user.UserID);
                }
                else if (paycfg.id == 102)
                {// 是否月卡
                    new PayMonthCardCommand().PayMonthCard(user.UserID);
                }
                    


                OrderInfoCache newOrderInfo = new OrderInfoCache()
                {
                    OrderId = jsonorder.OrderId,
                    UserId = user.UserID,
                    MerchandiseName = paycfg.Identifying,
                    PayId = jsoncustom.PayId,
                    Amount = jsonorder.Amount,
                    PassportID = user.Pid,
                    ServerID = user.EnterServerId,
                    GameCoins = deliverNum,
                    CreateDate = DateTime.Now,
                    RetailID = jsoncustom.RetailID,
                    RcId = jsonorder.RcId
                };
                orderInfoCache.Add(newOrderInfo);
                orderInfoCache.Update();

                PushMessageHelper.UserPaySucceedNotification(GameSession.Get(user.UserID));

                receipt.ResultCode = 1;
                receipt.ResultString = "SUCCEED";

            }
            catch (Exception e)
            {
                receipt.ResultString = "发货过程出现异常";
                TraceLog.WriteError(string.Format("{0}\n {1}\n {2}", receipt.ResultString, _data, e));
                
                return receipt;
            }
            return receipt;
        }
    }

  
}