using GameServer.Script.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Runtime;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 全局工具类
    /// </summary>
    public static class Util
    {

        private static List<string> CoinUnits = new List<string> () { "K", "M", "B", "T" };

        private static List<string> Letter = new List<string>() {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        };

        static Util()
        {
            CoinUnits.AddRange(Letter);
            for (int i = 0; i < 26; ++i)
            {
                for (int j = 0; j < 26; ++j)
                {
                    CoinUnits.Add(Letter[i] + Letter[j]);
                }
            }
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        public static long ConvertDateTimeStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 格式化日期显示，昨天，前天
        /// </summary>
        /// <param name="sendDate"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime sendDate)
        {
            string result = sendDate.ToString("HH:mm:ss");
            if (sendDate.Date == DateTime.Now.Date)
            {
                return result;
            }
            if (DateTime.Now > sendDate)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(sendDate);
                //TimeSpan timeSpan = DateTime.Now.Date - sendDate.Date;
                int day = (int)Math.Floor(timeSpan.TotalDays);
                if (day == 1)
                {
                    return string.Format("{0} {1}", Language.Instance.Date_Yesterday, result);
                }
                if (day == 2)
                {
                    return string.Format("{0} {1}", Language.Instance.Date_BeforeYesterday, result);
                }
                return string.Format("{0} {1}", string.Format(Language.Instance.Date_Day, day), result);
            }
            return result;
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="DataToDeCrypto">要解密的数据</param>
        /// <param name="RSAKeyInfo"></param>
        /// <param name="DoOAEPPadding"></param>
        /// <returns></returns>
        static public byte[] RSADeCrtypto(byte[] DataToDeCrypto, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                // System.Security.Cryptography.RSA 的参数。
                RSA.ImportParameters(RSAKeyInfo);
                //
                // 参数:
                //  
                //     要解密的数据。
                //
                //
                //     如果为 true，则使用 OAEP 填充（仅在运行 Microsoft Windows XP 或更高版本的计算机上可用）执行直接的 System.Security.Cryptography.RSA
                //     解密；否则，如果为 false，则使用 PKCS#1 1.5 版填充。
                return RSA.Decrypt(DataToDeCrypto, DoOAEPPadding);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>
        /// 获取6位随机密码
        /// </summary>
        /// <returns></returns>
        static public string GetRandom6Pwd()
        {
            Random random = new Random();
            int rid = random.Next(0, 999999);
            return rid.ToString().PadLeft(6, '0');
        }

        /// <summary>
        /// 获取4位随机密码
        /// </summary>
        /// <returns></returns>
        static public string GetRandom4Pwd()
        {
            Random random = new Random();
            int rid = random.Next(0, 9999);
            return rid.ToString().PadLeft(4, '0');
        }
        /// <summary>
        /// 获取随机GUID密码
        /// </summary>
        /// <returns></returns>
        static public string GetRandomGUIDPwd()
        {
            return Guid.NewGuid().ToString("N");
        }

        static public UserCenterUser CreateUserCenterUser(string openid, string retailId, int serverId)
        {
            var cache = new ShareCacheStruct<UserCenterUser>();
            Interlocked.Increment(ref SystemGlobal.UserCenterUserCount);
            var ucu = new UserCenterUser()
            {
                UserID = GameEnvironment.ProductServerId * 1000000 + SystemGlobal.UserCenterUserCount,
                NickName = string.Empty,
                OpenID = openid,
                ServerID = serverId,
                AccessTime = DateTime.Now,
                LoginNum = 0,
                RetailID = retailId
            };
            cache.Add(ucu);
            cache.Update();

            return ucu;
        }


        static public List<UserCenterUser> FindUserCenterUser(string openid, string retailId, int serverId)
        {
            var cache = new ShareCacheStruct<UserCenterUser>();
            return cache.FindAll(t => (t.OpenID == openid && t.RetailID == retailId && t.ServerID == serverId));
        }


        static public string ConvertGameCoinUnits(string strValue)
        {
            try
            {
                BigInteger tmp = 0;
                BigInteger bi = BigInteger.Parse(strValue);

                int count = strValue.Length / 3 - 1;
                string units = string.Empty;
                if (strValue.Length <= 5)
                {
                    count = 0;
                }

                if (count > 0)
                {
                    units = CoinUnits[count - 1];
                }

                for (int i = 0; i < count; ++i)
                {
                    bi /= 1000;
                }

                return bi.ToString() + units;
            }
            catch (Exception e)
            {
                TraceLog.WriteError("ConvertGameCoinUnits Error:{0}", e);
            }
            
            return "0";
        }

        static public string ConvertGameCoinString(string unitsValue)
        {

            try
            {
                string tmp = unitsValue;
                while (!tmp.IsEmpty())
                {
                    if (tmp[0] >= '0' && tmp[0] <= '9')
                        tmp = tmp.Substring(1);
                    else
                        break;
                }
                if (tmp.IsEmpty())
                {
                    return unitsValue;
                }
                int index = unitsValue.IndexOf(tmp);
                unitsValue = unitsValue.Substring(0, index);
                index = CoinUnits.IndexOf(tmp);
                for (int i = 0; i < index + 1; ++i)
                {
                    unitsValue += "000";
                }
                
                return unitsValue;
            }
            catch (Exception e)
            {
                TraceLog.WriteError("ConvertGameCoinString Error:{0}", e);
            }
            
            return "0";
        }

        static public BigInteger ConvertGameCoin(string unitsValue)
        {
            string strValue = ConvertGameCoinString(unitsValue);
            return BigInteger.Parse(strValue);
        }

    }
}