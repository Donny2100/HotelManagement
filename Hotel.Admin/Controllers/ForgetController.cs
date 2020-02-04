using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    /// <summary>
    /// 忘记密码
    /// </summary>
    public class ForgetController : Controller
    {
        // GET: Forget
        public ActionResult Index()
        {
            return View();
        }
    }
}