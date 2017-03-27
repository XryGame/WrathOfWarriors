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

    public class QueryUserData
    {
        public QueryUserData()
        {

        }
        public int UserId { get; set; }

        public string NickName { get; set; }

        public int Profession { get; set; }

        public int UserLv { get; set; }
        
        public int VipLv { get; set; }

        public int CombatRankId { get; set; }

        public UserAttributeCache Attribute { get; set; }

        public UserEquipsCache Equips { get; set; }

        public UserSoulCache Soul { get; set; }

    }

    /// <summary>
    /// 请求查看用户数据
    /// </summary>
    public class Action1600 : BaseAction
    {
        private QueryUserData receipt;
        private int _queryuserid;

        public Action1600(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1600, actionGetter)
        {

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
            UserBasisCache basis = UserHelper.FindUserBasis(_queryuserid);
            

            receipt = new QueryUserData()
            {
                UserId = basis.UserID,
                NickName = basis.NickName,
                Profession = basis.Profession,
                CombatRankId = basis.CombatRankID,
                VipLv = basis.VipLv,
                UserLv = basis.UserLv
            };
            receipt.Attribute = UserHelper.FindUserAttribute(_queryuserid);
            receipt.Equips = UserHelper.FindUserEquips(_queryuserid);
            receipt.Soul = UserHelper.FindUserSoul(_queryuserid);

            return true;
        }
        
  
    }
}
