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

            // 占领改名
            var occupylist = new ShareCacheStruct<OccupyDataCache>().FindAll();
            foreach (var v in occupylist)
            {
                if (v.UserId == ContextUser.UserID)
                {
                    v.NickName = ContextUser.NickName;
                    break;
                }
            }
            // 排行
            var combatuser = UserHelper.FindCombatRankUser(ContextUser.UserID);
            if (combatuser != null)
            {
                combatuser.NickName = ContextUser.NickName;
            }
            var leveluser = UserHelper.FindLevelRankUser(ContextUser.UserID);
            if (leveluser != null)
            {
                leveluser.NickName = ContextUser.NickName;
            }

            // 竞选
            var jobcache = new ShareCacheStruct<JobTitleDataCache>().FindAll();
            foreach (var v in jobcache)
            {
                if (v.UserId == ContextUser.UserID)
                {
                    v.NickName = ContextUser.NickName;
                }
                foreach(var v2 in v.CampaignUserList)
                {
                    if (v2.UserId == ContextUser.UserID)
                    {
                        v2.NickName = ContextUser.NickName;
                    }
                }
            }
            return true;
        }
    }
}