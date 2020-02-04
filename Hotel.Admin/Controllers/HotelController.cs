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
    public class HotelController : AdminBaseController
    {
        // GET: Hotel
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new HotelModel());
            var info = HotelBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows, string searchName = "")
        {
            var pager = HotelBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        public JsonResult GetAreaList(string pid, int type)
        {
            //1：获取省  2：获取市  3：获取县
            var areaList = AreaBll.GetList(pid, type);
            return Json(areaList);
        }


        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(HotelModel) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(HotelModel model)
        {
            var apiResult = new APIResult();
            try
            {
                HotelBll.AddOrUpdate(model,(UserEntity)UserContext.CurrentUser);
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
                HotelBll.Delete(id);
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