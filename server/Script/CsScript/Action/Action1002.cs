/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using GameServer.CsScript.Base;
using GameServer.CsScript.JsonProtocol;
using System;
using System.Security.Cryptography;
using System.Text;
using ZyGames.Framework.Common;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Game.Sns;
using ZyGames.Framework.RPC.Sockets;

namespace GameServer.CsScript.Action
{
    /// <summary>
    /// 通行证
    /// </summary>
    public class Action1002 : BaseStruct
    {
        private string passport = string.Empty;
        private string password = string.Empty;
        private string deviceID = string.Empty;
        //private int mobileType = 0;
        //private int gameType = 0;
        //private string retailID = string.Empty;
        //private string clientAppVersion = string.Empty;
        //private int ScreenX = 0;
        //private int ScreenY = 0;

        public Action1002(ActionGetter actionGetter)
            : base(ActionIDDefine.Cst_Action1002, actionGetter)
        {
            IsWebSocket = true;
            actionGetter.OpCode = OpCode.Text;
        }

        //public override void BuildPacket()
        //{
        //    PushIntoStack(passport);
        //    PushIntoStack(password);
        //}

        protected override string BuildJsonPack()
        {
            ResultData resultData = new ResultData()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = ErrorCode,
                ErrorInfo = ErrorInfo,
                Data = new JPPassData()
                {
                    PassportId = passport,
                    Password = password
                }
            };
            return MathUtils.ToJson(resultData);
        }

        public override bool GetUrlElement()
        {
            if (/*httpGet.GetInt("MobileType", ref mobileType) &&
                httpGet.GetInt("GameType", ref gameType) &&
                httpGet.GetString("RetailID", ref retailID) &&
                httpGet.GetString("ClientAppVersion", ref clientAppVersion) &&*/
                httpGet.GetString("DeviceID", ref deviceID))
            {
                //httpGet.GetInt("ScreenX", ref ScreenX);
                //httpGet.GetInt("ScreenY", ref ScreenY);
            }
            else
            {
                return false;
            }
            return true;
        }

        public override bool TakeAction()
        {
            try
            {
                string[] userList = SnsManager.GetRegPassport(deviceID);
                passport = userList[0];
                password = userList[1];


                return true;
            }
            catch (Exception ex)
            {
                SaveLog(ex);
                ErrorCode = Language.Instance.ErrorCode;
                ErrorInfo = Language.Instance.St1002_GetRegisterPassportIDError;
                return false;
            }
        }
    }
}