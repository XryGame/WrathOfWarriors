using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Sns._91sdk;
using ZyGames.Framework.RPC.IO;

namespace GameServer.CsScript.Base
{
    /// <summary>
    /// 排行
    /// </summary>
    public static class LevelRankingTop50Set
    {
        public static void LoadServerRanking()
        {

            try
            {
                var ranking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                LevelRanking levelranking = ranking as LevelRanking;
                levelranking.rankingData.RankTime = DateTime.Now;
                levelranking.rankingData.RankList.Clear();


                foreach (var v in ServerSet.Set)
                {
                    string url = v.ServerUrl;
                    string[] split = url.Split(':');
                    url = "http://" + split[0] + ":8091/GlobalCommon.aspx?";
                    url = url + HttpUtility.UrlEncode("ID=LevelRankingData");
                    
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Timeout = 5000;
                    request.Method = "GET";
                    WebResponse webresponse = request.GetResponse();
                    HttpWebResponse httpResponse = (HttpWebResponse)webresponse;
                    Stream stream = httpResponse.GetResponseStream();   //获取响应的字符串流  
                    StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
                    string data = sr.ReadToEnd();   //从头读到尾，放到字符串html  
                    
                    if (data != null && data.Length > 0)
                    {
                        byte[] bytes = Convert.FromBase64String(data);
                        MessageStructure ms = new MessageStructure(bytes);

                        int listsize = ms.ReadInt();
                        for (int i = 0; i < listsize; ++i)
                        {
                            UserRank rank = new UserRank();
                            rank.UserID = ms.ReadInt();
                            rank.NickName = ms.ReadString();
                            rank.Profession = ms.ReadInt();
                            rank.RankId = ms.ReadInt();
                            rank.UserLv = ms.ReadInt();
                            rank.FightValue = ms.ReadInt();
                            rank.VipLv = ms.ReadInt();
                            levelranking.rankingData.RankList.Add(rank);
                        }
                    }
                }


                Ranking<UserRank> levelRanking = RankingFactory.Get<UserRank>(LevelRanking.RankingKey);
                levelRanking.ForceRefresh();
            }
            catch (Exception ex)
            {
                new BaseLog().SaveLog(ex);
                return;
            }

        }
    }
}