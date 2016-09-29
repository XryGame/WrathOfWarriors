using GameServer.Script.Model.Config;
using System;
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

    }
}