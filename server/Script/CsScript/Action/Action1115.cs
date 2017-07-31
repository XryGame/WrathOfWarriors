using GameServer.CsScript.Base;
using GameServer.CsScript.Com;
using GameServer.CsScript.JsonProtocol;
using GameServer.Script.CsScript.Action;
using GameServer.Script.CsScript.Com;
using GameServer.Script.Model.Config;
using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using GameServer.Script.Model.Enum;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;

namespace GameServer.CsScript.Action
{


    /// <summary>
    /// 请求通关关卡
    /// </summary>
    public class Action1115 : BaseAction
    {
        private bool receipt;
        private int _ID;
        public Action1115(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1115, actionGetter)
        {

        }

        protected override string BuildJsonPack()
        {
            body = receipt;
            return base.BuildJsonPack();
        }

        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("ID", ref _ID))
            {
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            if (_ID > GetBasis.UserLv)
            {
                PushMessageHelper.UserGameDataExceptionNotification(Current);
                return false;
            }
            int needVit = _ID % 5 == 0 ? 3 : 1;
            if (GetBasis.Vit >= needVit)
            {
                receipt = true;
                if (DataHelper.VitMax == GetBasis.Vit)
                {
                    GetBasis.StartRestoreVitDate = DateTime.Now;
                }
                    
                GetBasis.Vit = MathUtils.Subtraction(GetBasis.Vit, needVit, 0);

                GetBasis.CurrentPassLevelID = _ID;
            }
            else
            {
                receipt = false;
            }
            PushMessageHelper.UserVitChangedNotification(Current);
            return true;
        }
    }
}