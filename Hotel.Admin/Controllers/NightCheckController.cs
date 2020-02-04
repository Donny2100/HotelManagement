using Hotel.Bll;
using Newtonsoft.Json;
using NIU.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class NightCheckController : AdminBaseController
    {
        // GET: NightCheck
        public ActionResult Index()
        {
            return View();
        }
    }
}