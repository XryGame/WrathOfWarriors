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

    public class OnGMDeliverGoods : HttpMessageInterface
    {
        public static readonly OnGMDeliverGoods instance = new OnGMDeliverGoods();

        private string _data;
        private JsonResult receipt;

        private readonly string md5key = "6sd5744smnye33pkr1b15678fdvsfgqutw";

        public class JsonInfo
        {
            // 角色ID
            public int UserId { get; set; }

            //  区服ID
            public int ServerID { get; set; }

            // 支付表支付ID
            public int PayId { get; set; }

            public string Sign { get; set; }
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

            try
            {
                while (true)
                {
                    parms.TryGetValue("DATA", out _data);

                    JsonInfo jsoninfo = MathUtils.ParseJson<JsonInfo>(_data);
                    if (jsoninfo == null)
                    {
                        receipt.ResultString = "数据解析错误";
                        break;
                    }
                    
                    // MD5
                    string signParameter = md5key + jsoninfo.UserId + jsoninfo.ServerID + jsoninfo.PayId;
                    string sign = CryptoHelper.MD5_Encrypt(signParameter, Encoding.UTF8).ToLower();
                    if (sign.CompareTo(jsoninfo.Sign) != 0)
                    {
                        receipt.ResultString = "MD5验证失败";
                        break;
                    }

                    GameUser user = UserHelper.FindUser(jsoninfo.UserId);
                    if (user == null || user.EnterServerId != jsoninfo.ServerID)
                    {
                        receipt.ResultString = "没有找到该玩家";
                        break;
                    }

                    UserPayCache userpay = UserHelper.FindUserPay(user.UserID);
                    if (userpay == null)
                    {
                        receipt.ResultString = "没有找到该玩家充值信息表";
                        break;
                    }

                    var paycfg = new ShareCacheStruct<Config_Pay>().FindKey(jsoninfo.PayId);
                    if (paycfg == null)
                    {
                        receipt.ResultString = "PayId 错误";
                        break;
                    }

                    int deliverNum = paycfg.AcquisitionDiamond + paycfg.PresentedDiamond;
                    int oldVipLv = user.VipLv;
                    if (!new PayMoneyCommand().PayMoney(user.UserID, paycfg.PaySum))
                    {
                        receipt.ResultString = "发货Money失败";
                        break;
                    }

                    if (!new DiamondCommand().AddUserDiamond(deliverNum, user.UserID))
                    {
                        receipt.ResultString = "发货Diamond失败";
                        break;
                    }

                    if (paycfg.id == 101)
                    {// 是否周卡
                        new PayWeekCardCommand().PayWeekCard(user.UserID);
                    }
                    else if (paycfg.id == 102)
                    {// 是否月卡
                        new PayMonthCardCommand().PayMonthCard(user.UserID);
                    }

                    PushMessageHelper.UserPaySucceedNotification(GameSession.Get(user.UserID));

                    if (oldVipLv != user.VipLv)
                    {
                        UserHelper.VipLvChangeNotification(user.UserID);
                    }

                    receipt.ResultCode = 1;
                    receipt.ResultString = "SUCCEED";

                    break;
                }
            }
            catch (Exception e)
            {
                receipt.ResultString = "Url参数格式错误";
                TraceLog.WriteError(string.Format("{0} {1}", receipt.ResultString, e));
                
                return receipt;
            }
            return receipt;
        }
    }

  
}