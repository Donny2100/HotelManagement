using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class GuestInfoCnController : AdminBaseController
    {
        // GET: GuestInfoCn
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 客人搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult _GuestSearch()
        {
            return View();
        }

        /// <summary>
        /// 根据客人名称搜索
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public string GetGuestList(string searchName)
        {
            var list = GuestInfoCNBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(list);
        }
    }
}