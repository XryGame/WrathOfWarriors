using GameServer.CsScript.Remote;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using ZyGames.Framework.Common.Log;

namespace GameServer.CsScript.Base
{
    internal class NewHttpListener
    {
        private IPEndPoint _IP;
        private TcpListener _Listeners;
        private volatile bool IsInit = false;
        HashSet<string> Names;

        /// <summary>
              /// 初始化服务器
              /// </summary>
        public NewHttpListener(string ip, int port, HashSet<string> names)
        {

            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip), port);
            this._IP = localEP;
            Names = names;
            if (Names == null)
            {
                Names = new HashSet<string>();
            }
            try
            {
                foreach (var item in names)
                {
                    TraceLog.WriteInfo("Http service:{0}:{1}/{2} is started.", ip, port, item);
                }
                this._Listeners = new TcpListener(IPAddress.Parse(ip), port);
                this._Listeners.Start(5000);
                IsInit = true;
                this.AcceptAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                this.Dispose();
            }
        }

        private void AcceptAsync()
        {
            try
            {
                this._Listeners.BeginAcceptTcpClient(new AsyncCallback(AcceptAsync_Async), null);
            }
            catch (Exception) { }
        }

        private void AcceptAsync_Async(IAsyncResult iar)
        {
            this.AcceptAsync();
            try
            {
                TcpClient client = this._Listeners.EndAcceptTcpClient(iar);
                var response = new NewHttpResponse(client);
                foreach (var item in Names)
                {
                    if (response.http_url.Contains(item))
                    {
                        try
                        {
                            if (response.isGet())
                            {
                                if (item.Equals("GamePay.aspx"))
                                {
                                    new OnPay().ActiveHttp(response, response.GetRequestExec());
                                }
                            }
                            else if (response.isPost())
                            {
                                if (item.Equals("GMCommon.aspx"))
                                {
                                    new OnGMCommon().ActiveHttp(response, response.PostRequestExec());
                                }
                            }
                        }
                        catch { break; }
                    }
                }
                response.WriteFailure();
                response.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (IsInit)
            {
                IsInit = false;
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// 释放所占用的资源
        /// </summary>
        /// <param name="flag1"></param>
        protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool flag1)
        {
            if (flag1)
            {
                if (_Listeners != null)
                {
                    try
                    {
                        TraceLog.WriteInfo(string.Format("Stop Http Listener -> {0}:{1} ", this.IP.Address.ToString(), this.IP.Port));
                        _Listeners.Stop();
                        _Listeners = null;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 获取绑定终结点
        /// </summary>
        public IPEndPoint IP { get { return this._IP; } }
    }


}