using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.DataModel;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 请求查看用户数据
    /// </summary>
    public class Action1600 : BaseAction
    {
        private JPQueryUserData receipt;
        private int _queryuserid;

        public Action1600(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1600, actionGetter)
        {

        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("QueryUserId", ref _queryuserid))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            GameUser user = UserHelper.FindUser(_queryuserid);
            UserRank rank = UserHelper.FindCombatRankUser(_queryuserid);
            if (user == null || rank == null)
            {
                ErrorInfo = Language.Instance.NoFoundUser;
                return true;
            }


            receipt = new JPQueryUserData()
            {
                UserId = user.UserID,
                NickName = user.NickName,
                LooksId = user.LooksId,
                FightValue = user.FightingValue,
                Attack = user.Attack,
                Defense = user.Defense,
                Hp = user.Hp,
                UserStage = user.UserStage,
                CombatRankId = rank.RankId,
                SkillList = user.SkillDataList,
                SkillCarryList = user.SkillCarryList,
                VipLv = user.VipLv
            };
            foreach (var item in DataHelper.CombatItemList)
            {
                ItemData data = new ItemData();
                data.ID = item.ID;
                data.Num = 0;
                receipt.CombatItemList.Add(data);
                var itemdata = user.findItem(item.ID);
                if (itemdata != null)
                {
                    data.Num = itemdata.Num;
                }
            }

            GameSession session = GameSession.Get(user.UserID);
            if (session != null && session.Connected)
                receipt.IsOnline = true;

            if (user.ClassData.ClassID != 0)
            {
                var classdata = new ShareCacheStruct<ClassDataCache>().FindKey(user.ClassData.ClassID);
                if (classdata != null)
                {
                    receipt.ClassName = classdata.Name;
                }
            }

            return true;
        }

        protected override string BuildJsonPack()
        {
            if (receipt != null)
            {
                body = receipt;
            }
            else
            {
                ErrorCode = ActionIDDefine.Cst_Action1600;
            }
            return base.BuildJsonPack();
        }


  
    }
}
