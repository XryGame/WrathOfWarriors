using GameServer.Script.Model.ConfigModel;
using GameServer.Script.Model.DataModel;
using System.Collections.Generic;
using System.Text;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Lang;
using ZyGames.Framework.Game.Message;

namespace GameServer.CsScript.Com
{
    /// <summary>
    /// 昵称检测模块
    /// </summary>
    public class NickNameCheck
    {

        
        /// <summary>
        /// 验证有效范围
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool VerifyRange(string nickName, out string msg)
        {
            msg = "";
            nickName = nickName ?? string.Empty;
            nickName = nickName.Trim();
            int minLength = ConfigEnvSet.GetInt("User.MinLength");
            int maxLength = ConfigEnvSet.GetInt("User.MaxLength");
            int length = Encoding.Default.GetByteCount(nickName);
            if (length < minLength || length > maxLength)
            {
                msg = string.Format(Language.Instance.St1005_NickNameOutRange, minLength, maxLength);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否存在角色名
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool IsExistNickName(string nickName, out string msg)
        {
            msg = "";
            var list = new ShareCacheStruct<UserCenterUser>().FindAll(m => m.NickName == nickName, false);
            if (list.Count > 0)
            {
                msg = Language.Instance.St1005_NickNameExist;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 创始成功之后处理事件
        /// </summary>
        public void OnCreateAfter(UserBasisCache user)
        {
            var cacheSet = new ShareCacheStruct<UserCenterUser>();
            UserCenterUser ucu = cacheSet.FindKey(user.UserID);
            if (ucu != null)
            {
                ucu.NickName = user.NickName;
            }
        }
    }
}
