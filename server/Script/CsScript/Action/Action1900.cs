using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using GameServer.Script.Model.Enum.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 改名
    /// </summary>
    public class Action1900 : BaseAction
    {
        private bool receipt;
        private string newName;

        public Action1900(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1900, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetString("NewName", ref newName))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {

            int needDiamond = ConfigEnvSet.GetInt("System.ChangeNicknameNeedDiamond");
            if (GetBasis.DiamondNum < needDiamond)
            {
                return true;
            }
            var nickNameCheck = new NickNameCheck();
            var KeyWordCheck = new KeyWordCheck();
            string msg;

            if (nickNameCheck.VerifyRange(newName, out msg) ||
                KeyWordCheck.VerifyKeyword(newName, out msg) ||
                nickNameCheck.IsExistNickName(newName, out msg))
            {
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = msg;
                return false;
            }
            
            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            GetBasis.NickName = newName;


            // 这里刷新排行榜数据
            var combat = UserHelper.FindRankUser(Current.UserId, RankType.Combat);
            combat.NickName = newName;
            var level = UserHelper.FindRankUser(Current.UserId, RankType.Level);
            level.NickName = newName;
            //var fightvaluer = UserHelper.FindRankUser(Current.UserId, RankType.FightValue);
            //fightvaluer.NickName = newName;

            receipt = true;
            return true;
        }
    }
}