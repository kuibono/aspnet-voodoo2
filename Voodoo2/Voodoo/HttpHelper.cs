using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Voodoo
{
    public static class HttpHelper
    {
        #region CookieCollection 转换为CookieContainer
        /// <summary>
        ///CookieCollection ×ª»»ÎªCookieContainer
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static CookieContainer ToCookieContainer(this CookieCollection collection)
        {
            CookieContainer cc = new CookieContainer();
            foreach (Cookie cookie in collection)
            {
                cc.Add(cookie);
            }
            return cc;

        }
        #endregion

        /// <summary>
        /// 判断指定CookieCollection是否存在指定的Cookie
        /// </summary>
        /// <param name="cookieCollection"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static bool Exist(this CookieCollection cookieCollection, Cookie cookie)
        {

            foreach (Cookie c in cookieCollection)
            {
                if (c == cookie)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
