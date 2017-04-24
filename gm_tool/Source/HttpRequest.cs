using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;

namespace gm_tool.Source
{

    public class HttpRequest
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }
        

        private HttpParameters _PostParameters = new HttpParameters();
        private HttpParameters _ReceiveParameters = null;

        public bool IsWriteLog = true;

        public bool HttpPostRequest(string url = "http://168.254.0.254:8091/GMCommon.aspx?")
        {
            int resultCode = -1;
            try
            {
                HttpWebRequest request = null;
                Encoding charset = Encoding.GetEncoding("utf-8");
                //HTTPSQ请求  
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = DefaultUserAgent;
                request.Timeout = 5000;
                //如果需要POST数据     
                if (!(_PostParameters == null || _PostParameters.GetParam.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in _PostParameters.GetParam.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, _PostParameters.GetParam[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, _PostParameters.GetParam[key]);
                        }
                        i++;
                    }
                    byte[] data = charset.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                WebResponse webresponse = request.GetResponse();
                _ReceiveParameters = new HttpParameters(webresponse as HttpWebResponse);
                resultCode = Convert.ToInt32(GetReceiveValue("ResultCode"));

                if (IsWriteLog)
                {
                    Log.Write(_ReceiveParameters);
                }
                
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            
            return resultCode == 0;
        }

        public bool HttpGetRequest(string url)
        {
            try
            {
                Encoding charset = Encoding.GetEncoding("utf-8");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                request.Method = "GET";
                WebResponse webresponse = request.GetResponse();
                HttpWebResponse httpResponse = (HttpWebResponse)webresponse;
                _ReceiveParameters = new HttpParameters(httpResponse, true);
                int status = (int)httpResponse.StatusCode;

            }
            catch (Exception e)
            {
                Log.Write(e.Message);
                return false;
            }

            return true;
        }

        public void AddPostParam(string key, string value)
        {
            _PostParameters.AddParam(key, value);
        }

        public HttpParameters GetPostParam
        {
            get { return _PostParameters; }
        }

        public HttpParameters GetReceiveParam
        {
            get { return _ReceiveParameters; }
        }

        public string GetReceiveValue(string key = "GetValue")
        {
            return  _ReceiveParameters.GetValue(key);
        }

    }
}