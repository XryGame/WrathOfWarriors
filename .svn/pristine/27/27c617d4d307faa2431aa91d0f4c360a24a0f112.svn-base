using ZyGames.Framework.Game.Command;

namespace GameServer.CsScript.Com
{
    public abstract class TryXGMCommand
    {
        protected int UserId = 0;
        protected string[] Args;
        public const string CmdError = "[{0}]命令无效";

       
        public abstract void ProcessCmd();

        public void Parse(int uid, string command)
        {
            UserId = uid;
            if (Args != null)
            {
                Args.Initialize();
            }

            command = command != null ? command.Trim() : string.Empty;
            string[] paramList = command.Split(new char[] { ' ' });
            if (paramList.Length < 2)
            {
                return;
            }

            Args = paramList;
        }

    }
}
