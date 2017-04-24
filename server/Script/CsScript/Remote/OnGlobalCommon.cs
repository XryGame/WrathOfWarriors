using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Remote
{


    public class OnGlobalCommon : HttpMessageInterface
    {

        string _OperateName;
        public void ActiveHttp(NewHttpResponse client, Dictionary<string, string> parms)
        {
            MessageStructure receipt = verifyDeliver(parms);
            //string buffer = Encoding.UTF8.GetString();
            string buffer = Convert.ToBase64String(receipt.PopBuffer());
            client.OutputStream.WriteLine(buffer);
            client.Close();
        }

        private MessageStructure verifyDeliver(Dictionary<string, string> parms)
        {
            MessageStructure ms = new MessageStructure();
            try
            {
                while (true)
                {
                    
                    parms.TryGetValue("ID", out _OperateName);

                    if (_OperateName == "LevelRankingData")
                    {
                        
                        int pagecout;
                        var ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                        var list = ranking.GetRange(0, 50, out pagecout);

                        ms.WriteByte(list.Count);
                        foreach (var data in list)
                        {
                            ms.WriteByte(data.UserID);
                            ms.WriteByte(data.NickName);
                            ms.WriteByte(data.Profession);
                            ms.WriteByte(data.RankId);
                            ms.WriteByte(data.UserLv);
                            ms.WriteByte(data.FightValue);
                            ms.WriteByte(data.VipLv);
                        }
                    }
                    break;
                }

            }
            catch (Exception e)
            {
                TraceLog.WriteError(string.Format("{0} {1}", "Url参数格式错误", e));
            }
            return ms;
        }
    }

  
}