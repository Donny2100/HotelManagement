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
    public class BuildFloorController : AdminBaseController
    {
        // GET: BuildFloor
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0, int buildFloorType = 1)
        {
            if (id == 0)
                return View(new BuildFloor() { BuildFloorType = buildFloorType, HotelId = UserContext.CurrentUser.HotelId });
            var info = BuildFloorBll.GetById(id);
            return View(info);
        }

        public string GetList()
        {
            var pager = BuildFloorBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(pager);
        }

        public string GetFloorList(long buildId)
        {
            var list = BuildFloorBll.GetFloorList(buildId);
            return JsonConvert.SerializeObject(list);
        }
        public string GetAllFloor(string tip)
        {
            var list = BuildFloorBll.GetAllFloor(UserContext.CurrentUser.HotelId);
            if (!string.IsNullOrEmpty(tip))
            {
                list.Insert(0, new BuildFloor() { Id = 0, Name = tip });
            } 
            return JsonConvert.SerializeObject(list);
        }
        
        public string GetFloorListBy(long buildId, string tip)
        {
            var list = BuildFloorBll.GetFloorList(buildId);
            if (list == null)
                list = new List<BuildFloor>();
            list.Insert(0, new BuildFloor() { Id = 0, Name = tip });
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(BuildFloor) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(BuildFloor model)
        {
            var apiResult = new APIResult();
            try
            {
                BuildFloorBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
                BuildFloorBll.DeleteById(id);
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