
using GameServer.CsScript.Com;
using GameServer.Script.Model.DataModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;

namespace GameServer.CsScript.GM
{
    public class VitCommand : TryXGMCommand
    {
        public static readonly VitCommand instance = new VitCommand();

        public override void ProcessCmd()
        {
            int num = Args.Length > 2 ? Args[2].Trim().ToInt() : 0;
            if (num > 0)
            {
                AddUserVit(num, UserId);
            }
        }

        /// <summary>
        /// 添加玩家体力
        /// </summary>
        /// <param name="num"></param>
        /// <param name="userId"></param>
        private void AddUserVit(int num, int userId)
        {
            GameUser user = UserHelper.FindUser(userId);
            if (user != null)
                user.Vit = MathUtils.Addition(user.Vit, num, int.MaxValue);
        }
    }
}