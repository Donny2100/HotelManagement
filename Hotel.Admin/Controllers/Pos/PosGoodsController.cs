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
    public class PosGoodsController : AdminBaseController
    {
        public ActionResult Menu(long posId)
        {
            ViewBag.PosId = posId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }
        public ActionResult Index(long posId)
        {
            ViewBag.PosId = posId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }
        // GET: PosGoods
        public ActionResult Search(long posId,decimal discountValue = 1)
        {
            var pos = PosDefineBll.GetById(posId);
            ViewBag.Pos = pos;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = posId;
            ViewBag.DiscountValue = discountValue;
            var types = PosCatBll.GetListByPos(posId);
            ViewBag.Types = types;

            return View();
        }

        public ActionResult _SearchContent(long posId,string searchKey, decimal discountValue = 1)
        {
            var pos = PosDefineBll.GetById(posId);
            ViewBag.Pos = pos;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = posId;
            ViewBag.DiscountValue = discountValue;
            var types = PosCatBll.GetListByPos(posId);
            ViewBag.Types = types;
            ViewBag.SearchKey = searchKey;
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet] 
        public ActionResult Edit(long posId, long id = 0)
        {
            ViewBag.PosId = posId;
            if (id == 0)
            {
                return View(new PosGoods()
                {
                    PosId = posId,
                    IsCanDiscount = true,
                    IsEnabled = true 
                });
            } 
            var info = PosGoodsBll.GetById(id);
            if (info.PosId == 0) info.PosId = posId;
            return View(info);
        }
        public string GetAll(long catId = 0, string searchName = null)
        {
            var datas = PosGoodsBll.GetListByCat(catId);

            var data  = new Pager<PosGoods>() { total = datas.Count, rows = datas };

            return JsonConvert.SerializeObject(data);
        }


        public string GetPager(int page  , int rows, long posId,  long catId =0, string searchName = null)
        {
            if(catId == 0)
            {
                var pager2 = PosGoodsBll.GetPagerByPos(posId, page, rows, UserContext.CurrentUser.HotelId, 0, searchName);
                return JsonConvert.SerializeObject(pager2);
            }
            var pager = PosGoodsBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, catId, searchName);
            return JsonConvert.SerializeObject(pager);
        }
        public string GetPagerByPos(long posId,int page, int rows, long catId = 0, string searchName = null)
        {
            var pager = PosGoodsBll.GetPagerByPos(posId , page, rows, UserContext.CurrentUser.HotelId, catId, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Hotel.Model.PosGoods) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Hotel.Model.PosGoods model)
        {
            var apiResult = new APIResult();
            try
            {
                PosGoodsBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }

            return Json(apiResult);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public ActionResult Delete(long id)
        {
            var apiResult = new APIResult();
            try
            {
                PosGoodsBll.Delete(id);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            return Json(apiResult);
        }
    }
}