using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class NonAccountsController : AdminBaseController
    {
        // GET: Hotel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new NonAccountsModel());
            //var info = NonAccountsBll.GetById();
            return View();
        }
        public ActionResult del(long id = 0)
        {
           
            return View();
        }
        public string GetPager(int page, int rows, string searchName = "")
        {
            var pager = NonAccountsBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, searchName);
            
            return JsonConvert.SerializeObject(pager);
        }
        public JsonResult GetGoods(int page, int rows, long catId = 0, string searchName = "")
        {
            var pager = GoodsBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, catId, searchName);
            //var pager = GoodsBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, catId, searchName);
            return Json(pager);
        }
        
    
        public bool AddGoods()
        {

            int cnt = 0;
            var c = 0;           
            var sgdh = string.Empty;
            var goodList = NonAccountsBll.GetList();         
            foreach (var goods in goodList.Items)
            {
                c = goods.CDate;
                sgdh = goods.Sgdh;
            }
            string now  = DateTime.Now.ToString("yyyyMMdd");
            string res = string.Empty;
            string b = TypeConvert.IntToDateTime(c).ToString("yyyyMMdd");
            string d = now;

            if (TypeConvert.IntToDateTime(c).ToString("yyyyMMdd").Equals(now))
            {
                cnt = int.Parse(sgdh.Substring(sgdh.Length - 4)) + 1;
                res = "f" + DateTime.Now.ToString("yyyyMMddHH") + cnt.ToString("0000");
            }
            else
            {
                res = "f" + DateTime.Now.ToString("yyyyMMddHH") + cnt.ToString("0000");
            }
                
            string Number1 = Request.Form["RowNumber"];
            string[] Number = Number1.Split(',');
          
            string GoodsName1 = Request.Form["name"];
            string[] GoodsName = GoodsName1.Split(',');

            string GoodsID1 = Request.Form["number"];
            string[] GoodsID = GoodsID1.Split(',');

            string price1 = Request.Form["price"];
            string[] price = price1.Split(',');

            var CDate1 = TypeConvert.DateTimeToInt(DateTime.Now);

            var sgdh1 = res; //"f" + DateTime.Now.ToString("yyyyMMddHH");

            for (int i = 0; i < Number.Length; i++)
            {
                var CDate = CDate1;
                var Sgdh = sgdh1;//单号
                var Number2 = int.Parse(Number[i]);
                var Money = int.Parse(price[i]) * int.Parse(Number[i]);
                //PayTyepeID = int.Parse(Request.Form["DDlZffs"]);
                var HandlerID = UserContext.CurrentUser.Id;
                var HandlerName = UserContext.CurrentUser.Name;
                var Remark = Request.Form["remark"];
                var PayTyepeName = Request.Form["DDlZffs"];
                var GoodsName2 = GoodsName[i];
                var GoodsID2 = GoodsID[i];


                var hotel = new NonAccountsModel()
                {
                    CDate = CDate1,
                    Sgdh = Sgdh,//"f" + DateTime.Now.ToString("yyyyMMddHHmmss"),//单号
                    Number = int.Parse(Number[i]),
                    Money = int.Parse(price[i]) * int.Parse(Number[i]),
                    //PayTyepeID = int.Parse(Request.Form["DDlZffs"]),
                    HandlerID = UserContext.CurrentUser.Id,
                    HandlerName = UserContext.CurrentUser.Name,
                    Remark = Request.Form["remark"],
                    PayTyepeName = Request.Form["DDlZffs"],
                    GoodsName = GoodsName[i],
                    GoodsID = GoodsID[i]
                };
                bool pager = NonAccountsBll.RegHotel(hotel);     
            }
            return true;
        }
        public string GetList(bool isJzfk, bool state)
        {
            var models = PayTypeBll.GetList(UserContext.CurrentUser.HotelId, isJzfk, state);
            return JsonConvert.SerializeObject(models);
        }

        public bool Delete(long id)
        {
            var models = NonAccountsBll.Del(id);
            return true;

        }

       
        public string Find(long goodsId = 0,long hotelId = 0)
        {
            var a = Request.Params["a"];
            goodsId = int.Parse(Request.Params["a"]);
            var models = NonAccountsBll.Find(goodsId, hotelId);
            return JsonConvert.SerializeObject(models);
        }

        public JsonResult search(string beginTime, string endTime,string segd)
        {
            var pager = NonAccountsBll.search(beginTime, endTime, segd);
            return Json(pager);

            //jack
        }


    }
}