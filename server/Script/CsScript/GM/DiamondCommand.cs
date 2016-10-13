
using GameServer.CsScript.Com;
using GameServer.Script.Model.DataModel;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;

namespace GameServer.CsScript.GM
{
    public class DiamondCommand : TryXGMCommand
    {
        public static readonly DiamondCommand instance = new DiamondCommand();

        public override void ProcessCmd()
        {
            int num = Args.Length > 2 ? Args[2].Trim().ToInt() : 0;
            if (num > 0)
            {
                AddUserDiamond(num, UserId);
            }
        }

        /// <summary>
        /// 添加玩家钻石
        /// </summary>
        /// <param name="num"></param>
        /// <param name="userId"></param>
        private void AddUserDiamond(int num, int userId)
        {
            UserHelper.GiveAwayDiamond(userId, num);
        }
    }
}