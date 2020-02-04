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

namespace Hotel.Admin.Controllers.Yd
{
    public class YdSkController : AdminBaseController
    {
        // GET: YdSk
        public ActionResult Index(long roomRegId, long itemId = 0)
        {
            if (itemId == 0)
            {
                return View(new RoomRegSk()
                {
                    HotelId = UserContext.CurrentUser.HotelId,
                    RoomRegId = roomRegId,
                    FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                });
            }
            else
            {
                var model = RoomRegSkBll.GetById(itemId);
                if (model == null)
                {
                    model = new RoomRegSk()
                    {
                        HotelId = UserContext.CurrentUser.HotelId,
                        RoomRegId = roomRegId,
                        FsTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss")
                    };
                }
                return View(model);
            }
        }

        public string GetList(long roomRegId)
        {
            var datas = RoomRegSkBll.GetList(roomRegId);
            return JsonConvert.SerializeObject(datas);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegSk) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult AddOrUpdate(RoomRegSk model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegSkBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
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