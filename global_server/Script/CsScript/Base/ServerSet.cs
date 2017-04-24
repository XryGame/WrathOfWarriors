using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Sns._91sdk;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Base
{
    public class ServerInfo
    {

        /// <summary>
        /// 游戏服编号
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// 游戏服名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 游戏服访问地址
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// 分服状态，停服、顺畅、拥挤等
        /// </summary>
        public string Status { get; set; }

        public int StatusCode { get; set; }
        
        /// <summary>
        /// 排序的权重比
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 合服转向目录务器
        /// </summary>
        public int TargetServer { get; set; }

        
    }
    /// <summary>
    /// 分区
    /// </summary>
    public static class ServerSet
    {
        public static List<ServerInfo> Set = new List<ServerInfo>();
        public static void LoadServerConfig()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["ServerSerUrl"];
                HttpStatusCode statusCode = (HttpStatusCode)0;
                byte[] data = HttpPostManager.GetPostData(url, null, out statusCode);

                if (data != null && data.Length > 0)
                {
                    MessageStructure ms = new MessageStructure(data);
                    int length = ms.ReadInt();
                    int errcode = ms.ReadInt();
                    int msgid = ms.ReadInt();
                    string errorInfo = ms.ReadString();
                    int actionId = ms.ReadInt();
                    string st = ms.ReadString();
                    int lastloginid = ms.ReadInt();
                    int listsize = ms.ReadInt();
                    for (int i = 0; i < listsize; ++i)
                    {
                        ServerInfo info = new ServerInfo();
                        info.ID = ms.ReadInt();
                        info.ServerName = ms.ReadString();
                        info.Status = ms.ReadString();
                        info.StatusCode = ms.ReadInt();
                        info.ServerUrl = ms.ReadString();
                        info.Weight = ms.ReadInt();
                        info.TargetServer = ms.ReadInt();
                        Set.Add(info);
                    }

                    TraceLog.WriteLine("Request server list successful!");
                }
                else
                {
                    TraceLog.ReleaseWrite("Request server list fail result:{0}, request url:{1}", data, url);
                    return;
                }
            }
            catch (Exception ex)
            {
                new BaseLog().SaveLog(ex);
                return;
            }
        }
    }
}