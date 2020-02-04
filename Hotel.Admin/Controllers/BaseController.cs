using Hotel.Model;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected void SetMenuCookie(long navId)
        {
            Cookies.WriteCookie(Cookies._CookieAdminName, "navId", navId.ToString(), 60 * 24);
        }

        protected long GetMenuCookieBlock()
        {
            long blockId = 0;
            try
            {
                blockId = long.Parse(Cookies.GetCookie(Cookies._CookieAdminName, "blockId"));
            }
            catch (Exception ex) { }
            return blockId;
        }
    }
}