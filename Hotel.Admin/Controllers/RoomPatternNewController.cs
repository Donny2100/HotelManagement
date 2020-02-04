using Hotel.Bll;
using Hotel.Model;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NIU.Forum.Common;

namespace Hotel.Admin.Controllers
{
    /// <summary>
    /// 房态图
    /// </summary>
    public partial class RoomPatternController : AdminBaseController
    {
        public ActionResult _XZ()
        {
            return View();
        }
        public ActionResult _SelHotel()
        {
            return View();
        }
        public ActionResult _SelHotel2()
        {
            return View();
        }
        public string GetInHotelPager(int page, int rows, int cwState = 0, string searchName = null)
        {
            var pager = RoomRegBll.GetInRoomPagerBySearch(page, rows, UserContext.CurrentUser.HotelId,  searchName);
            return JsonConvert.SerializeObject(pager);
        }
        public string BatchRoomList(int page, int rows, long buildId ,long floorId, string roomNO, string changeType)
        {
            var models = RoomBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, buildId, floorId, roomNO, changeType);

           


            foreach (var item in models.rows)
            {
                if(item.RoomRegId  != 0)
                {
                    item.RoomReg = RoomRegBll.GetById(item.RoomRegId);
                }


                LoadRoomInfo2(item); 
            
            }
             
