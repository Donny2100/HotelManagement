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
    public class RoomRegController : AdminBaseController
    {
        // GET: RoomReg
        public ActionResult Index(long roomId)
        {
            var user = UserContext.CurrentUser;
            var hotelId = user.HotelId;

            var room = RoomBll.GetById(roomId);

            var globalFeeSet = GlobalFeeSetBll.GetByHotelId(hotelId);

            ViewBag.HotelId = hotelId;
            ViewBag.RoomId = roomId;
            ViewBag.GlobalFeeSet = globalFeeSet;
            var now = DateTime.Now;
            DateTime yuLiTime = now;
            //如果是6点，那么当天6点之后开的房间就是第二天中午退房，0点到6点开房就是当天中午退房
            if (now.Hour >= 0 && now.Hour <= globalFeeSet.QTAfterPointToNextDay)
            {
                //当天中午退房
                yuLiTime = now.Date.AddHours(globalFeeSet.QTExitPoint);
            }
            else
            {
                //第二天中午退房
                yuLiTime = now.Date.AddDays(1).AddHours(globalFeeSet.QTExitPoint);
            }

            //var yltime = "";
            //if (now.Hour > globalFeeSet.QTAfterPointToNextDay)
            //{
            //    //第二天退房
            //    yltime = now.AddDays(1).ToShortDateString() + $" {globalFeeSet.QTExitPoint}:00:00";
            //}
            //else
            //{
            //    //当天退房
            //    yltime = now.ToShortDateString() + $" {globalFeeSet.QTExitPoint}:00:00";
            //}
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.yltime = yuLiTime.ToString("yyyy-MM-dd HH:mm:ss");
            var my = UserBll.GetById(user.Id);
            ViewBag.MyKdzk = my == null ? 10 : my.CanSale;
            //获取登记单
            var roomReg = new Model.RoomReg();
            if (room != null)
            {
                if (room.FjState == FjStateEnum.干净房)
                    roomReg = new Model.RoomReg();
                else
                {
                    roomReg = RoomRegBll.GetById(room.RoomRegId);
                    if (roomReg == null)
                        roomReg = new Model.RoomReg();
                }
                if ((int)roomReg.FjState >= (int)FjStateEnum.脏房)
                {
                   // return Content("房间状态不允许开房登记");
                }
                //if(roomReg.FjState ==4)
                //    roomReg = new Model.RoomReg();
            } 
            ViewBag.PanelHeight = 300;
            if (Session["PanelHeight"] != null)
            {
                ViewBag.PanelHeight = Convert.ToInt32(Session["PanelHeight"]); 
            }
            if(room == null)
            {
                room = new Room();
                ViewBag.IsEmptyRoom = true;
            }
            else
            {

                ViewBag.IsEmptyRoom = false;
            }
            ViewBag.Room = room;
            return View(roomReg);
        }

        /// <summary>
        /// 获取联房列表
        /// </summary>
        /// <returns></returns>
        public string GetLfList(long roomRegId)
        {
            var list = RoomRegBll.GetLfList(roomRegId, UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(list);
        }

        public string GetLfListWithoutSelf(long roomRegId)
        {
            var list = RoomRegBll.GetLfList(roomRegId, UserContext.CurrentUser.HotelId);
 
            return JsonConvert.SerializeObject(list);
        }
        [HttpPost]
        public JsonResult SavePanelHeight(int value)
        {
            var apiResult = new APIResult();
            Session["PanelHeight"] = value;
            return Json(apiResult);
        }


        [HttpPost]
        public JsonResult RoomRegSave(Model.RoomReg model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            long backId = 0;
            try
            {
                RoomRegBll.AddOrUpdate(model, user.HotelId, user.Id, user.Name, ref backId);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            apiResult.ExtData = backId.ToString();
            return Json(apiResult);
        }

        [HttpPost]
        public JsonResult RoomRegDelete(long id)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegBll.RegDelete(id, user.HotelId);
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
        /// 选择房号页面
        /// </summary>
        /// <returns></returns>
        public ActionResult _SelRoom()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.HotelId = hotelId;
            return View();
        }

        /// <summary>
        /// 选择联房房间--添加联防时弹出
        /// </summary>
        /// <returns></returns>
        public ActionResult _SelRoomForLF()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.HotelId = hotelId;
            return View();
        }

        /// <summary>
        /// 选择房号界面获取房号列表
        /// </summary>
        /// <param name="roomTypeId"></param>
        /// <returns></returns>
        public string GetRoomList(long buildId = 0, long floorId = 0, long roomTypeId = 0, string roomNO = "")
        {
            var models = RoomBll.GetListBy(UserContext.CurrentUser.HotelId, buildId, floorId, roomTypeId, roomNO);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 为登记界面的  联防成员   选择联防（主房）
        /// </summary>
        /// <returns></returns>
        public ActionResult _SelZfForReg(long roomRegId)
        {
            ViewBag.RoomRegId = roomRegId;
            return View();
        }

        public string GetPagerForZf(int page, int rows, int cwState = 0, string searchName = null)
        {
            var pager = RoomRegBll.GetPagerForZf(page, rows, UserContext.CurrentUser.HotelId, cwState, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 设为主房
        /// </summary>
        /// <returns></returns>
        public JsonResult LfSetZf(long id)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegBll.LfSetZf(id);
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

        public string GetRoomReg(long roomRegId)
        {
            var model = RoomRegBll.GetById(roomRegId); 
            return JsonConvert.SerializeObject(model);
        }
        /// <summary>
        /// 获取具体房间信息
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public string GetRoom(long roomId)
        {
            var model = RoomBll.GetById(roomId);
            List<RoomPrice> priceList = null;
            if (model != null)
            {
                //根据房型id获取房价方案
                priceList = RoomPriceBll.GetList(UserContext.CurrentUser.HotelId, model.RoomTypeId);
            }
            else
                model = new Room();
            return JsonConvert.SerializeObject(new { Room = model, PriceList = priceList });
        }

        /// <summary>
        /// 获取具体房间信息for联房
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public string GetRoomForLF(long roomId)
        {
            var model = RoomBll.GetById(roomId);
            List<RoomPrice> priceList = null;
            if (model == null)
                model = new Room();

            return JsonConvert.SerializeObject(new { Room = model, PriceList = priceList });
        }

        /// <summary>
        /// 凌晨房检测，返回凌晨价
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public string LCRoomCheck(long roomId)
        {
            try
            {
                var hotelId = UserContext.CurrentUser.HotelId;
                var model = RoomBll.GetById(roomId);
                var roomType = RoomTypeBll.GetById(model.RoomTypeId);
                var setting = GlobalFeeSetBll.GetByHotelId(hotelId);

                var now = DateTime.Now; 
         
                if(now.Hour >= setting.LCStartFeePoint && now.Hour <= setting.LCEndFeePoint)
                {
                    return JsonConvert.SerializeObject(new { IsLC = true, RoomPrice = roomType.MorningPrice });
                }
                return JsonConvert.SerializeObject(new { IsLC = false });

            }
            catch
            {
                return JsonConvert.SerializeObject(new { IsLC = false });
            }
           
        }


        /// <summary>
        /// 根据开房方式获取预离时间，同时绑定钟点房方案id或时段房方案id
        /// 20190828同时绑定价格
        /// </summary>
        /// <returns></returns>
        public string GetYlTimeByKffs(long roomRegId,int type, int rzDays,long fnid = 0, long roomId = 0)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            var now = DateTime.Now;
            if (roomReg != null)
                now = TypeConvert.IntToDateTime(roomReg.RegTime);

            var room = RoomBll.GetById(roomId);
            var roomType = RoomTypeBll.GetById(room.RoomTypeId);
            if (type == (int)KaifangFangshiEnum.全天房 || type == (int)KaifangFangshiEnum.长包房 || type == (int)KaifangFangshiEnum.免费房)
            {
                var globalFeeSet = GlobalFeeSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
                if (globalFeeSet == null)
                    return JsonConvert.SerializeObject(new { Ret = 0, CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss"), YlTime = "" });

                var yltime = now;
                //如果是6点，那么当天6点之后开的房间就是第二天中午退房，0点到6点开房就是当天中午退房
                if (now.Hour >= 0 && now.Hour <= globalFeeSet.QTAfterPointToNextDay)
                {
                    //当天中午退房
                    yltime = now.Date.AddDays(rzDays - 1).AddHours(globalFeeSet.QTExitPoint);
                }
                else
                {
                    //第二天中午退房
                    yltime = now.Date.AddDays(1 + (rzDays - 1)).AddHours(globalFeeSet.QTExitPoint);
                }

                if(type != (int)KaifangFangshiEnum.免费房)
                {
                    return JsonConvert.SerializeObject(new {
                        Ret = 0,
                        CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss"),
                        YlTime = yltime.ToString("yyyy-MM-dd HH:mm:ss"),
                        RoomPrice = roomType.Price
                    });
                }

                return JsonConvert.SerializeObject(new { Ret = 0, CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss"), YlTime = yltime.ToString("yyyy-MM-dd HH:mm:ss") });
            }
            else if (type == (int)KaifangFangshiEnum.钟点房)
            {
                //钟点房
                var hourRoomList = HourRoomBll.GetList(UserContext.CurrentUser.HotelId);
                if (hourRoomList == null || hourRoomList.Count == 0)
                    return JsonConvert.SerializeObject(new { Ret = -1, Msg = "无钟点房方案" });
                var hourRoom = hourRoomList[0];//默认选择第一个
                if(fnid != 0 && hourRoomList.Any(a=>a.Id == fnid))
                {
                    hourRoom = hourRoomList.FirstOrDefault(a => a.Id == fnid);
                }
                var yltime = now.AddMinutes(hourRoom.Minute).ToString("yyyy-MM-dd HH:mm:ss");
                 

                return JsonConvert.SerializeObject(new { Ret = 0, CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss"), YlTime = yltime, Id = hourRoom.Id,RoomPrice = hourRoom.Price });
            }
            else if (type == (int)KaifangFangshiEnum.时段房)
            {
                //时段房
                var periodRoomList = PeriodRoomBll.GetList(UserContext.CurrentUser.HotelId);
                if (periodRoomList == null || periodRoomList.Count == 0)
                    return JsonConvert.SerializeObject(new { Ret = -1, Msg = "无时段房方案" });

                if (fnid != 0 && periodRoomList.Any(a => a.Id == fnid))
                {
                    var periodRoom = periodRoomList.FirstOrDefault(a => a.Id == fnid);
                    var CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                    var YlTime = now.ToShortDateString() + $" {periodRoom.EndPoint}:00:00";
                    return JsonConvert.SerializeObject(new { Ret = 0, CurrentTime = CurrentTime, YlTime = YlTime, Id = periodRoom.Id, RoomPrice = periodRoom.Price });
                }
                else
                {
                    foreach (var periodRoom in periodRoomList)
                    {
                        if (now.Hour >= periodRoom.StartPoint && now.Hour <= periodRoom.EndPoint)
                        {
                            var CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                            var YlTime = now.ToShortDateString() + $" {periodRoom.EndPoint}:00:00";
                            return JsonConvert.SerializeObject(new { Ret = 0, CurrentTime = CurrentTime, YlTime = YlTime, Id = periodRoom.Id, RoomPrice = periodRoom.Price });
                        }
                    }
                }
               
                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "无匹配的时段房方案，请手动选择" });
            }
            return "";
        }

        public string GetNewRoomPrice(int type, long fnid , long roomId)
        {
 
            var now = DateTime.Now; 

            var room = RoomBll.GetById(roomId);
            var roomType = RoomTypeBll.GetById(room.RoomTypeId);
           if (type == (int)KaifangFangshiEnum.钟点房)
            {
                //钟点房
                var hourRoomList = HourRoomBll.GetList(UserContext.CurrentUser.HotelId);
                if (hourRoomList == null || hourRoomList.Count == 0)
                    return JsonConvert.SerializeObject(new { Ret = -1, Msg = "无钟点房方案" });
                var hourRoom = hourRoomList[0];//默认选择第一个
                if (fnid != 0 && hourRoomList.Any(a => a.Id == fnid))
                {
                    hourRoom = hourRoomList.FirstOrDefault(a => a.Id == fnid);
                }
                var yltime = now.AddMinutes(hourRoom.Minute).ToString("yyyy-MM-dd HH:mm:ss");


                return JsonConvert.SerializeObject(new { Ret = 0, Id = hourRoom.Id, RoomPrice = hourRoom.Price });
            }
            else if (type == (int)KaifangFangshiEnum.时段房)
            {
                //时段房
                var periodRoomList = PeriodRoomBll.GetList(UserContext.CurrentUser.HotelId);
                if (periodRoomList == null || periodRoomList.Count == 0)
                    return JsonConvert.SerializeObject(new { Ret = -1, Msg = "无时段房方案" });

                if (fnid != 0 && periodRoomList.Any(a => a.Id == fnid))
                {
                    var periodRoom = periodRoomList.FirstOrDefault(a => a.Id == fnid);
                    var CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
                    var YlTime = now.ToShortDateString() + $" {periodRoom.EndPoint}:00:00";
                    return JsonConvert.SerializeObject(new { Ret = 0, Id = periodRoom.Id, RoomPrice = periodRoom.Price });
                } 

                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "无匹配的时段房方案，请手动选择" });
            }
            return "";
        }




        /// <summary>
        /// 续住时获取预离时间
        /// </summary>
        /// <returns></returns>
        public string GetYlTimeForXz(long roomRegId, int xzDays)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "房间登记数据不存在" });
            if (roomReg.KaiFangFangShi != (int)KaifangFangshiEnum.全天房 && roomReg.KaiFangFangShi != (int)KaifangFangshiEnum.长包房
                && roomReg.KaiFangFangShi != (int)KaifangFangshiEnum.免费房)
                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "只有【全天房、长包房、免费房】才可续住" });
            var globalFeeSet = GlobalFeeSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
            var now = TypeConvert.IntToDateTime(roomReg.RegTime);
            if (globalFeeSet == null)
                return JsonConvert.SerializeObject(new { Ret = 0, YlTime = "" });
            var yltime = now;
            //如果是6点，那么当天6点之后开的房间就是第二天中午退房，0点到6点开房就是当天中午退房
            if (now.Hour >= 0 && now.Hour <= globalFeeSet.QTAfterPointToNextDay)
            {
                //当天中午退房
                yltime = now.Date.AddDays(xzDays - 1).AddHours(globalFeeSet.QTExitPoint);
            }
            else
            {
                //第二天中午退房
                yltime = now.Date.AddDays(1 + (xzDays - 1)).AddHours(globalFeeSet.QTExitPoint);
            }

            var yulidate = TypeConvert.IntToDateTime(roomReg.LeaveTime);
            yltime = yulidate.Date.AddDays(xzDays).AddHours(globalFeeSet.QTExitPoint);

            return JsonConvert.SerializeObject(new { Ret = 0, YlTime = yltime.ToString("yyyy-MM-dd HH:mm:ss") });
        }

        public string GetRuzhuDaysForXz(long roomRegId, string leaveTime)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "房间登记数据不存在" });
            if (roomReg.KaiFangFangShi != (int)KaifangFangshiEnum.全天房 && roomReg.KaiFangFangShi != (int)KaifangFangshiEnum.长包房
                && roomReg.KaiFangFangShi != (int)KaifangFangshiEnum.免费房)
                return JsonConvert.SerializeObject(new { Ret = -1, Msg = "只有【全天房、长包房、免费房】才可续住" });
            var globalFeeSet = GlobalFeeSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
 
            if (globalFeeSet == null)
                return JsonConvert.SerializeObject(new { Ret = 0, YlTime = "" });

            var days = (DateTime.Parse(leaveTime).Date - TypeConvert.IntToDateTime(roomReg.LeaveTime).Date).TotalDays;

            return JsonConvert.SerializeObject(new { Ret = 0, RuzhuDays = Convert.ToInt32(days) });
        }



        /// <summary>
        /// 续住的保存
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <param name="xzDays"></param>
        /// <returns></returns>
        public JsonResult RoomRegXzSave(long roomRegId, int xzDays, string yulitime)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegBll.RoomRegXzSave(roomRegId, xzDays, yulitime);
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
        /// 房价授权
        /// </summary>
        /// <returns></returns>
        public ActionResult _Fjsq()
        {
            return View();
        }

        /// <summary>
        /// 选择特邀说明
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public ActionResult _SelTysm(long roomRegId)
        {
            var tysmList = new List<Tysm>();
            tysmList = TysmBll.GetList(UserContext.CurrentUser.HotelId);
            if (tysmList == null)
                tysmList = new List<Tysm>();
            if (tysmList.Count > 0)
            {
                var roomReg = RoomRegBll.GetById(roomRegId);
                if (roomReg != null)
                {
                    string tysmids = roomReg.TysmIds;
                    if (!string.IsNullOrWhiteSpace(tysmids))
                    {
                        var tysmidArr = tysmids.Replace('，', ',').Split(',');
                        foreach (var tysmid in tysmidArr)
                        {
                            tysmList.ForEach(m =>
                            {
                                if (m.Id.ToString() == tysmid)
                                    m.IsCheck = true;
                            });
                        }
                    }
                }
            }
            return View(tysmList);
        }

        #region 客人资料
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
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetGuestList(string searchName,string state)
        {
            var list = RoomRegBll.GetList(UserContext.CurrentUser.HotelId, searchName, state);
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 客人资料
        /// </summary>
        /// <param name="zfdjid"></param>
        /// <param name="djids"></param>
        /// <returns></returns>
        public ActionResult _Guest(string zfdjid, string djids, string roomnos)
        {
            var dic = RoomRegGuestBll.GetList(zfdjid, djids, roomnos);
            return View(dic);
        }

        /// <summary>
        /// 客人信息的保存
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(Cat) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult GuestSave(long roomRegId, List<RoomRegGuest> models)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegGuestBll.AddOrUpdate(roomRegId, ref models);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            apiResult.ExtData = JsonConvert.SerializeObject(models);
            return Json(apiResult);
        }

        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "删除", IsFormPost = false, LogWay = OprtLogType.删除, IsFromCache = true)]
        public JsonResult GuestDel(long id, long roomRegId)
        {
            var apiResult = new APIResult();
            var models = new List<RoomRegGuest>();
            try
            {
                RoomRegGuestBll.DeleteBy(id, roomRegId, ref models);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            apiResult.ExtData = JsonConvert.SerializeObject(models);
            return Json(apiResult);
        }

        #endregion

        #region 转账时登记单的选择

        /// <summary>
        /// 转账时  选择登记单  只可选择正常入住或未结退房
        /// </summary>
        /// <returns></returns>
        public ActionResult _RoomRegSel(long roomRegId)
        {
            ViewBag.RoomRegId = roomRegId;
            return View();
        }

        public string GetPagerForZz(int page, int rows, int cwState = 0, string searchName = null)
        {
            var pager = RoomRegBll.GetPagerForZz(page, rows, UserContext.CurrentUser.HotelId, cwState, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        #endregion

        #region 佣金设置

        [HttpGet]
        public string GetYjSet(long roomRegId)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                return JsonConvert.SerializeObject(new { code = -1, msg = "房间未登记，不可操作" });
            var model = RoomRegYjBll.GetBy(roomReg.Id);
            return JsonConvert.SerializeObject(new { code = 0, data = model });
        }

        [HttpPost]
        public JsonResult YjSetSave(RoomRegYj model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            long backId = 0;
            try
            {
                RoomRegYjBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            apiResult.ExtData = backId.ToString();
            return Json(apiResult);
        }

        #endregion
    }
}