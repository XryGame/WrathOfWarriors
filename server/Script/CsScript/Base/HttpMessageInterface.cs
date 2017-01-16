
using System;
using System.Collections.Generic;
using GameServer.CsScript.Base;

namespace GameServer.CsScript.Base
{

    public interface HttpMessageInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        void ActiveHttp(NewHttpResponse client, Dictionary<string, string> parms);
    }

}