            return JsonConvert.SerializeObject(models);
        }
        public string GetHotelPager(int page, int rows,int cwState,   string searchName = null)
        {
            var pager = RoomRegBll.GetPagerBySearch(page, rows, UserContext.CurrentUser.HotelId, cwState, searchName);//正常入住
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 房态里面单个房间的HTML部分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _Room(long id)
        {
            var hotelId = UserContext.CurrentUser.HotelId; 
            var roomSet = RoomSetBll.GetBy(hotelId);
            if (roomSet == null)
                roomSet = RoomSetBll.GetBy(0);
 
           
            var today = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            var item = RoomBll.GetById(id);
            if(item.RoomRegId != 0)
            {
                item.RoomReg = roomRegs.Where(a => a.Id == item.RoomRegId).FirstOrDefault();
            } 
            LoadRoomInfo(item);
            return View(item);
        }
        public ActionResult BatchMaintainRoom()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            HttpCookie TadyNum = new HttpCookie("TadyNum");
            TadyNum["num"] = "0";
            Response.Cookies.Add(TadyNum);
            var now = DateTime.Now;
            DateTime yuLiTime = now;
            yuLiTime = now.Date.AddDays(1).AddHours(12);
            ViewBag.BeginTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.EndTime = yuLiTime.ToString("yyyy-MM-dd HH:mm:ss");
             
            var treedata = RoomBll.GetFloorTreeData(hotelId);
            ViewBag.Rooms = RoomBll.GetAllRoomData2(hotelId);
            ViewBag.TreeData = treedata;
            PreLoadFTData();
            var simpleModels = new List<object>();
            var models = _ft_rooms;
            var roomRegs = _ft_roomregs;
            foreach (var item in models)
            {
                item.RoomReg = roomRegs.Where(a => a.Id == item.RoomRegId).FirstOrDefault();
                LoadRoomInfo(item);
                simpleModels.Add(RoomToSRoom(item));
            }
            ViewBag.SRooms = simpleModels;

            return View(UserContext.CurrentUser);
        }
        [HttpPost]
        public ActionResult SaveBatchMaintainRoom(MaintainRoom model, long[] roomIds)
        {

            var apiResult = new APIResult(); 
            try
            {
                MaintainRoomBll.SaveMaRooms(model, roomIds);
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
 

        public ActionResult _SelTyly(long roomId)
        {
            ViewBag.RoomId = roomId;
            var room = RoomBll.GetById(roomId);
            var msg = RoomBll.GetSpecialMessage(roomId);
            if (msg == null) msg = new RoomMessage();
            ViewBag.Message = msg;

            ViewBag.LockBz = room.LockBz;
            return View();
        }

        public ActionResult SaveSpecialMessage(long roomId,RoomMessage msg)
        {

            var apiResult = new APIResult();
            try
            {
                RoomBll.SaveSpecialMessage(roomId, msg.MsgContent, msg.IsEnabled);
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



        public ActionResult SaveMessage(RoomMessage msg)
        {

            var apiResult = new APIResult();
            try
            {
                RoomBll.SaveMessage(msg);
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
        /// 房间留言
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public ActionResult _SelMessage(long roomId)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.HotelId = hotelId;
            ViewBag.RoomId = roomId;
            var room = RoomBll.GetById(roomId);
            if(room != null)
            {
                var msg = RoomBll.GetMessage(roomId);
                if (msg == null) msg = new RoomMessage();
                ViewBag.Message = msg;
                ViewBag.RoomRegId = room.RoomRegId;
                ViewBag.Room = room;
                ViewBag.RoomReg = RoomRegBll.GetById(room.RoomRegId);
            }
           else
            {
                ViewBag.Message = new RoomMessage();
                ViewBag.Room = new Hotel.Model.Room();
                ViewBag.RoomRegId = 0;
                ViewBag.RoomReg = new Hotel.Model.RoomReg();
            }

            return View();
        }


        public ActionResult _SelMessage2()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.HotelId = hotelId;
            ViewBag.RoomId = 0;
            ViewBag.Room = new Hotel.Model.Room();
            ViewBag.RoomRegId = 0;
            ViewBag.RoomReg = new Hotel.Model.RoomReg();

            return View();
        }

        public ActionResult ChangeRoom(long roomId,long roomRegId = 0)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            if(roomRegId != 0)
            {
                ViewBag.RoomRegId = roomRegId;
                var rg = RoomRegBll.GetById(roomRegId); 
                 
                ViewBag.RoomId = rg.RoomId;
            }
            else
            {
                ViewBag.RoomId = roomId;

                var model = RoomBll.GetById(roomId);
                if (model == null)
                {
                    model = new Room();
                }
                ViewBag.RoomRegId = model.RoomRegId;
            }
          
            return View(UserContext.CurrentUser);
        }

        public ActionResult GroupRoom(long roomId)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.RoomId = roomId;


            var model = RoomBll.GetById(roomId);

            ViewBag.RoomRegId = model.RoomRegId;

            return View(UserContext.CurrentUser);
        }
        public ActionResult SplitRoom(long roomId)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.RoomId = roomId;

            var model = RoomBll.GetById(roomId);

            ViewBag.RoomRegId = model.RoomRegId;
            return View(UserContext.CurrentUser);
        }
        /// <summary>
        /// 提交换房
        /// </summary>
        /// <param name="OldRoomId"></param>
        /// <param name="NewRoomId"></param>
        /// <param name="Remark"></param>
        /// <param name="RoomPriceFaId"></param>
        /// <param name="ChangeType"></param>
        /// <returns></returns>
        public ActionResult SaveChangeRoom(RoomChangeRecord data)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.SaveChangeRoom(data);
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

        public string GetRoomInfo(long roomId)
        {
            var model = RoomBll.GetById(roomId);
            List<RoomPrice> priceList = null;
            if (model != null)
            {
                //根据房型id获取房价方案
                priceList = RoomPriceBll.GetList(UserContext.CurrentUser.HotelId, model.RoomTypeId);
            }
            else
            { 
                model = new Room();
            }

            Hotel.Model.RoomReg reg = null;

            if(model.RoomRegId != 0)
            {
                reg = RoomRegBll.GetById(model.RoomRegId);
            }

            var roomType = RoomTypeBll.GetById(model.RoomTypeId);
            var HolidaySet = HolidaySetBll.GetList(UserContext.CurrentUser.HotelId);
            var WeekendSet = WeekendSetBll.GetByHotelId(UserContext.CurrentUser.HotelId);
             
            var SalePrice = model.Price; //价
            double SaleRate = 1.0;//折扣率
            if (roomType.IsAllowWeekendRoom) //周末价判断
            {
                if(WeekendSet.IsMonday && DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
                if (WeekendSet.IsTuesday && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
                if (WeekendSet.IsWednesday && DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
                if (WeekendSet.IsThursday && DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
                if (WeekendSet.IsFriday && DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
                if (WeekendSet.IsSaturday && DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
                if (WeekendSet.IsSunday && DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    SalePrice = roomType.WeekendPrice;
                }
            }
            if(roomType.IsAllowHolidayRoom) //假日价
            {
                foreach(var h in HolidaySet)
                {
                    var start = TypeConvert.IntToDateTime(h.StartDate);
                    var end = TypeConvert.IntToDateTime(h.EndDate).AddDays(1).AddSeconds(-1); //定位在这天最后一秒
                    if(DateTime.Now > start && DateTime.Now < end)
                    {
                        SalePrice = roomType.HolidayPrice;
                    }
                }
            }



            SaleRate = Convert.ToDouble(SalePrice) / Convert.ToDouble(model.Price);


            return JsonConvert.SerializeObject(new
            {
                Room = model,
                RoomPrice = roomType.Price,
                SalePrice = SalePrice,
                SaleRate = SaleRate,
                PriceList = priceList,
                RoomReg = reg
            });
        }


        public ActionResult _BatchRoom()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.HotelId = hotelId;
            return View();
        }
        [HttpPost]
        public ActionResult SaveBatchRoomStatus(long[] roomIds, string changeType, long EmployeeId = 0)
        {

            var apiResult = new APIResult();
            try
            {
                RoomBll.SaveBatchRoomStatus(roomIds, changeType, EmployeeId);
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

        public ActionResult SetStartCleanRoom(long roomId ,long employeeId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.SetStartCleanRoom(roomId, employeeId);
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

        public ActionResult AddLf(long roomRegId, long lfRoomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegBll.AddLf(roomRegId, lfRoomId);
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
        /// 设置为脏房
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public JsonResult SetToZF(long roomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.SetToZF(roomId);
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
        /// 设置为脏住房
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public JsonResult SetToZZF(long roomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.SetToZZF(roomId);
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
        /// 脏住房清理干净
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public JsonResult ZZFSetToClean(long roomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomBll.ZZFSetToClean(roomId);
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
        public ActionResult getRoomUpdatData()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var data = RoomBll.GetUpdateData(hotelId);
            return Content(JsonConvert.SerializeObject(data));
        }
        [HttpGet]
        public ActionResult getRoomSet()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var data = RoomBll.GetRoomSet(hotelId);
            return Content(JsonConvert.SerializeObject(data));
        }
        [HttpPost]
        public ActionResult GetFullData(long[] RoomIds)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            if(RoomIds.Length > 0)
            {
                return GetFullData2(RoomIds);
            }
            var rooms = new List<object>();
            foreach (var RoomId in RoomIds)
            {
                var item = RoomBll.GetById(RoomId);
                if (item.RoomRegId != 0)
                {
                    item.RoomReg = RoomRegBll.GetById(item.RoomRegId);
                }

                 LoadRoomInfo(item); 

                // var sroom = RoomToSRoom(item);
                rooms.Add(RoomToSRoom(item));
            }

            return Content(JsonConvert.SerializeObject(new { Rooms = rooms }));
        }

        public ActionResult GetFullData2(long[] RoomIds)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            PreLoadFTData();
            var rooms = new List<object>();
            foreach (var RoomId in RoomIds)
            {
                var item = _ft_rooms.FirstOrDefault(a => a.Id == RoomId);
                if (item.RoomRegId != 0)
                {
                    item.RoomReg = _ft_roomregs.FirstOrDefault(a => a.Id == item.RoomRegId);
                } 
                LoadRoomInfo(item); 
                rooms.Add(RoomToSRoom(item));
            } 
            return Content(JsonConvert.SerializeObject(new { Rooms = rooms }));
        }



        private bool _ft_isLoaded = false;
        private List<Room> _ft_rooms = new List<Room>();
        private List<Hotel.Model.RoomReg> _ft_roomregs = new List<Model.RoomReg>();
        private List<Member> _ft_members = new List<Member>();
        private List<MemberType> _ft_membersType = new List<MemberType>();
        private List<RoomRegGuestInfoCN> _ft_RoomRegGuestInfoCN = new List<RoomRegGuestInfoCN>();
        private List<RoomRegGuestInfoEN> _ft_RoomRegGuestInfoEN = new List<RoomRegGuestInfoEN>();
        private List<MaterialLease> _ft_MaterialLease = new List<MaterialLease>();
        private List<RoomType> _ft_RoomType = new List<RoomType>();
        private List<RoomYdRecord> _ft_RoomYdRecord = new List<RoomYdRecord>();
        private List<MaintainRoom> _ft_MaintainRoom = new List<MaintainRoom>();
        private List<RoomYd> _ft_RoomYD = new List<RoomYd>();
        private List<RoomSelfuse> _ft_RoomSelfuse = new List<RoomSelfuse>();
        private GlobalFeeSet _ft_GlobalFeeSet = new GlobalFeeSet();
        private RoomSet _ft_RoomSet = new RoomSet();


        private void PreLoadFTData()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            if (!_ft_isLoaded)
            {
                _ft_isLoaded = true;
                _ft_rooms = RoomBll.GetListBy(hotelId);
                _ft_roomregs = RoomRegBll.GetListByHotelId(hotelId);
                _ft_members = MemberBll.GetList(hotelId);
                _ft_membersType = MemberTypeBll.GetList(hotelId);
                _ft_RoomRegGuestInfoCN = RoomRegGuestInfoCNBll.GetListByHotelId(hotelId);
                _ft_RoomRegGuestInfoEN = RoomRegGuestInfoENBll.GetListByHotelId(hotelId);
                _ft_MaterialLease = MaterialLeaseBll.GetListByHotel(hotelId);
                _ft_RoomType = RoomTypeBll.GetListByHotelId(hotelId);
                _ft_RoomYdRecord = RoomYdRecordBll.GetListByHotel(hotelId);
                _ft_GlobalFeeSet = GlobalFeeSetBll.GetByHotelId(hotelId);
                _ft_MaintainRoom = MaintainRoomBll.GetListByHotelId(hotelId);
                _ft_RoomSelfuse = RoomSelfBll.GetListByHotelId(hotelId);
                _ft_RoomYD = RoomYdBll.GetListByHotel(hotelId);
                _ft_RoomSet = RoomSetBll.GetBy(hotelId);
                if (_ft_RoomSet == null)
                    _ft_RoomSet = RoomSetBll.GetBy(0);
            }
        }

        public ActionResult GetFullData_Test1()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var d = DateTime.Now;
            PreLoadFTData(); 
            return Content((DateTime.Now - d).TotalMilliseconds.ToString());
        }

        public ActionResult GetFullData_Test2()
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var d = DateTime.Now;
            PreLoadFTData();
            var rooms = new List<object>();
            foreach (var item in _ft_rooms)
            {
                if (item.RoomRegId != 0)
                {
                    item.RoomReg = _ft_roomregs.FirstOrDefault(a => a.Id == item.RoomRegId);
                }

                 LoadRoomInfo(item); 

                // var sroom = RoomToSRoom(item);
                rooms.Add(RoomToSRoom(item));
            }

            return Content((DateTime.Now - d).TotalMilliseconds.ToString());
        }
        public ActionResult GetRoomShowData(long roomId)
        {
            var apiResult = new APIResult();
            var hotelId = UserContext.CurrentUser.HotelId;
            
            try
            { 
                var item = RoomBll.GetById(roomId); 
                if (item.RoomRegId != 0)
                {
                    item.RoomReg = RoomRegBll.GetById(item.RoomRegId); 
                }

                LoadRoomInfo(item);

                return Json(RoomToSRoom(item));
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
        public ActionResult _BatchMaterial(string roomIds)
        {
            ViewBag.RoomIds = roomIds;

            var roomids = roomIds.Split(',');
            var room = RoomBll.GetById(Convert.ToInt64(roomids[0]));

            ViewBag.RoomId = room.Id;
            ViewBag.RoomTypeId = room.RoomTypeId;
            ViewBag.RoomRegId = room.RoomRegId;

            ViewBag.IsMaterialChangeEveryday = true;
            ViewBag.IsMaterialChangeWash = true;



            return View();
        }

        public ActionResult _Material(long roomId)
        {
            ViewBag.RoomId = roomId;

            var room = RoomBll.GetById(roomId);

            ViewBag.RoomTypeId = room.RoomTypeId;
            ViewBag.RoomRegId = room.RoomRegId;

            if(room.LastCleanEmployeeId != 0)
            {
                if(room.FjState != FjStateEnum.脏房)
                {

                    ViewBag.EmployeeId = room.LastCleanEmployeeId;
                    var employee = EmployeeBll.GetById(room.LastCleanEmployeeId);
                    ViewBag.EmployeeName = employee.Name;
                }


                ViewBag.IsMaterialChangeEveryday = room.IsMaterialChangeEveryday;
                ViewBag.IsMaterialChangeWash = room.IsMaterialChangeWash;
            }else
            {
                ViewBag.IsMaterialChangeEveryday = true;
                ViewBag.IsMaterialChangeWash = true;
            }



            return View();
        }
        [HttpPost]
        public JsonResult SaveRoomMaterial(string dataJson, Room model)
        {
            var apiResult = new APIResult();
            try
            {
                var data = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DisposableMaterial>>(dataJson);

                RoomBll.SaveRoomMaterial(data, model);
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
        public JsonResult SaveRoomsMaterial(string dataJson, Room model,string Ids)
        {
            var apiResult = new APIResult();
            try
            {
                var data = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<DisposableMaterial>>(dataJson);

                RoomBll.SaveRoomsMaterial(data, model, Ids);
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

        public ActionResult _Split(long roomId)
        {
            ViewBag.RoomId = roomId;

            var room = RoomBll.GetById(roomId);

            ViewBag.RoomTypeId = room.RoomTypeId;
            ViewBag.RoomRegId = room.RoomRegId;
             


            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult SplitSave(long[] roomRegIds)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegBll.LfSplit(roomRegIds);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }

            return Json(apiResult);
        }

        
        [HttpPost]
        public JsonResult LfGroup(long roomRegId, long[] addRoomRegIds)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegBll.LfGroup(roomRegId, addRoomRegIds);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }

            return Json(apiResult);
        }
    }
}