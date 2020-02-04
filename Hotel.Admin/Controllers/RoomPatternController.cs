using Hotel.Bll;
using Hotel.Model;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    /// <summary>
    /// 房态图
    /// </summary> 

    public partial class RoomPatternController : AdminBaseController
    {
        private static List<Room> RoomNew;
        private static RoomSet roomSet;
        private static List<Room> models;
        private static List<Model.RoomReg> roomRegs;
        private static List<AgreeCompany> agreeCompanies;
        private static List<Room> xiala = new List<Room>();
        private static List<Room> buzhu = new List<Room>();
        private static List<Room> qianF = new List<Room>();

      

        // GET: RoomPattern
        public ActionResult Index(long buildId = 0, long floorId = 0, long roomTypeId = 0, int roomState = 0)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            ViewBag.Param = ParamBll.GetBy(hotelId);
            RoomNew = new List<Room>();
            roomSet = RoomSetBll.GetBy(hotelId);
            if (roomSet == null)
                roomSet = RoomSetBll.GetBy(0);

            PreLoadFTData();

            var roomTypeList = _ft_RoomType;
            var isLinc = false;
            if (roomTypeList[0].IsUseLC == true)
            {
                var timer = int.Parse(DateTime.Now.Hour.ToString());
                var RoomGlobal = _ft_GlobalFeeSet;
                var LCStartFeePoint = RoomGlobal.LCStartFeePoint;
                var LCEndFeePoint = RoomGlobal.LCEndFeePoint;
                if (timer >= LCStartFeePoint && timer < LCEndFeePoint)
                {
                    isLinc = true;
                }

            } 

            var bf= BuildFloorBll.GetAllBuild(hotelId);
            ViewBag.BuildFloors = bf; 
            ViewBag.Builds = bf.Where(a => a.Pid == 0).ToList();

        

            models = _ft_rooms;

            roomRegs = _ft_roomregs; 

              
            ViewBag.IsLinc = isLinc;
            ViewBag.HotelId = hotelId;
            ViewBag.RoomSet = roomSet;
            ViewBag.RoomTypeList = roomTypeList;
            ViewBag.RoomregList = roomRegs;
            ViewBag.RoomQianf = qianF;
            ViewBag.RoomBu = buzhu;

            var today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var simpleModels = new List<object>();
            var SimpleRoomTypeList = new List<object>();
            foreach(var item in roomTypeList)
            {
                SimpleRoomTypeList.Add(new
                {
                    item.Id,
                    item.Name
                });
            }
            ViewBag.SimpleRoomTypeList = SimpleRoomTypeList;
            foreach (var item in models)
            {
                item.RoomReg = roomRegs.Where(a => a.Id == item.RoomRegId).FirstOrDefault();
                LoadRoomInfo(item);

                simpleModels.Add(RoomToSRoom(item));
            }
            ViewBag.SimpleModels = simpleModels; 


            models = models.OrderBy(a => a.RoomNO).ToList();
            ViewBag.Count = models.Count();
            //MessageBox(models.ToString());
            return View(models);
        }

        

        private object RoomToSRoom(Room item)
        {
            return new
            {
                Id = item.Id.ToString(),
                item.RoomNO,
                RoomRegId = item.RoomRegId.ToString(),
                ZfDjId = item.RoomReg == null ? "0" : item.RoomReg.ZfDjId.ToString(),
                item.FjState,
                item.FjStateName,
                item.SimpleJson,
                item.Price,
                item.RoomTypeName,
                item.RoomTypeId,  
                BuildId = item.BuildId.ToString(),
                FloorId = item.FloorId.ToString()
            };
        }


        /// <summary>
        /// 加载 房间相关信息
        /// </summary>
        /// <param name="item"></param>
        private void LoadRoomInfo(Room item)
        {
            var hotelId = UserContext.CurrentUser.HotelId;

            var now = DateTime.Now;

            var setting = _ft_GlobalFeeSet;
            if (!_ft_isLoaded)
            {
                setting = GlobalFeeSetBll.GetByHotelId(hotelId);
            }
            var today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var rz = new RoomRegZwController();
            RoomSet roomSet = _ft_RoomSet;
            if (!_ft_isLoaded)
            {
                roomSet = RoomSetBll.GetBy(hotelId);
                if (roomSet == null)
                    roomSet = RoomSetBll.GetBy(0);
            }


          

            item.IsBuZu = false;
            item.IsQianfei = false;
            item.IsLC = false;
            item.HasMaterialLease = false;
            item.GuestInfo = "";
            item.GuestNames = new List<string>();
            item.GuestCertificateNOs = new List<string>();



            item.MemberInfo = "";
            item.SalePrice2 = item.Price;
            double stest1, stest2, stest3, stest4, stest5, stest6;
            stest1 = (DateTime.Now - now).TotalMilliseconds;
            if (item.RoomReg != null)
            {
                item.SalePrice2 = item.RoomReg.SalePrice;

                if (item.RoomReg.CustomerType == 3 && item.RoomReg.MemCompId != null)
                {
                    var memeber = _ft_members.FirstOrDefault(a => a.Id == Convert.ToInt64(item.RoomReg.MemCompId));
                    var memberType = _ft_membersType.FirstOrDefault(a => a.Id == memeber.MemberTypeId);

                    if (!_ft_isLoaded)
                    {
                        memeber = MemberBll.GetById(Convert.ToInt64(item.RoomReg.MemCompId));
                        memberType = MemberTypeBll.GetById(memeber.MemberTypeId);
                    }
                    item.MemberInfo = "会员卡号:" + item.RoomReg.MemberCardNO + "  类型：" + memberType.Name;
                    item.MemberInfo += "<br >";
                    if (memberType.IsJFK)
                    {
                        item.MemberInfo += "剩下积分：" + item.RoomReg.MemberExp;
                    }

                    if (memberType.IsCZK)
                    {
                        item.MemberInfo += " 储值余额：" + item.RoomReg.MemberMoney + "账号余额： " + (item.RoomReg.MemberMoney - item.RoomReg.Money);
                    }

                }
                stest2 = (DateTime.Now - now).TotalMilliseconds;
                var guestdatas = _ft_RoomRegGuestInfoCN.Where(a => a.RoomRegId == item.RoomReg.Id).ToList();

                if (!_ft_isLoaded)
                {
                    guestdatas = RoomRegGuestInfoCNBll.GetList(item.RoomReg.Id);
                }

                var guestInfos = new List<string>();
                foreach (var gd in guestdatas)
                {
                    if (string.IsNullOrEmpty(gd.Name)) continue;
                    if (gd.CertificateTypeName == "请选择证件类型") gd.CertificateTypeName = "";
                    if (!string.IsNullOrEmpty(gd.Address)) gd.Address = "<br />地址:" + gd.Address;

                    if (string.IsNullOrEmpty(item.FirstGuestName))
                    {
                        item.FirstGuestName = gd.Name;
                    }
                    if (string.IsNullOrEmpty(item.FirstGuestSex))
                    {
                        item.FirstGuestSex = gd.Sex;
                    }

                    item.GuestNames.Add(gd.Name);
                    item.GuestCertificateNOs.Add(gd.CertificateNO);


                    guestInfos.Add($"{gd.Name} {gd.Sex} {gd.CertificateTypeName} {gd.CertificateNO} {gd.Address}");
                }

                var guestdatas2 = _ft_RoomRegGuestInfoEN.Where(a => a.RoomRegId == item.RoomReg.Id).ToList();

                if (!_ft_isLoaded)
                {
                    guestdatas2 = RoomRegGuestInfoENBll.GetList(item.RoomReg.Id);
                }
                stest3 = (DateTime.Now - now).TotalMilliseconds;
                foreach (var gd in guestdatas2)
                {
                    if (string.IsNullOrEmpty(gd.Name)) continue;
                    if (gd.CertificateTypeName == "请选择证件类型") gd.CertificateTypeName = "";

                    if (string.IsNullOrEmpty(item.FirstGuestName))
                    {
                        item.FirstGuestName = gd.Name;
                    }
                    if (string.IsNullOrEmpty(item.FirstGuestSex))
                    {
                        item.FirstGuestSex = gd.Sex;
                    }

                    item.GuestNames.Add(gd.Name);
                    item.GuestCertificateNOs.Add(gd.CertificateNO);

                    guestInfos.Add($"{gd.Name} {gd.Sex} {gd.CertificateTypeName} {gd.CertificateNO}");
                }

                item.GuestInfo = string.Join("<br />", guestInfos.ToArray());

                var regTime = TypeConvert.IntToDateTime(item.RoomReg.RegTime);
                item.IsLC = regTime.Hour >= setting.LCStartFeePoint && regTime.Hour <= setting.LCEndFeePoint;
                stest4 = (DateTime.Now - now).TotalMilliseconds;
                var mls = _ft_MaterialLease.Where(a => a.RoomRegId == item.RoomReg.Id && a.State == 0).ToList();

                if (!_ft_isLoaded)
                {
                    mls = MaterialLeaseBll.GetList(item.RoomReg.Id, 0);//未还的租赁物品
                }
                if (mls.Any())
                {
                    item.HasMaterialLease = true;
                }
                var lf_rooms = _ft_roomregs.Where(a => a.ZfDjId == item.RoomReg.ZfDjId && (a.FjState == FjStateEnum.在住房 || a.FjState == FjStateEnum.脏住房)).ToList();

                if (!_ft_isLoaded)
                {
                    lf_rooms = RoomRegBll.GetLfList2(item.RoomReg.Id, hotelId);
                }
                item.LFRoomNos = lf_rooms.Select(a => a.RoomNO).ToArray();
                decimal jy = 0;

                if (!_ft_isLoaded)
                { 
                    var zzw = rz.getthis(item.RoomReg.Id);
                    jy = zzw.jy;
                }
                else
                {
                    foreach(var o in lf_rooms)
                    {
                        jy += o.Jy;
                    }
                }
                if (roomSet.IsShowWhenDepositNotEnoughType1)
                {
                    if (jy < item.RoomReg.SalePrice * roomSet.DepositNotEnoughType1Value && item.RoomReg.SalePrice != 0)
                    {
                        item.IsBuZu = true;
                    }
                }
                else if (roomSet.IsShowWhenDepositNotEnoughType2)
                {
                    if (jy < roomSet.DepositNotEnoughType2Value && item.RoomReg.SalePrice != 0)
                    {
                        item.IsBuZu = true;
                    }
                }
                if (jy < 0 && item.RoomReg.SalePrice != 0)
                {
                    item.IsQianfei = true;
                }

            }
            if (item.FjState == FjStateEnum.维修房)
            {
                var mr = _ft_MaintainRoom.FirstOrDefault(a => a.RoomId == item.Id);
                if (!_ft_isLoaded)
                {
                    mr = MaintainRoomBll.GetByRoomId(item.Id); 
                } 
                item.MaintainRoom = mr;
            }
            if (item.FjState == FjStateEnum.自用房)
            {
                var mr = _ft_RoomSelfuse.FirstOrDefault(a => a.RoomId == item.Id);
                if (!_ft_isLoaded)
                {
                    mr = RoomSelfBll.GetByRoomId(item.Id);
                } 
                item.RoomSelfuse = mr;
            }
            if (item.RoomTypeId != 0)
            {

                var roomType = _ft_RoomType.FirstOrDefault(a => a.Id == item.RoomTypeId);

                if (!_ft_isLoaded)
                {
                    roomType = RoomTypeBll.GetById(item.RoomTypeId);
                }
                if (roomType != null)
                {
                    item.RoomType = roomType;
                    item.Price = roomType.Price;
                    if(item.RoomReg == null)
                    {
                        item.SalePrice2 = item.Price;
                    } 
                    item.RoomTypeName = roomType.Name;
                    item.ManNumber = roomType.ManNumer;
                    item.BedCount = roomType.BedCount;
                }
            }
            stest5 = (DateTime.Now - now).TotalMilliseconds;
            var roomydRecordList = _ft_RoomYdRecord.Where(a => a.RoomId == item.Id && !a.Sfgq).ToList();

            if (!_ft_isLoaded)
            {
                roomydRecordList = RoomYdRecordBll.GetList(item.Id); //预定
            }
            foreach (var record in roomydRecordList)
            {
                //这里加载 房间 预定状态 和预定 入住剩下天数
                var date = TypeConvert.IntToDateTime(record.YudaoTime);
                if (date > today)
                {
                    item.IsYD = true;

                    DateTime end = Convert.ToDateTime(date.ToShortDateString());
                    TimeSpan sp = end.Subtract(today);
                    int days = sp.Days;
                    if (days == 0) item.YDDays = "";
                    else item.YDDays = days.ToString();


                    item.RoomYd = _ft_RoomYD.FirstOrDefault(a => a.Id == record.YdId);

                    if (!_ft_isLoaded)
                    {
                        item.RoomYd = RoomYdBll.GetById(record.YdId);
                    }
                   
                    item.RoomYdRecord = record;
                }
            }

            stest6 = (DateTime.Now - now).TotalMilliseconds;


        }


        private void LoadRoomInfo2(Room item)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var setting = GlobalFeeSetBll.GetByHotelId(hotelId);
            var today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var rz = new RoomRegZwController();
            var roomSet = RoomSetBll.GetBy(hotelId);
            if (roomSet == null)
                roomSet = RoomSetBll.GetBy(0);
            item.IsBuZu = false;
            item.IsQianfei = false;
            item.IsLC = false;
            item.HasMaterialLease = false;
            item.GuestInfo = "";
            item.GuestNames = new List<string>();
            item.GuestCertificateNOs = new List<string>();



            item.MemberInfo = "";

            if (item.RoomReg != null)
            {
               
                var regTime = TypeConvert.IntToDateTime(item.RoomReg.RegTime);
                item.IsLC = regTime.Hour >= setting.LCStartFeePoint && regTime.Hour <= setting.LCEndFeePoint;

                var mls = MaterialLeaseBll.GetList(item.RoomReg.Id, 0);//未还的租赁物品
                if (mls.Any())
                {
                    item.HasMaterialLease = true;
                }
                

            }
            
            if (item.RoomTypeId != 0)
            {

                var roomType = RoomTypeBll.GetById(item.RoomTypeId);
                if (roomType != null)
                {
                    item.RoomType = roomType;
                    item.Price = roomType.Price;
                    item.RoomTypeName = roomType.Name;
                    item.ManNumber = roomType.ManNumer;
                    item.BedCount = roomType.BedCount;
                }
            }

            if(item.FloorId != 0)
            {
                var floor = BuildFloorBll.GetById(item.FloorId);
                item.FloorName = floor.Name;
            }


            var roomydRecordList = RoomYdRecordBll.GetList(item.Id); //预定
            foreach (var record in roomydRecordList)
            {
                //这里加载 房间 预定状态 和预定 入住剩下天数
                var date = TypeConvert.IntToDateTime(record.YudaoTime);
                if (date > today)
                {
                    item.IsYD = true; 
                }
            }
        }
        public JsonResult GetRoom(int tianjian)
        {
            var hotelId = UserContext.CurrentUser.HotelId;
            var today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var rz = new RoomRegZwController();
            var roomSet = RoomSetBll.GetBy(hotelId);
            if (roomSet == null)
                roomSet = RoomSetBll.GetBy(0);

            List<Room> thisRoom = new List<Room>();
            if (tianjian == 0)//全部
            {
                thisRoom = xiala;
            }
            else if (tianjian == 1)//全天房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 1))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 2)//预定房
            {
                thisRoom = xiala.FindAll(a => a.FjState == (FjStateEnum)1);
            }
            else if (tianjian == 3)//是否为免费房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 5))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 4)//干净房
            {
                thisRoom = xiala.FindAll(a => a.FjState == 0);
            }
            else if (tianjian == 5)//脏房
            {
                thisRoom = xiala.FindAll(a => a.FjState == (FjStateEnum)4);
            }
            else if (tianjian == 6)//是否为长包房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 3))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 7)//维修房
            {
                thisRoom = xiala.FindAll(a => a.FjState == (FjStateEnum)5);
            }
            else if (tianjian == 8)//自用房
            {
                thisRoom = xiala.FindAll(a => a.FjState == (FjStateEnum)6);
            }
            else if (tianjian == 9)//联房
            {
                foreach (var item in roomRegs)
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && item.RType == 2)
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 11)//保密
            {
                foreach (var item in roomRegs)
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && item.IsSecrecy)
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 12)//协议
            {
                foreach (var item in roomRegs)
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && item.CustomerType == 4)
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if(tianjian == 13) //余额不足
            {
                foreach (var item in roomRegs)
                {
                    var room = xiala.FirstOrDefault(a => a.RoomRegId == item.Id);
                    if(room != null)
                    {
                        var zzw = rz.getthis(item.Id);//获取财务信息
                        if (roomSet.IsShowWhenDepositNotEnoughType1)
                        {
                            if (zzw.jy < item.RoomPrice * roomSet.DepositNotEnoughType1Value)
                            {
                                thisRoom.Add(room);
                            }
                        }
                        else if (roomSet.IsShowWhenDepositNotEnoughType2)
                        {
                            if (zzw.jy < roomSet.DepositNotEnoughType2Value)
                            {
                                thisRoom.Add(room);
                            }
                        } 
                    }
                } 
            }
            else if (tianjian == 15) //欠费
            {
                foreach (var item in roomRegs)
                {
                    var room = xiala.FirstOrDefault(a => a.RoomRegId == item.Id);
                    if (room != null)
                    {
                        var zzw = rz.getthis(item.Id); 
                        if (zzw.jy < 0)
                        {
                            thisRoom.Add(room);
                        }
                    }
                }
            } 
            else if (tianjian == 16)//会员
            {
                foreach (var item in roomRegs)
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && item.CustomerType == 3)
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 17)//当天预离
            { 
                 
                foreach (var item in roomRegs)
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjState == FjStateEnum.在住房 && i.RoomReg.LeaveTime != 0)
                        {
                            var date = TypeConvert.IntToDateTime(i.RoomReg.LeaveTime); 
                            if(DateTime.Now.ToShortDateString() == date.ToShortDateString())
                            {
                                thisRoom.Add(xiala.Find(a => a.Id == item.RoomId)); 
                            }
                        }
                    }
                }
            }
            else if (tianjian == 21)//贵宾
            {
                foreach (var item in roomRegs)
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && item.VipLevel != 0)
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            else if (tianjian == 19)//脏住房
            {
                thisRoom = xiala.FindAll(a => a.FjState == (FjStateEnum)3);
            }

            else if (tianjian == 23)//是否为钟点房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 2))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }
            }
            RoomNew = thisRoom;

            return Json(new { content = GetRoomId() }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetKaiFangFangShi(string tianjian)
        {
            string kaifangfangshi = "";
            foreach (var item in roomRegs)
            {
                foreach (var i in xiala)
                {

                    if (item.Id == i.RoomRegId && i.FjStateName == "在住房" && i.Id == long.Parse(tianjian))
                    {
                        kaifangfangshi = item.KaiFangFangShiName;
                    }
                }
            }

            return Json(new { content = kaifangfangshi }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 下拉框搜索
        /// </summary>
        /// <param name="Build"></param>
        /// <param name="floor"></param>
        /// <param name="RoomType"></param>
        /// <param name="RoomState"></param>
        /// <returns></returns>
        public JsonResult GetSouSuo(long Build, long floor)
        {

            List<Room> thisRoom = new List<Room>();

            //models.FindAll(a=>)

            if (Build == 0)
            {
                if (floor == 0)
                {
                    thisRoom = models;
                }
                else
                {

                    thisRoom = models.FindAll(a => a.FloorId == floor).ToList();

                }
            }
            else
            {

                if (floor == 0)
                {
                    thisRoom = models.FindAll(a => a.BuildId == Build);
                }
                else
                {

                    thisRoom = models.FindAll(a => a.FloorId == floor && a.BuildId == Build).ToList();
                }

            }


            int state0 = 0;//干净房
            int state1 = 0;//预订房
            int state2 = 0;//脏住房

            int state3 = 0;//在住房

            int state4 = 0;//脏房
            int state5 = 0;//维修房
            int state6 = 0;//自用房
            int state7 = 0;//钟点房
            int state8 = 0;//免费房
            int state9 = 0;//长包房

            int state10 = 0;//时段房

            int state11 = 0;//全天房
            int state12 = 0;//联房
            int state13 = 0;//续住
            int state14 = 0;//保密
            int state15 = 0;//协议
            int state16 = 0;//不足
            int state17 = 0;//团队
            int state18 = 0;//欠费
            int state19 = 0;//会员
            int state20 = 0;//预离
            int state21 = 0;//锁房
            int state22 = 0;//凌晨
            int state23 = 0;//贵宾
            int state24 = 0;//换房
            int state25 = 0;//当日续住
            int state26 = 0;//当日入住

            xiala = thisRoom;
            foreach (var item in thisRoom)
            {
                if (item.FjStateName == "干净房")
                {
                    state0 += 1;
                }
                if (item.FjStateName == "预定房")
                {
                    state1 += 1;
                }
                if (item.FjStateName == "脏住房")
                {
                    state2 += 1;
                }
                if (item.FjStateName == "在住房")
                {
                    state3 += 1;
                }
                if (item.FjStateName == "脏房")
                {
                    state4 += 1;
                }
                if (item.FjStateName == "维修房")
                {
                    state5 += 1;
                }
                if (item.FjStateName == "自用房")
                {
                    state6 += 1;
                }
            }
            foreach (var item in roomRegs)
            {
                foreach (var i in thisRoom)
                {
                    if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                    {
                        if (item.FjStateName == "在住房" && item.KaiFangFangShiName == "全天房")
                        {
                            state11 += 1;
                        }
                        if (item.FjStateName == "在住房" && item.KaiFangFangShiName == "钟点房")
                        {
                            state7 += 1;
                        }
                        if (item.FjStateName == "在住房" && item.KaiFangFangShiName == "免费房")
                        {
                            state8 += 1;
                        }
                        if (item.FjStateName == "在住房" && item.KaiFangFangShiName == "长包房")
                        {
                            state9 += 1;
                        }
                        if (item.FjStateName == "在住房" && item.KaiFangFangShiName == "时段房")
                        {
                            state10 += 1;
                        }
                    }
                    if (item.Id == i.RoomRegId && item.RType == 2)//联房
                    {
                        state12 += 1;
                    }
                    if (item.Id == i.RoomRegId && item.CustomerType == 4)//协议
                    {
                        state15 += 1;
                    }
                    if (item.Id == i.RoomRegId && item.CustomerType == 3)//会员
                    {
                        state19 += 1;
                    }
                    if (item.Id == i.RoomRegId && item.VipLevel != 0)//贵宾
                    {
                        state23 += 1;
                    }
                }

            }
            ViewBag.Count = thisRoom.Count();
            ViewBag.state0 = state0;
            ViewBag.state1 = state1;
            ViewBag.state11 = state11;
            ViewBag.state4 = state4;
            ViewBag.state5 = state5;
            ViewBag.state6 = state6;
            ViewBag.state7 = state7;
            ViewBag.state8 = state8;
            ViewBag.state9 = state9;
            ViewBag.state2 = state2;
            ViewBag.state12 = state12;
            ViewBag.state15 = state15;
            ViewBag.state19 = state19;
            ViewBag.state23 = state23;

            List<int> s = new List<int>();
            s.Add(ViewBag.Count);
            s.Add(ViewBag.state11);
            s.Add(ViewBag.state1);
            s.Add(ViewBag.state8);
            s.Add(ViewBag.state0);
            s.Add(ViewBag.state4);
            s.Add(ViewBag.state9);
            s.Add(ViewBag.state5);
            s.Add(ViewBag.state6);
            s.Add(ViewBag.state7);
            s.Add(ViewBag.state2);
            s.Add(ViewBag.state12);
            s.Add(ViewBag.state15);
            s.Add(ViewBag.state19);
            s.Add(ViewBag.state23);
            return Json(new { content = GetRoomId(xiala), Num = s }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 图标点击搜索
        /// </summary>
        /// <param name="roomIcon"></param>
        /// <param name="addOrRem"></param>
        /// <returns></returns>
        public JsonResult GetIconRoom(int addOrRwm, string roomIcon)
        {


            List<Room> thisRoom = new List<Room>();

            switch (roomIcon)
            {
                case "lianfang":
                    var id = roomRegs.Where(x => x.RType == 2 && x.IsAlreadyZrz).Select(y => y.RoomId).ToString();
                    if (id == "")
                        thisRoom = xiala.FindAll(a => a.Id == long.Parse(id));

                    break;
                case "xuzhu":
                    //RoomNew = RoomNew.Union(models.FindAll(a => a.Id == long.Parse(roomRegs.Select(y => y.RoomId).ToString()))).ToList();
                    //暂无
                    break;
                case "baomi":

                    //前台是否可以查询  协议单位才有
                    //long.Parse(roomRegs.Where(x => x.AgreeCompanyName == agreeCompanies.Where(y =>   y.IsAllowQtSearch).Select(z => z.Name).ToString()).Select(i => i.RoomId).ToString())
                    thisRoom = xiala.FindAll(a => a.Id == 0);
                    break;
                case "xieyi":

                    //是否为协议单位
                    thisRoom = xiala.FindAll(a => a.Id == long.Parse("1")).ToList();
                    break;
                case "buzu":

                    thisRoom = buzhu;

                    break;
                case "tuandui":
                    //暂不不用
                    break;
                case "qianfei":
                    //欠费
                    thisRoom = qianF;
                    break;
                case "yuli":
                    //当天退房的
                    var day = DateTime.Now.ToString("yyy-MM-dd");
                    var yuli = from roomReg in roomRegs where TypeConvert.IntToDateTime(roomReg.OutTime).ToString("yyy-MM-dd") == day select roomReg.RoomId;
                    foreach (var item in yuli)
                    {
                        thisRoom.Add(xiala.Find(a => a.Id == item));
                    }

                    break;
                case "suofang":

                    thisRoom = xiala.FindAll(a => a.IsLock == 1).ToList();

                    break;
                case "zangfang":
                    thisRoom = xiala.FindAll(a => a.FjState == FjStateEnum.脏房).ToList();

                    break;
                case "lingchenfang":
                    var linc = from hh in roomRegs where hh.KaiFangFangShi == 4 select hh.RoomId;
                    foreach (var item in linc)
                    {
                        thisRoom.Add(xiala.Find(a => a.Id == item));
                    }

                    break;
                case "vip":
                    //thisRoom = xiala.FindAll(a => a.Id == long.Parse(roomRegs.Where(x => x.VipLevel > 0).Select(y => y.RoomId).ToString())).ToList();
                    thisRoom = xiala.FindAll(a => a.Id == long.Parse("0")).ToList();
                    break;
                case "huanfang":
                    //暂时没有

                    break;
                case "danri":
                    break;

                default:
                    break;
            }



            AddAndRom(addOrRwm, thisRoom);


            return Json(new { content = GetRoomId() }, JsonRequestBehavior.AllowGet);
        }


        public List<string> GetRoomId(List<Room> thisRoom)
        {

            List<string> need = new List<string>();
            foreach (var item in thisRoom)
            {
                need.Add(item.Id.ToString());
            }
            return need;

        }

        public List<string> GetRoomId()
        {

            List<string> need = new List<string>();
            foreach (var item in RoomNew)
            {
                if (item != null)
                {
                    need.Add(item.Id.ToString());
                }

            }
            return need;

        }
        private void AddAndRom(int addOrRwm, List<Room> thisRoom)
        {

            if (addOrRwm == 1)
            {
                RoomNew = RoomNew.Union(thisRoom).ToList();
            }
            else
            {
                thisRoom.ForEach(delegate (Room room) { RoomNew.Remove(room); });
            }

        }
        

        /// <summary>
        /// 房态搜索
        /// </summary>
        /// <param name="addOrRwm"></param>
        /// <param name="RoomState"></param>
        /// <returns></returns>
        public JsonResult GetTjRoom(int addOrRwm, int RoomState)
        {
            List<Room> thisRoom = new List<Room>();
            if (RoomState == 11)//是否为长包房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 3))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }

            }
            else if (RoomState == 12)//是否为免费房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 5))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }

            }
            else if (RoomState == 13)//是否为全天房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 1))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }

                }

            }
            else if (RoomState == 14)//是否为钟点房
            {
                foreach (var item in roomRegs.Where(a => a.KaiFangFangShi == 2))
                {
                    foreach (var i in xiala)
                    {
                        if (item.Id == i.RoomRegId && i.FjStateName == "在住房")
                        {
                            thisRoom.Add(xiala.Find(a => a.Id == item.RoomId));
                        }
                    }
                }

            }
            else if (RoomState == 10)//全部
            {
                thisRoom = xiala;
            }
            else
            {
                thisRoom = xiala.FindAll(a => a.FjState == (FjStateEnum)RoomState);
            }
            AddAndRom(addOrRwm, thisRoom);
            return Json(new { content = GetRoomId() }, JsonRequestBehavior.AllowGet);


        }


        public JsonResult SetSession(string state)
        {
            Session["state"] = state;
            return Json("");
        }

        public ActionResult SuoF()
        {
            if (Request.QueryString["roomId"] != null)
            {
                var roomId = Request.QueryString["roomId"];
                var Room = RoomBll.GetById(long.Parse(roomId));
                if (Room == null)
                {
                    Room = new Room();

                }

       
           

                ViewBag.room = Room;
            }
            return View();
        }


        public ActionResult OneselfRoom(long Id = 0)
        {

            HttpCookie TadyNum = new HttpCookie("TadyNum");
            TadyNum["num"] = "0";
            Response.Cookies.Add(TadyNum);
            var now = DateTime.Now;
            DateTime yuLiTime = now;
            yuLiTime = now.Date.AddDays(1).AddHours(12);
            ViewBag.BeginTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.EndTime = yuLiTime.ToString("yyyy-MM-dd HH:mm:ss");

            if(Id != 0)
            {
                var SelfRoom = RoomSelfBll.GetById(Id);
                var Room = RoomBll.GetById(SelfRoom.RoomId);
                ViewBag.BeginTime = SelfRoom.StartDate;
                ViewBag.EndTime = SelfRoom.YEndDate;
                if (Room == null)
                {
                    Room = new Room(); 
                }
                if (SelfRoom == null)
                {
                    SelfRoom = new RoomSelfuse();
                }
                else
                {
                    ViewBag.RoomSelfuseId = SelfRoom.Id;
                }
                ViewBag.room = Room;
                ViewBag.roomId = SelfRoom.RoomId;
                ViewBag.room_self = SelfRoom;


                if (!string.IsNullOrEmpty(SelfRoom.EndTime))
                {
                    ViewBag.IsEnd = "Y";
                }
            }
            else if (Request.QueryString["roomId"] != null && Request.QueryString["roomId"] != "0")
            {
                var roomId = Request.QueryString["roomId"];

                var Room = RoomBll.GetById(long.Parse(roomId));
                var SelfRoom = RoomSelfBll.GetByRoomId(long.Parse(roomId));
              
                if (Room == null)
                {
                    Room = new Room();

                }
                if (Room.FjState != FjStateEnum.自用房)
                {
                    SelfRoom = new RoomSelfuse();
                }
                if (SelfRoom == null)
                {
                    SelfRoom = new RoomSelfuse();
                }else
                {
                    ViewBag.RoomSelfuseId = SelfRoom.Id;
                }
                ViewBag.room = Room;
                ViewBag.roomId = roomId;
                ViewBag.room_self = SelfRoom; 

            }
            else
            {
                ViewBag.room = new Room();
                ViewBag.room_self = new RoomSelfuse();

                ViewBag.roomId = 0;
            }

            return View(UserContext.CurrentUser);
        }

        public JsonResult SeekRoom(string roomTj)
        {

            // 房间号，客人名字，身份证号，手机号，会员卡号，车牌号
            var hotelId = UserContext.CurrentUser.HotelId;
            var models = RoomPatternBll.GetList(hotelId, 0, 0, 0, 0);
            List<string> roomIds = new List<string>();

            foreach (var item in models.FindAll(a => a.RoomNO == roomTj))
            {
                roomIds.Add(item.Id.ToString());
            }
            foreach (var item in RoomRegBll.GetRoomNoByName(roomTj))
            {
                roomIds.Add(item.RoomId.ToString());
            }
            RoomNew = new List<Room>();

            return Json(new { content = roomIds }, JsonRequestBehavior.AllowGet);

        }

   

        /// <summary>
        /// 设置为自用房
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="addTimer"></param>
        /// <param name="finshTimer"></param>
        /// <param name="addUser"></param>
        /// <param name="bZ"></param>
        /// <returns></returns>
        public JsonResult SetOnselfRoom(long roomId, string addTimer, string finshTimer,  string bZ, string zyR, string Danju,  long id = 0)
        {

            var apiResult = new APIResult();
            var tadyNo = DateTime.Now.ToString("yyyyMMdd ", DateTimeFormatInfo.InvariantInfo);
            try
            {
                var addUser = UserContext.CurrentUser.UserName;
                RoomBll.SetMySelfRoom(roomId, tadyNo, addTimer, finshTimer, addUser, bZ, zyR, Danju, id);
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
        /// 设置为锁房
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public JsonResult SetToSuo(long roomId, int Lock, string LockBz)
        {
             
            var apiResult = new APIResult();
            try
            {
                RoomBll.SetToSuo(roomId, Lock, LockBz);
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


        public ActionResult MaintainRoom(string roomId,long Id = 0)
        {
            HttpCookie TadyNum = new HttpCookie("TadyNum");
            TadyNum["num"] = "0";
            Response.Cookies.Add(TadyNum);
            var now = DateTime.Now;
            DateTime yuLiTime = now;
            yuLiTime = now.Date.AddDays(1).AddHours(12);
            ViewBag.BeginTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.EndTime = yuLiTime.ToString("yyyy-MM-dd HH:mm:ss");

            if(Id != 0)
            {
                var model = MaintainRoomBll.GetById(Id);
                ViewBag.BeginTime = model.StatrTimer;
                ViewBag.EndTime = model.YjEndTimer;
                var Room = RoomBll.GetById(model.RoomId);
                if (Room == null)
                {
                    Room = new Room();
                }
                ViewBag.room = Room;
                ViewBag.room_Maintain = model;
                ViewBag.MaintainRoomId = model.Id;
                ViewBag.RoomId = model.RoomId;
                if (!string.IsNullOrEmpty(model.EndTime))
                {
                    ViewBag.IsEnd = "Y";
                }
            }
            else
            {
                ViewBag.RoomId = roomId;
                if (!string.IsNullOrEmpty(roomId) && roomId != "0")
                {
                    var Room = RoomBll.GetById(long.Parse(roomId));
                    var MaintainRoom = MaintainRoomBll.GetByRoomId(long.Parse(roomId));

                    if(Room.FjState != FjStateEnum.维修房)
                    {
                        MaintainRoom = new MaintainRoom();
                    }
                    if (Room == null)
                    {
                        Room = new Room(); 
                    }
                    if (MaintainRoom == null)
                    {
                        MaintainRoom = new MaintainRoom();
                    }
                    ViewBag.room = Room;
                    ViewBag.room_Maintain = MaintainRoom;
                    ViewBag.MaintainRoomId = MaintainRoom.Id;
                    ViewBag.RoomId = roomId;
                }else
                {
                    var Room = new Room();
                    var MaintainRoom = new MaintainRoom();
                    ViewBag.room = Room;
                    ViewBag.room_Maintain = MaintainRoom;
                    ViewBag.MaintainRoomId = MaintainRoom.Id;
                    ViewBag.RoomId = 0;
                }
            }

          
          

            return View(UserContext.CurrentUser);
        }



        /// <summary>
        /// 审核功能
        /// </summary>
        /// <param name="selfId"></param>
        /// <param name="isA"></param>
        /// <param name="Shr"></param>
        /// <returns></returns>
        public JsonResult AuditOnselfRoom(long selfId, int isA, string Shr)
        {
            var apiResult = new APIResult();
            try
            {
                RoomSelfBll.SetAudit(selfId, isA, Shr);
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
        /// 删除自用房记录
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="SelRoomId"></param>
        /// <returns></returns>
        public JsonResult DelOnselfRoom(long roomId, long SelRoomId)
        {


            var apiResult = new APIResult();
            try
            {
                RoomBll.DelOneRoom(roomId, SelRoomId);
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
        /// 结束自用房
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="SelfRoom"></param>
        /// <returns></returns>
        public JsonResult EndMySelfRoom(long roomId)
        {

            var apiResult = new APIResult();
            try
            {
                RoomBll.EndMySelfRoom(roomId);
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
        /// 重新获取当前的自用房信息
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="SelfRoomId"></param>
        /// <returns></returns>
        public JsonResult GetThisSelfRoom(long roomId )
        {
             
            var SelfRoom = RoomSelfBll.GetByRoomId(roomId);
           

            if (SelfRoom == null)
            {
                SelfRoom = new RoomSelfuse(); 
            }
              

            return Json(SelfRoom);
        }





        private static string rId = string.Empty;
        [HttpGet]
        public ActionResult AlarmClock(string id)
        {
            rId = id;
            long tempId = Convert.ToInt64(id);
            var room = RoomBll.GetById(tempId);

            ViewBag.RoomRegId = room.RoomRegId;


            return View(room);
        }
        /// <summary>
        /// 添加或修改唤醒实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetAlarmClock(AlarmClock entity)
        {
            entity.RoomId = rId;
            var apiResult = new APIResult();
            try
            {
                AlarmClockBll.InsertOrUpdate(entity);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex + entity.ToString());
            }
            return Json(apiResult);
        }
        /// <summary>
        /// 获取唤醒分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPageList(int page, int rows)
        {
            long tempId = Convert.ToInt64(rId);
            var query = AlarmClockBll.GetPager(page, rows, tempId);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除唤醒实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(long[] arr)
        {
            var apiResult = new APIResult();
            try
            {
                foreach (var item in arr)
                {
                    AlarmClockBll.DeleteById(item);
                }
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
        public ActionResult RoomDetails(long id)
        {
            //房间
            var room = new Model.Room();
            //房型
            var room_type = new Model.RoomType();
            //登记
            var room_reg = new Hotel.Model.RoomReg();
            //境内客人
            var guest_info_cn = new List<Model.RoomRegGuestInfoCN>();
            //境外客人
            var guest_info_en = new List<Model.RoomRegGuestInfoEN>();
            if (id != 0)
            {
                room = RoomBll.GetById(id);
                room_type = RoomTypeBll.GetById(room.RoomTypeId);
                //房间登记不能为空
                if (room.RoomRegId != 0)
                {
                    room_reg = RoomRegBll.GetByRoomId(room.Id);
                }
                try
                {
                    if (room_reg == null)
                    {
                        return Json(new { room, room_type, room_reg, guest_info_cn, guest_info_en }, JsonRequestBehavior.AllowGet);
                    }
                    if (room_reg != null || room_reg.Id != 0)
                    {
                        guest_info_cn = RoomRegGuestInfoCNBll.GetList(room_reg.Id);
                        guest_info_en = RoomRegGuestInfoENBll.GetList(room_reg.Id);
                    }
                }
                catch (Exception)
                {
                }
                //if (room_reg != null || room_reg.Id != 0)
                //{

                //}
            }
            return Json(new { room, room_type, room_reg, guest_info_cn, guest_info_en }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 通过房间id查询
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public ActionResult IsLianFan(long roomID)
        {
            var guGuestInfo = GuestInfoENRzRecordBll.GetGuestByRoomid(roomID);
            if (guGuestInfo == null)
            {
                guGuestInfo = new GuestInfoENRzRecord();
            }

            return Json(guGuestInfo);

        }


        public ActionResult GetMaintainRoom(long roomId)
        {

            var data = MaintainRoomBll.GetByRoomId(roomId);

            return Json(data);

        }

        /// <summary>
        /// 添加维修房
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="addTimer"></param>
        /// <param name="finshTimer"></param>
        /// <param name="addUser"></param>
        /// <param name="bZ"></param>
        /// <param name="zyR"></param>
        /// <param name="Danju"></param>
        /// <param name="setDate"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddMaRoom(long roomId, string addTimer, string finshTimer,   string bZ, string zyR, string Danju, string setDate, long id)
        {

            var apiResult = new APIResult();
            var tadyNo = DateTime.Now.ToString("yyyyMMdd ", DateTimeFormatInfo.InvariantInfo);
            try
            {
                var addUser = UserContext.CurrentUser.UserName;
                RoomBll.AddMaRoom(roomId, tadyNo, addTimer, finshTimer, addUser, bZ, zyR, Danju, id);

                var ma = MaintainRoomBll.GetByRoomId(roomId);
                if(ma != null)
                {
                    apiResult.SeqId = ma.Id;
                }
              
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
        /// 审核维修房
        /// </summary>
        /// <param name="selfId"></param>
        /// <param name="isA"></param>
        /// <param name="Shr"></param>
        /// <returns></returns>
        public JsonResult AuditMafRoom(long selfId, int isA, string Shr)
        {
            var apiResult = new APIResult();
            try
            {
                MaintainRoomBll.SetAudit(selfId, isA, Shr);
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

        public JsonResult DelMaRoom(long roomId)
        {
            var apiResult = new APIResult();
            try
            {
                MaintainRoomBll.DelMaRoom(roomId);
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


        public JsonResult DelOnslfRoom(long roomId)
        {
            var apiResult = new APIResult();
            try
            {
                RoomSelfBll.DelOnslfRoom(roomId);
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
        /// 结束维修房
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="SelfRoomId"></param>
        /// <returns></returns>
        /// 
        public JsonResult EndMaRoom(long roomId)
        {

            var apiResult = new APIResult();
            try
            {
                RoomBll.EndMaRoom(roomId);
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