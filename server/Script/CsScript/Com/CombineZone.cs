using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Com.Rank;
using GameServer.Script.Model;
using ZyGames.Framework.Net;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.CsScript.Com;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Script;
using ZyGames.Framework.Game.Config;
using System.Web;
using System.Text;
using ZyGames.Framework.Game.Sns._91sdk;
using GameServer.Script.Model.Enum;
using System.Configuration;
using System.Threading;
using GameServer.Script.Model.Config;
using ZyGames.Framework.Game.Runtime;

namespace GameServer.CsScript.Com
{
    /// <summary>
    /// 合区
    /// </summary>
    public static class CombineZone
    {
        private static bool isRun = false;
        private static char PassprotHeadChar = 'a';
        private static int ProductServerId = 1;

        public static void Run()
        {
            if (isRun)
                return;
            isRun = true;

            MakeClassData();
            MakeGameUser();
            MakeJobTitleData();
            MakeOrderInfo();
            MakeOccupyData();
            MakeUserCenterPassport();
            MakeUserCenterUser();
            MakeUserPay();
        }

        public static void MakeClassData()
        {

            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = "DELETE FROM ClassDataCache WHERE ClassID%1000 >= 3";
            dbProvider.ExecuteReader(CommandType.Text, sql);
            
            sql = string.Format("UPDATE ClassDataCache SET MemberList='[]', Monitor=0");
            dbProvider.ExecuteReader(CommandType.Text, sql);
        }
        


        public static void MakeGameUser()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format(
                "UPDATE UserBasisCache SET UserId=UserID%1000000 + {0}*1000000, Pid=CONCAT('{1}', Pid), ClassData='{2}', CombatLogList='[]', FriendsData='{3}'",
                ProductServerId,
                PassprotHeadChar,
                "{\"ClassID\":0}",
                "{\"FriendsList\":[],\"ApplyList\":[],\"GiveAwayCount\":0}"
                );

            dbProvider.ExecuteReader(CommandType.Text, sql);
        }

        public static void MakeJobTitleData()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format(
                "UPDATE JobTitleDataCache SET UserId=0, NickName='{0}', ClassId=0, CampaignUserList='[]', Profession=0",
                string.Empty
                );

            dbProvider.ExecuteReader(CommandType.Text, sql);

        }

        public static void MakeOrderInfo()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format("UPDATE OrderInfoCache SET UserId=UserId%1000000 + {0}*1000000", ProductServerId);
            dbProvider.ExecuteReader(CommandType.Text, sql);

        }


        public static void MakeOccupyData()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format("UPDATE OccupyDataCache SET UserId=0, NickName=\"\"");
            dbProvider.ExecuteReader(CommandType.Text, sql);
        }

        public static void MakeUserCenterPassport()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format("UPDATE UserCenterPassport SET PassportID=CONCAT('{0}', PassportID)", PassprotHeadChar);
            dbProvider.ExecuteReader(CommandType.Text, sql);
        }

        public static void MakeUserCenterUser()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format(
                "UPDATE UserCenterUser SET UserId=UserId%1000000 + {0}*1000000, PassportID=CONCAT('{1}', PassportID)", 
                ProductServerId,
                PassprotHeadChar
                );

            dbProvider.ExecuteReader(CommandType.Text, sql);
        }

        public static void MakeUserPay()
        {
            var dbProvider = DbConnectionProvider.CreateDbProvider(DbConfig.Data);
            string sql = string.Format(
                "UPDATE UserPayCache SET UserID=UserID%1000000 + {0}*1000000", ProductServerId);

            dbProvider.ExecuteReader(CommandType.Text, sql);
        }
    }
}