using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.CsScript.Remote;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Model;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Net;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 1009_客户端连接到聊天服的通知
    /// </summary>
    public class Action1009 : BaseAction
    {
        private Random random = new Random();
        public Action1009(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1009, actionGetter)
        {
            IsNotRespond = true;
        }

        public override bool GetUrlElement()
        {
            return true;
        }

        public override bool TakeAction()
        {
            ChatRemoteService.SendUserData(GetBasis, GetGuild.GuildID);

            string content = "欢迎进入勇者之怒！";
            ChatRemoteService.SendSystemChat(Current.UserId, content);
            return true;
        }


       
    }
}