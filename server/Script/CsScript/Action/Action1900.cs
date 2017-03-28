using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
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
        private JPChangeNickNameData receipt;
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
            receipt = new JPChangeNickNameData();
            

            int needDiamond = ConfigEnvSet.GetInt("System.ChangeNicknameNeedDiamond");
            if (GetBasis.DiamondNum < needDiamond)
            {
                receipt.Result = EventStatus.Bad;
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

            receipt.Result = EventStatus.Good;
            UserHelper.ConsumeDiamond(Current.UserId, needDiamond);
            GetBasis.NickName = newName;
            receipt.CurrDiamond = GetBasis.DiamondNum;
            receipt.NewNickName = newName;


            // 排行
            var combatuser = UserHelper.FindCombatRankUser(GetBasis.UserID);
            if (combatuser != null)
            {
                combatuser.NickName = GetBasis.NickName;
            }
            //var leveluser = UserHelper.FindLevelRankUser(GetBasis.UserID);
            //if (leveluser != null)
            //{
            //    leveluser.NickName = GetBasis.NickName;
            //}

            return true;
        }
    }
}