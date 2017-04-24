using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Remote
{

    public static class GlobalRemoteService
    {
        private static RemoteService remote;

        public static void Reuest()
        {
            //创建一个代理类RemoteService的实例
            try
            {
                string addr = ConfigurationManager.AppSettings["GlobalServiceAddr"];
                remote = RemoteService.CreateWebSocketProxy("Global", addr, 30 * 1000, 13);
            }
            catch(Exception e)
            {
                TraceLog.WriteError(string.Format("Connect to ChatService failed!!!\n{0}", e));
            }
            

        }

        public static void SendSystemChat(int receiver, string content)
        {
            var basis = UserHelper.FindUserBasis(receiver);
            if (basis == null)
                return;
            var param = new RequestParam();
            param.Add("Type", (int)ChatType.System);
            param.Add("Sender", 0);
            param.Add("Receiver", receiver);
            param.Add("ServerID", basis.ServerID);
            param.Add("SendDate", Util.GetTimeStamp());
            SendChat(param, content);
        }

        public static void SendWhisperChat(int sender, int receiver, string content)
        {
            var basis = UserHelper.FindUserBasis(sender);
            if (basis == null)
                return;
            var param = new RequestParam();
            param.Add("Type", (int)ChatType.Whisper);
            param.Add("Sender", sender);
            param.Add("Receiver", receiver);
            param.Add("ServerID", basis.ServerID);
            param.Add("SendDate", Util.GetTimeStamp());
            SendChat(param, content);
        }

        public static void SendAllServerChat(int sender, string content)
        {
            var basis = UserHelper.FindUserBasis(sender);
            if (basis == null)
                return;
            var param = new RequestParam();
            param.Add("Type", (int)ChatType.AllService);
            param.Add("Sender", sender);
            param.Add("Receiver", 0);
            param.Add("ServerID", basis.ServerID);
            param.Add("SendDate", Util.GetTimeStamp());
            SendChat(param, content);
        }

        public static void SendWorldChat(int sender, string content)
        {
            var basis = UserHelper.FindUserBasis(sender);
            if (basis == null)
                return;
            var param = new RequestParam();
            param.Add("Type", (int)ChatType.World);
            param.Add("Sender", sender);
            param.Add("Receiver", 0);
            param.Add("ServerID", basis.ServerID);
            param.Add("SendDate", Util.GetTimeStamp());
            SendChat(param, content);
        }

        public static void SendGuildChat(int sender, string content)
        {
            var basis = UserHelper.FindUserBasis(sender);
            if (basis == null)
                return;
            var param = new RequestParam();
            param.Add("Type", (int)ChatType.Guild);
            param.Add("Sender", sender);
            param.Add("Receiver", 0);
            param.Add("ServerID", basis.ServerID);
            param.Add("SendDate", Util.GetTimeStamp());
            SendChat(param, content);
        }

        public static void SendNotice(NoticeMode type, string content)
        {
            var param = new RequestParam();
            param.Add("Type", type);
            param.Add("ServerID", GameEnvironment.ProductServerId);
            param.Add("Content", content);
            remote.Call("NoticeService", param, successCallback);
        }

        public static void SendUserData(UserBasisCache basis, string guildId)
        {
            var param = new RequestParam();
            param.Add("UserId", basis.UserID);
            param.Add("UserName", basis.NickName);
            param.Add("VipLv", basis.VipLv);
            param.Add("Profession", basis.Profession);
            param.Add("ServerID", basis.ServerID);
            param.Add("GuildID", guildId);
            remote.Call("UpdateUserDataService", param, successCallback);
        }

        private static void SendChat(RequestParam param, string content)
        {
            content = new KeyWordCheck().FilterMessage(content);
            param.Add("Content", content);
            remote.Call("ChatService", param, successCallback);
        }



        public static void successCallback(RemotePackage package)
        {
            //var reader = new MessageStructure(package.Message as byte[]);
            //var gold = reader.ReadInt();
            //var itemsCount = reader.ReadInt();
            //var items = new int[itemsCount];
            //for (int i = 0; i < itemsCount; i++)
            //{
            //    reader.RecordStart();
            //    items[i] = reader.ReadInt();
            //    reader.RecordEnd();
            //}
        }
    }
  
}