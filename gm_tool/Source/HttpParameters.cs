using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Web;
using ZyGames.Framework.Common;

namespace gm_tool.Source
{

    public class HttpParameters
    {
        private IDictionary<string, string> parameters = new Dictionary<string, string>();
        public HttpParameters()
        {

        }
        
        public HttpParameters(HttpWebResponse response)
        {
            Stream stream = response.GetResponseStream();   //获取响应的字符串流  
            StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
            string data = sr.ReadToEnd();   //从头读到尾，放到字符串html  
            data = HttpUtility.UrlDecode(data);
            parameters = GetData(data);
        }

        /// <summary>
        /// 分析http提交数据分割
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetData(string rawData)
        {
            var rets = new Dictionary<string, string>();
            if (rawData.IsEmpty())
                return rets;
            string[] rawParams = rawData.Split('&');
            foreach (string param in rawParams)
            {
                int indexof = param.IndexOf('=');
                string key = param.Substring(0, indexof);
                string value = HttpUtility.UrlDecode(param.Substring(indexof + 1));
                //string[] kvPair = param.Split('=');
                //string key = kvPair[0];
                //string value = HttpUtility.UrlDecode(kvPair[1]);
                rets[key] = value;
            }
            return rets;
        }

        public void AddParam(string key, string value)
        {
            parameters.Add(key, value);
        }

        public IDictionary<string, string> GetParam
        {
            get { return parameters; }
        }

        public string GetValue(string key)
        {
            string value;
            parameters.TryGetValue(key, out value);
            return value;
        }
    }

}