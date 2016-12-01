using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.Enum;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{

    /// <summary>
    /// 10600_改名
    /// </summary>
    public class Action10600 : BaseAction
    {
        private JPChangeNickNameData receipt;
        private string newName;

        public Action10600(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action10600, actionGetter)
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
            receipt = new JPChangeNickNameData();
            

            int needDiamond = ConfigEnvSet.GetInt("System.ChangeNicknameNeedDiamond");
            if (ContextUser.DiamondNum < needDiamond)
            {
                receipt.Result = EventStatus.Bad;
                return true;
            }
            var roleFunc = new RoleFunc();
            string msg;

            if (roleFunc.VerifyRange(newName, out msg) ||
                roleFunc.VerifyKeyword(newName, out msg) ||
                roleFunc.IsExistNickName(newName, out msg))
            {
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = msg;
                return false;
            }

            receipt.Result = EventStatus.Good;
            ContextUser.UsedDiamond = MathUtils.Addition(ContextUser.UsedDiamond, needDiamond);
            ContextUser.NickName = newName;
            receipt.CurrDiamond = ContextUser.DiamondNum;
            receipt.NewNickName = newName;
            return true;
        }
    }
}