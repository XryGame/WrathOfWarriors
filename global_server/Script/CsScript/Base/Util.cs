using System;
using System.Security.Cryptography;
using ZyGames.Framework.Game.Lang;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 全局工具类
    /// </summary>
    public static class Util
    {

        static Util()
        {

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
        static public string GetRandomPwd()
        {
            Random random = new Random();
            int rid = random.Next(0, 999999);
            return rid.ToString().PadLeft(6, '0');
        }
        /// <summary>
        /// 获取随机GUID密码
        /// </summary>
        /// <returns></returns>
        static public string GetRandomGUIDPwd()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}