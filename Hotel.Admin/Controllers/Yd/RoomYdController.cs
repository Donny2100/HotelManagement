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
    public class RoomYdController : AdminBaseController
    {
        // GET: RoomYd
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetPager(int page, int rows, string roomNO = "", int status = -1, string name = "", string spell = "",
            string tel = "", long yxryId = 0, long createHandlerId = 0, string cdate = "", string sydsj = "", string eydsj = "",
            string sylsj = "", string eylsj = "", string yudNum = "", string memCompName = "",string memberCardNO = "",string remark = "")
        {
            var pager = RoomYdBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, roomNO, status, name, spell,
                tel, yxryId, createHandlerId, cdate, sydsj, eydsj, sylsj, eylsj, yudNum, memCompName, memberCardNO, remark);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            //获取房型
            var roomTypeList = RoomTypeBll.GetList(hotelId);
            if (roomTypeList == null)
                roomTypeList = new List<RoomType>();
            //获取所有房价方案
            var roomPriceList = RoomPriceBll.GetList(hotelId);
            if (roomPriceList == null)
                roomPriceList = new List<RoomPrice>();
            //循环房型获取房型对应的房价方案
            foreach (var roomtype in roomTypeList)
            {
                var roomPriceArr = roomPriceList.Where(m => m.RoomTypeId == roomtype.Id || m.RoomTypeId == 0);
                roomtype.RoomPriceList = roomPriceArr.ToList();
            }
            if (id == 0)
            {
                ViewBag.RoomPriceList = roomPriceList;
                ViewBag.RoomTypeList = roomTypeList;
                return View(new RoomYd() { HotelId = hotelId });
            }
            else
            {
                //获取所有预订的房间
                var roomYdRoomList = RoomYdRoomBll.GetLIst(id);
                if (roomYdRoomList != null && roomYdRoomList.Count > 0)
                {
                    //循环获取房间
                    foreach (var roomtype in roomTypeList)
                    {
                        var roomList = roomYdRoomList.Where(m => m.RoomTypeId == roomtype.Id);
                        if (roomList != null && roomList.Count() > 0)
                        {
                            roomtype.YdRoomList = new List<RoomYdRoom>();
                            foreach (var room in roomList)
                            {
                                if (room.RoomId != 0)
                                    roomtype.YdRoomList.Add(room);
                            }
                            roomtype.RoomCount = roomList.ElementAt(0).RoomCount;
                            roomtype.RoomPriceId = roomList.ElementAt(0).RoomPriceIdTip;
                            roomtype.RoomPrice = roomList.ElementAt(0).PriceTip;
                            roomtype.RoomSale = roomList.ElementAt(0).SaleTip;
                            roomtype.RoomSalePrice = roomList.ElementAt(0).SalePriceTip;
                        }
                        else
                        {
                            roomtype.YdRoomList = new List<RoomYdRoom>();
                            roomtype.RoomCount = 0;
                            roomtype.RoomPrice = roomtype.Price;
                            roomtype.RoomSale = decimal.Parse("1.00");
                            roomtype.RoomSalePrice = roomtype.Price;
                        }
                    }
                }

                var info = RoomYdBll.GetById(id);
                ViewBag.RoomPriceList = roomPriceList;
                ViewBag.RoomTypeList = roomTypeList;
                return View(info);
            }
        }

        public ActionResult _SelRoom(long roomTypeId)
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.RoomTypeId = roomTypeId;
            return View();
        }

        /// <summary>
        /// 选择房号界面获取房号列表
        /// </summary>
        /// <param name="roomTypeId"></param>
        /// <returns></returns>
        public string GetRoomList(long roomTypeId, long buildId = 0, long floorId = 0, string roomNO = "")
        {
            var models = RoomBll.GetListBy2(UserContext.CurrentUser.HotelId, buildId, floorId, roomTypeId, roomNO);
            return JsonConvert.SerializeObject(models);
        }

        public JsonResult Save(RoomYd model, List<RoomYdRoom> roomYdRoomList)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                long backId = 0;
                RoomYdBll.AddOrUpdate(model, roomYdRoomList, user.HotelId, UserContext.CurrentUser.Id, user.Name, ref backId);
                apiResult.ExtData = backId.ToString();
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
                RoomYdBll.DeleteById(id);
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
        /// 设置状态
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "设置状态", IsFormPost = false, LogWay = OprtLogType.未知, IsFromCache = true)]
        public ActionResult SetYdStatus(long id, YdStatusEnum status)
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.SetYdStatus(id, status);
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
        /// 设置状态
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "恢复取消", IsFormPost = false, LogWay = OprtLogType.未知, IsFromCache = true)]
        public ActionResult SetYdCancel(long id, bool isCancel, string reason = "")
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.SetYdCancel(id, isCancel, reason);
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

        [HttpGet]
        public string GetById(long id)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            //获取房型
            var roomTypeList = RoomTypeBll.GetList(hotelId);
            if (roomTypeList == null)
                roomTypeList = new List<RoomType>();
            if (id == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    Model = new RoomYd() { HotelId = hotelId },
                    RoomTypeList = roomTypeList
                });
            }
            else
            {
                //获取所有预订的房间
                var roomYdRoomList = RoomYdRoomBll.GetLIst(id);
                if (roomYdRoomList != null && roomYdRoomList.Count > 0)
                {
                    //循环获取房间
                    foreach (var roomtype in roomTypeList)
                    {
                        var roomList = roomYdRoomList.Where(m => m.RoomTypeId == roomtype.Id).ToList();
                        if (roomList != null && roomList.Count() > 0)
                        {
                            roomtype.YdRoomList = roomList;
                            //foreach (var room in roomList)
                            //{
                            //    if (room.RoomId != 0)
                            //        roomtype.YdRoomList.Add(new RoomYdRoom() { Id = room.RoomId, RoomNO = room.RoomNO });
                            //}
                            roomtype.RoomPriceId = roomList.ElementAt(0).RoomPriceId;
                            roomtype.RoomCount = roomList.ElementAt(0).RoomCount;
                            roomtype.RoomPrice = roomList.ElementAt(0).Price;
                            roomtype.RoomSale = roomList.ElementAt(0).Sale;
                            roomtype.RoomSalePrice = roomList.ElementAt(0).SalePrice;
                        }
                        else
                        {
                            roomtype.YdRoomList = new List<RoomYdRoom>();
                            roomtype.RoomCount = 0;
                        }
                    }
                }
                var info = RoomYdBll.GetById(id);
                return JsonConvert.SerializeObject(new
                {
                    Model = info,
                    RoomTypeList = roomTypeList
                });
            }
        }

        /// <summary>
        /// 预订转入住
        /// </summary>
        /// <param name="ydid"></param>
        /// <returns></returns>
        public ActionResult Ydzrz(long ydid)
        {
            if (ydid <= 0)
                return Content("无预订数据");
            var info = RoomYdBll.GetById(ydid);
            if (info == null)
                return Content("预订数据不存在");
            Model.RoomReg zfRoomReg = null;
            if (info.ZfRoomRegId > 0)
                zfRoomReg = RoomRegBll.GetById(info.ZfRoomRegId);
            var hotelId = UserContext.CurrentUser.HotelId;
            //获取房型
            var roomTypeList = RoomTypeBll.GetList(hotelId);
            if (roomTypeList == null)
                roomTypeList = new List<RoomType>();
            var ydRoomTypeList = new List<RoomType>();
            //获取所有预订的房间
            var roomYdRoomList = RoomYdRoomBll.GetLIst(ydid);
            if (roomYdRoomList != null && roomYdRoomList.Count > 0)
            {
                //循环获取房间
                foreach (var roomtype in roomTypeList)
                {
                    var roomList = roomYdRoomList.Where(m => m.RoomTypeId == roomtype.Id);
                    if (roomList != null && roomList.Count() > 0)
                    {
                        roomtype.YdRoomList = new List<RoomYdRoom>();
                        foreach (var room in roomList)
                        {
                            room.Name = info.Name;
                            room.IsZf = zfRoomReg == null ? false : (room.RoomRegId == zfRoomReg.Id ? true : false);
                            //room.IsZf = room.RoomRegId == zfRoomReg.Id ? true : false;
                            room.YdaoTime = info.YdaoTime;
                            room.YliTime = info.YliTime;
                            roomtype.YdRoomList.Add(room);
                        }
                        roomtype.RoomCount = roomList.ElementAt(0).RoomCount;
                        //if (roomtype.RoomCount > roomtype.YdRoomList.Count)
                        //{
                        //    for (var p = roomtype.YdRoomList.Count; p < roomtype.RoomCount; p++)
                        //    {
                        //        roomtype.YdRoomList.Add(new RoomYdRoom()
                        //        {
                        //            Id = 0,
                        //            Name = info.Name,
                        //            RoomTypeId = roomtype.Id,
                        //            RoomTypeName = roomtype.Name,
                        //            RoomId = 0,
                        //            RoomNO = "",
                        //            IsZf = false,
                        //            Price = roomtype.YdRoomList.Count > 0 ? roomtype.YdRoomList[0].Price : roomtype.Price,
                        //            Sale = roomtype.YdRoomList.Count > 0 ? roomtype.YdRoomList[0].Sale : 1,
                        //            SalePrice = roomtype.YdRoomList.Count > 0 ? roomtype.YdRoomList[0].SalePrice : roomtype.Price,
                        //            YdaoTime = info.YdaoTime,
                        //            YliTime = info.YliTime,
                        //        });
                        //    }
                        //}
                    }
                    else
                    {
                        continue;
                        //roomtype.YdRoomList = new List<RoomYdRoom>();
                        //roomtype.RoomCount = 0;
                    }
                    ydRoomTypeList.Add(roomtype);
                }
            }

            ViewBag.YdId = ydid;
            ViewBag.RoomTypeList = ydRoomTypeList;
            return View();
        }

        #region 预定转入住

        /// <summary>
        /// 分配房号
        /// </summary>
        /// <param name="ydid">预定id</param>
        /// <param name="roomTypeId"></param>
        /// <returns></returns>
        public ActionResult _Fpfh(long ydid, long roomTypeId)
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.YdId = ydid;
            ViewBag.RoomTypeId = roomTypeId;
            return View();
        }

        /// <summary>
        /// 分配房号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FpfhSave(RoomYdRoom model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomYdRoom backModel = model;
                RoomYdBll.FpfhSave(model, out backModel, UserContext.CurrentUser.Id, user.Name);
                apiResult.ExtData = JsonConvert.SerializeObject(backModel);
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
        /// 设为主房
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetZf(RoomYdRoom model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomYdBll.SetZf(model, UserContext.CurrentUser.Id, user.Name);
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
        /// 取消分房
        /// </summary>
        /// <param name="roomYdRoomId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Qxff(long roomYdRoomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.Qxff(roomYdRoomId);
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
        /// 删除该记录
        /// </summary>
        /// <param name="roomYdRoomId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RoomYdRoomDel(long roomYdRoomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.RoomYdRoomDel(roomYdRoomId);
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
        /// 自动分房
        /// </summary>
        /// <param name="ydid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AutoFf(long ydid, string order)
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.AutoFf(ydid, order);
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
        /// 预订转入住界面的刷新--自动分房和取消分房后的刷新
        /// </summary>
        /// <param name="ydid"></param>
        /// <returns></returns>
        public string YdzrzRefresh(long ydid)
        {
            var info = RoomYdBll.GetById(ydid);
            Model.RoomReg zfRoomReg = null;
            if (info.ZfRoomRegId > 0)
                zfRoomReg = RoomRegBll.GetById(info.ZfRoomRegId);
            var hotelId = UserContext.CurrentUser.HotelId;
            //获取房型
            var roomTypeList = RoomTypeBll.GetList(hotelId);
            if (roomTypeList == null)
                roomTypeList = new List<RoomType>();
            var ydRoomTypeList = new List<RoomType>();
            //获取所有预订的房间
            var roomYdRoomList = RoomYdRoomBll.GetLIst(ydid);
            if (roomYdRoomList != null && roomYdRoomList.Count > 0)
            {
                //循环获取房间
                foreach (var roomtype in roomTypeList)
                {
                    var roomList = roomYdRoomList.Where(m => m.RoomTypeId == roomtype.Id);
                    if (roomList != null && roomList.Count() > 0)
                    {
                        roomtype.YdRoomList = new List<RoomYdRoom>();
                        foreach (var room in roomList)
                        {
                            room.Name = info.Name;
                            room.IsZf = zfRoomReg == null ? false : (room.RoomRegId == zfRoomReg.Id ? true : false);
                            //room.IsZf = room.RoomRegId == zfRoomReg.Id ? true : false;
                            room.YdaoTime = info.YdaoTime;
                            room.YliTime = info.YliTime;
                            roomtype.YdRoomList.Add(room);
                        }
                        roomtype.RoomCount = roomList.ElementAt(0).RoomCount;
                        //if (roomtype.RoomCount > roomtype.YdRoomList.Count)
                        //{
                        //    for (var p = roomtype.YdRoomList.Count; p < roomtype.RoomCount; p++)
                        //    {
                        //        roomtype.YdRoomList.Add(new RoomYdRoom()
                        //        {
                        //            Id = 0,
                        //            Name = info.Name,
                        //            RoomTypeId = roomtype.Id,
                        //            RoomTypeName = roomtype.Name,
                        //            RoomId = 0,
                        //            RoomNO = "",
                        //            IsZf = false,
                        //            Price = roomtype.YdRoomList.Count > 0 ? roomtype.YdRoomList[0].Price : roomtype.Price,
                        //            Sale = roomtype.YdRoomList.Count > 0 ? roomtype.YdRoomList[0].Sale : 1,
                        //            SalePrice = roomtype.YdRoomList.Count > 0 ? roomtype.YdRoomList[0].SalePrice : roomtype.Price,
                        //            YdaoTime = info.YdaoTime,
                        //            YliTime = info.YliTime,
                        //        });
                        //    }
                        //}
                    }
                    else
                    {
                        continue;
                        //roomtype.YdRoomList = new List<RoomYdRoom>();
                        //roomtype.RoomCount = 0;
                    }
                    ydRoomTypeList.Add(roomtype);
                }
            }
            return JsonConvert.SerializeObject(ydRoomTypeList);
        }

        /// <summary>
        /// 全部取消分房
        /// </summary>
        /// <param name="ydid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CancelAllFf(long ydid)
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.CancelAllFf(ydid);
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
        /// 确定转入住
        /// </summary>
        /// <param name="roomYdRoomList"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ZrzSave(long ydid, List<RoomYdRoom> roomYdRoomList)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomYdBll.ZrzSave(ydid, roomYdRoomList, user.Id, user.Name);
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
        /// 撤销入住
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CxRz(long roomYdRoomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomYdBll.CxRz(roomYdRoomId);
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

        #endregion
    }
}