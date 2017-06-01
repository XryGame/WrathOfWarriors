using GameServer.Script.Model;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Remote
{

    public class UpdateUserDataService : RemoteStruct
    {
        private int _userId;
        private string _userName;
        private int _vipLv;
        private int _profession;
        private string _avatarUrl;
        private int _serverID;
        private string _guildID;

        public UpdateUserDataService(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {
            IsNotRespond = true;
        }


        protected override bool Check()
        {
            if (paramGetter.GetInt("UserId", ref _userId)
                && paramGetter.GetString("UserName", ref _userName)
                && paramGetter.GetInt("VipLv", ref _vipLv)
                && paramGetter.GetInt("Profession", ref _profession)
                && paramGetter.GetString("AvatarUrl", ref _avatarUrl)
                && paramGetter.GetInt("ServerID", ref _serverID)
                && paramGetter.GetString("GuildID", ref _guildID))
            {

                return true;
            }
            return false;
        }

        protected override void TakeRemote()
        {
            var session = paramGetter.GetSession();
            if (session == null)
            {
                ErrorCode = 10000;
                ErrorInfo = "Sessin is null.";
                return;
            }

            var cache = new MemoryCacheStruct<ChatUser>();
            ChatUser chatUser = cache.Find(t => t.UserId == _userId);
            if (chatUser != null)
            {
                chatUser.UserName = _userName;
                chatUser.VipLv = _vipLv;
                chatUser.Profession = _profession;
                chatUser.AvatarUrl = _avatarUrl;
                chatUser.ServerID = _serverID;
                chatUser.GuildID = _guildID;
            }
        }

        protected override void BuildPacket()
        {
        }
    }
}