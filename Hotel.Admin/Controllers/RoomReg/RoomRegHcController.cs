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
    public class RoomRegHcController : AdminBaseController
    {
        // GET: RoomRegHc
        public ActionResult Index(long roomRegId)
        {
            //获取房费
            List<RoomRegHcDetail> fyList = new List<RoomRegHcDetail>();
            var list = RoomRegFfRecordBll.GetList(roomRegId, 0);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    fyList.Add(new RoomRegHcDetail
                    {
                        Id = 0,
                        RoomRegId = item.RoomRegId,
                        RType = 1,
                        TargetId = item.Id,
                        FeeName = $"房费",
                        HotelId = item.HotelId,
                        FeeMoney = item.Money,
                        CzMoney = 0
                    });
                }
            }
            ViewBag.FfList = fyList;
            ViewBag.RoomRegId = roomRegId;
            return View();
        }

        /// <summary>
        /// 获取商品费用
        /// </summary>
        /// <returns></returns>
        public string GetSpfyList(long roomRegId)
        {
            List<RoomRegHcDetail> fyList = new List<RoomRegHcDetail>();
            var list = RoomRegGoodsDetailsBll.GetList(roomRegId, 0);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    fyList.Add(new RoomRegHcDetail
                    {
                        Id = 0,
                        RoomRegId = item.RoomRegId,
                        RType = 2,
                        TargetId = item.Id,
                        FeeName = item.GoodsName,
                        HotelId = item.HotelId,
                        FeeMoney = item.Money,
                        CzMoney = 0
                    });
                }
            }
            return JsonConvert.SerializeObject(fyList);
        }

        /// <summary>
        /// 获取赔偿费用
        /// </summary>
        /// <returns></returns>
        public string GetSwpcList(long roomRegId)
        {
            List<RoomRegHcDetail> fyList = new List<RoomRegHcDetail>();
            var list = RoomRegSwpcDetailsBll.GetList(roomRegId, 0);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    fyList.Add(new RoomRegHcDetail
                    {
                        Id = 0,
                        RoomRegId = item.RoomRegId,
                        RType = 3,
                        TargetId = item.Id,
                        FeeName = item.SwpcName,
                        HotelId = item.HotelId,
                        FeeMoney = item.Money,
                        CzMoney = 0
                    });
                }
            }
            return JsonConvert.SerializeObject(fyList);
        }

        /// <summary>
        /// 获取其他费用
        /// </summary>
        /// <returns></returns>
        public string GetQtfyList(long roomRegId)
        {
            List<RoomRegHcDetail> fyList = new List<RoomRegHcDetail>();
            var list = RoomRegQtfyBll.GetList(roomRegId, 0);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    fyList.Add(new RoomRegHcDetail
                    {
                        Id = 0,
                        RoomRegId = item.RoomRegId,
                        RType = 4,
                        TargetId = item.Id,
                        FeeName = item.Name,
                        HotelId = item.HotelId,
                        FeeMoney = item.Money,
                        CzMoney = 0
                    });
                }
            }
            return JsonConvert.SerializeObject(fyList);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegHc) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Save(RoomRegHc model, List<RoomRegHcDetail> details)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegHcBll.AddOrUpdate(model, details, user.HotelId, user.Id, user.Name);
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

        [HttpPost]
        public JsonResult Del(long id)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegHcDetailBll.DeleteById(id);
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