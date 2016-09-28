using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;

namespace GameServer.CsScript.JsonProtocol
{
    public class JPRequestOccupyData
    {
        public JPRequestOccupyData()
        {
            SceneData = new JPOccupySceneData();
            RivalData = new JPRivalUserData();
        }

        public RequestOccupyResult Result { get; set; }

        public CacheList<SceneType> OccupySceneList { get; set; }

        public JPOccupySceneData SceneData { get; set; }

        public JPRivalUserData RivalData { get; set; }

    }
}
