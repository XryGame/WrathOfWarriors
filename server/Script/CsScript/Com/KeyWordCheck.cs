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
    public class KeyWordCheck
    {

        public KeyWordCheck()
        {
            InitChatKeyWord();
        }

        public static List<Config_ChatKeyWord> ChatKeyWordList
        {
            get;
            private set;
        }

        public static void InitChatKeyWord()
        {
            ChatKeyWordList = new ShareCacheStruct<Config_ChatKeyWord>().FindAll();
        }
        /// <summary>
        /// 验证昵称是否有关键词
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool VerifyKeyword(string nickName, out string msg)
        {
            msg = "";
            foreach (Config_ChatKeyWord chatKeyWord in ChatKeyWordList)
            {
                nickName = nickName.Replace(chatKeyWord.KeyWord, new string('*', chatKeyWord.KeyWord.Length));
            }
            if (nickName.Contains("*"))
            {
                msg = Language.Instance.St1005_NickNameExistKeyword;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 将消息的敏感字符替换为‘*’
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string FilterMessage(string message)
        {
            foreach (Config_ChatKeyWord chatKeyWord in ChatKeyWordList)
            {
                message = message.Replace(chatKeyWord.KeyWord, new string('*', chatKeyWord.KeyWord.Length));
            }
            return message;
        }
    }
}
