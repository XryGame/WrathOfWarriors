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

//using GameServer.CsScript.GM;
//using GameServer.CsScript.JsonProtocol;
//using GameServer.Script.Model.ConfigModel;
//using GameServer.Script.Model.DataModel;
//using System;
//using System.Text;
//using ZyGames.Framework.Cache.Generic;
//using ZyGames.Framework.Common;
//using ZyGames.Framework.Common.Security;
//using ZyGames.Framework.Game.Service;
//using ZyGames.Framework.RPC.Sockets;

//namespace GameServer.CsScript.Action
//{
//    /// <summary>
//    /// 1000_订单发货接口
//    /// </summary>
//    public class Action1000 : BaseStruct
//    {
//        private string _data;
//        private JsonResult receipt;

//        private readonly string md5key = "fsdfdsdshyye334551215678fddsfsfdsf";
//        public Action1000(ActionGetter actionGetter)
//            : base(ActionIDDefine.Cst_Action1000, actionGetter)
//        {
//            IsWebSocket = true;
//            actionGetter.OpCode = OpCode.Text;
//        }

//        protected override string BuildJsonPack()
//        {
//            return MathUtils.ToJson(receipt);
//        }

//        public override bool GetUrlElement()
//        {
//            if (httpGet.GetString("DATA", ref _data))
//            {
//                return true;
//            }
//            return false;
//        }

//        public override bool TakeAction()
//        {
//            receipt = new JsonResult();
//            receipt.ResultCode = 0;
//            JsonOrderInfo jsonorder = MathUtils.ParseJson<JsonOrderInfo>(_data);
//            if (jsonorder == null)
//            {
//                receipt.ResultString = "数据解析错误";
//                return true;
//            }
//            JsonCustomData jsoncustom = MathUtils.ParseJson<JsonCustomData>(jsonorder.CustomData);
//            if (jsoncustom == null)
//            {
//                receipt.ResultString = "自定义数据解析错误";
//                return true;
//            }

//            // MD5
//            string signParameter = md5key + jsonorder.OrderId + jsonorder.CpOrderId + jsonorder.Amount + jsoncustom.PayId;
//            string sign = CryptoHelper.MD5_Encrypt(signParameter, Encoding.UTF8).ToLower();
//            if (sign.CompareTo(jsonorder.Sign) != 0)
//            {
//                receipt.ResultString = "MD5验证失败";
//                return true;
//            }

//            GameUser user = UserHelper.FindUser(jsonorder.UserId);
//            if (user == null || user.EnterServerId != jsoncustom.ServerID)
//            {
//                receipt.ResultString = "没有找到该玩家";
//                return true;
//            }

//            UserPayCache userpay = UserHelper.FindUserPay(user.UserID);
//            if (userpay == null)
//            {
//                receipt.ResultString = "没有找到该玩家充值信息表";
//                return true;
//            }
//            var orderInfoCache = new ShareCacheStruct<OrderInfoCache>();
//            var orderinfo = orderInfoCache.FindKey(jsonorder.OrderId);
//            if (orderinfo != null && orderinfo.UserId == jsonorder.UserId)
//            {
//                receipt.ResultString = "该订单已经发货";
//                return true;
//            }

//            var paycfg = new ShareCacheStruct<Config_Pay>().FindKey(jsoncustom.PayId);
//            if (paycfg == null)
//            {
//                receipt.ResultString = "PayId 错误";
//                return true;
//            }

//            int deliverNum = paycfg.AcquisitionDiamond + paycfg.PresentedDiamond;
//            try
//            {
//                if (!new PayMoneyCommand().PayMoney(user.UserID, paycfg.PaySum))
//                {
//                    receipt.ResultString = "发货Money失败";
//                    return true;
//                }
                
//                if (!new DiamondCommand().AddUserDiamond(deliverNum, user.UserID))
//                {
//                    receipt.ResultString = "发货Diamond失败";
//                    return true;
//                }

//                if (paycfg.id == 101)
//                {// 是否周卡
//                    new PayWeekCardCommand().PayWeekCard(user.UserID);
//                }
//                else if (paycfg.id == 102)
//                {// 是否月卡
//                    new PayMonthCardCommand().PayMonthCard(user.UserID);
//                }
//            }
//            catch (Exception)
//            {
//                receipt.ResultString = "发货失败";
//                return true;
//            }

//            receipt.ResultCode = 1;
//            receipt.ResultString = "SUCCEED";

//            OrderInfoCache newOrderInfo = new OrderInfoCache()
//            {
//                OrderId = jsonorder.OrderId,
//                UserId = user.UserID,
//                MerchandiseName = paycfg.Identifying,
//                PayId = jsoncustom.PayId,
//                Amount = jsonorder.Amount,
//                PassportID = user.Pid,
//                ServerID = jsoncustom.ServerID,
//                GameCoins = deliverNum,
//                CreateDate = DateTime.Now,
//                RetailID = jsoncustom.RetailID,
//                RcId = jsonorder.RcId
//            };
//            orderInfoCache.Add(newOrderInfo);
//            orderInfoCache.Update();

//            return true;
//        }
//    }

//    public class JsonOrderInfo
//    {
//        // 订单ID
//        public string OrderId { get; set; }
//        // CP订单ID
//        public string CpOrderId { get; set; }
//        // 用户ID
//        public int UserId { get; set; }
//        // 平台自定义支付ID
//        public int RcId { get; set; }
//        // 支付金额
//        public int Amount { get; set; }
//        // 自定义数据
//        public string CustomData { get; set; }

//        public string Sign { get; set; }
//    }

//    public class JsonCustomData
//    {
//        //  区服ID
//        public int ServerID { get; set; }

//        // 渠道ID
//        public string RetailID { get; set; }

//        // 支付表支付ID
//        public int PayId { get; set; }
//    }


//    public class JsonResult
//    {
//        public int ResultCode { get; set; }

//        public string ResultString { get; set; }

//    }
//}