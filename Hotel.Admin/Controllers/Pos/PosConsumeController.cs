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
    public partial class PosConsumeController : AdminBaseController
    {
        public string GetHotelPagerForZf(int page, int rows,   string searchName = null)
        {
            var pager = RoomRegBll.GetPagerForZf(page, rows, UserContext.CurrentUser.HotelId, -2, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        public ActionResult _BfjzJzmx()
        {
            return View();
        }
        public ActionResult _ShiftDetails(long posShiftId)
        {
            ViewBag.PosShiftId = posShiftId;
            ViewBag.PayTypeList = PayTypeBll.GetList(UserContext.CurrentUser.HotelId, true, true);
            return View();
        }
        public ActionResult _Jz(long consumeId, int jstype)
        {
            var user = UserContext.CurrentUser;
            var consume = PosConsumeBll.GetById(consumeId);

            var details_list = PosConsumeDetailBll.GetList(consumeId);
            details_list = details_list.Where(a => a.ToRoomRegId == 0 && a.SourceRoomRegId == 0).ToList(); 
            ViewBag.WjsmxList = details_list;

            var roomRegId = consume.RoomRegId;
            var roomReg = new Model.RoomReg() { Id = consume.RoomRegId, HotelId = user.HotelId };
            if(consume.RoomRegId != 0)
            {
                roomReg = RoomRegBll.GetById(consume.RoomRegId);
                if(roomReg == null) roomReg = new Model.RoomReg() { Id = consume.RoomRegId, HotelId = user.HotelId };
            }
            if (roomReg.CustomerType == 2)
            {
                //如果是连房成员，则需要获取主房登记id
                var zf = RoomRegBll.GetById(roomReg.ZfDjId);
                if (zf.CustomerType == 3)
                {
                    //主房如果是酒店会员
                    var member = MemberBll.GetById(long.Parse(zf.MemCompId));
                    if (member == null)
                        member = new Member();
                    ViewBag.CustomerType = new CustomerTypeHelp { Type = 3, Data = JsonConvert.SerializeObject(member) };
                }
                else if (zf.CustomerType == 4)
                {
                    //主房如果是协议单位
                    var comAgree = AgreeCompanyBll.GetById(zf.MemCompId);
                    if (comAgree == null)
                        comAgree = new AgreeCompany();
                    ViewBag.CustomerType = new CustomerTypeHelp { Type = 4, Data = JsonConvert.SerializeObject(comAgree) };
                }
                else
                {
                    ViewBag.CustomerType = new CustomerTypeHelp { Type = 1 };
                }
            }
            else if (roomReg.CustomerType == 3)
            {
                //如果是酒店会员
                var member = MemberBll.GetById(long.Parse(roomReg.MemCompId));
                if (member == null)
                    member = new Member();
                ViewBag.CustomerType = new CustomerTypeHelp { Type = 3, Data = JsonConvert.SerializeObject(member) };
            }
            else if (roomReg.CustomerType == 4)
            {
                //如果是协议单位
                var comAgree = AgreeCompanyBll.GetById(roomReg.MemCompId);
                if (comAgree == null)
                    comAgree = new AgreeCompany();
                ViewBag.CustomerType = new CustomerTypeHelp { Type = 4, Data = JsonConvert.SerializeObject(comAgree) };
            }
            else
            {
                ViewBag.CustomerType = new CustomerTypeHelp { Type = 1 };
            }
            //获取支付方式列表
            var payTypeList = PayTypeBll.GetList(user.HotelId, true, true);
            if (payTypeList == null)
                payTypeList = new List<PayType>();
            var payList = new List<RoomRegZwPaytypeHelp>();
            foreach (var item in payTypeList)
            {
                payList.Add(new RoomRegZwPaytypeHelp()
                {
                    Id = item.Id.ToString(),
                    PayId = item.Id.ToString(),
                    PayName = item.Name,
                    Money = 0,
                });
            }
            ViewBag.PayList = payList;
            //获取卡类型
            var cardTypeList = CardTypeBll.GetList(UserContext.CurrentUser.HotelId);
            ViewBag.CardTypeList = cardTypeList;


            //ViewBag.WjsmxList = wjsmxList;
            //获取信用卡预授权
            var xykysqList = RoomRegXykBll.GetYsq(roomRegId);
            if (xykysqList == null || xykysqList.Count == 0)
                xykysqList = new List<RoomRegXyk>();
            ViewBag.Xykysq = xykysqList;
            ViewBag.jstype = jstype;
            return View(consume);
        }
        /// <summary>
        /// 结账撤销
        /// </summary>
        /// <param name="consumeId"></param>
        /// <param name="isCxZt"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JzCx(long consumeId, bool isCxZt, string reason)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                //撤销结账
               PosConsumeBll.JzCx(consumeId, isCxZt, reason, user.HotelId, user.Id, user.Name);
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
        // GET: PosConsume
        public ActionResult OverReport(string id, long posId = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            var o = PosDefineBll.GetByProjectNo(id);
            if (posId != 0 && o == null)
            {
                o = PosDefineBll.GetById(posId);
            }
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = o.Id;

            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PayTypeList = PayTypeBll.GetList(UserContext.CurrentUser.HotelId, true, true);
            return View();
        }
        public ActionResult OverReportView(string id, long posId = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            var o = PosDefineBll.GetByProjectNo(id);
            if (posId != 0 && o == null)
            {
                o = PosDefineBll.GetById(posId);
            }
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = o.Id;

            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PayTypeList = PayTypeBll.GetList(UserContext.CurrentUser.HotelId, true, true);
            return View();
        }

        public ActionResult _AddTempGoods(long posId)
        {
            ViewBag.PosId = posId;
            return View(new PosGoods() { PosId = posId }); 
        }

        public ActionResult Print(string Ids)
        {
            ViewBag.PayTypeList = PayTypeBll.GetList(UserContext.CurrentUser.HotelId, true, true);
            ViewBag.Ids = Ids;
            return View();
        }
        public ActionResult GetData(long id)
        {
            ViewBag.Id = id;
            var info = PosConsumeBll.GetById(id);

            PosConsumeBll.LoadData(new List<PosConsume>() { info }, info.PosId);

            return Json(info);
        }

        public ActionResult PrintDetail(long id)
        {
            ViewBag.Id = id;
            var info = PosConsumeBll.GetById(id);

            PosConsumeBll.LoadData(new List<PosConsume>() { info }, info.PosId);

            return View(info);
        }

        public ActionResult Index(string id,long posId = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            var o = PosDefineBll.GetByProjectNo(id);
            if(posId != 0 && o == null)
            {
                o = PosDefineBll.GetById(posId);
            }
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = o.Id;

            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }
        public ActionResult History(string id, long posId = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            var o = PosDefineBll.GetByProjectNo(id);
            if (posId != 0 && o == null)
            {
                o = PosDefineBll.GetById(posId);
            }
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = o.Id;

            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public ActionResult _SetZZ(long PosConsumeId)
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosConsumeId = PosConsumeId;
            var o = PosConsumeBll.GetById(PosConsumeId);
            ViewBag.RoomRegId = o.RoomRegId;
            return View();
        }
         

        public ActionResult _Zz(long PosConsumeId)
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosConsumeId = PosConsumeId;
            var o = PosConsumeBll.GetById(PosConsumeId);
            ViewBag.RoomRegId = o.RoomRegId;
            return View();
        }
        public ActionResult _RoomSel()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;

            return View();
        }
        public string GetPagerForZz(int page, int rows, int cwState = 0, string searchName = null)
        {
            var pager = RoomRegBll.GetPagerForZz(page, rows, UserContext.CurrentUser.HotelId, cwState, searchName);
            return JsonConvert.SerializeObject(pager);
        }
        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="cwState"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public string GetPagerForHotel(int page, int rows, int cwState = 0, string searchName = null)
        {
            var pager = RoomRegBll.GetPagerForZz(page, rows, UserContext.CurrentUser.HotelId, cwState, searchName);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 获取收退款明细
        /// </summary> 
        /// <returns></returns>
        public string GetStkList(long consumeId)
        {
            var datas = new List<RoomRegStkViewHelp>();
            //获取收款数据
            var skList = PosConsumeSkBll.GetList(consumeId);
            if (skList != null && skList.Count > 0)
            {
                var sum = skList.Sum(m => m.Money);
                var sk = new RoomRegStkViewHelp()
                {
                    Type = 0,
                    Id = Guid.NewGuid().ToString(),
                    Name = "收款",
                    Money = $"汇总：{sum}",
                    children = new List<RoomRegStkViewHelp>()
                };
                foreach (var item in skList)
                { 
                    sk.children.Add(new RoomRegStkViewHelp()
                    {
                        Type = 1,
                        Id = item.Id.ToString(),
                        Name = string.Empty,
                        DjNum = item.DjNum,
                        SgDh = item.SgDh,
                        FsTime = item.FsTime,
                        PayTypeName = item.PayTypeName,
                        RType = item.RType.ToString(),
                        YhkId = item.YhkId.ToString(),
                        Money = item.Money.ToString(),
                        HandlerName = item.HandlerName,
                        Remark = item.Remark,
                        KdRemark = item.KdRemark,
                    });
                }
                datas.Add(sk);
            }

           
            return Newtonsoft.Json.JsonConvert.SerializeObject(datas);
        }
        
        public string GetShiftHistoryList(long posId, PosConsumeSearchInfo search)
        {
            var shift_list = PosShiftBll.GetList(posId, search);
            var outdata = new List<Dictionary<string, object>>();
            var user = UserContext.CurrentUser;

            var payTypeList = PayTypeBll.GetList(user.HotelId, true, true);
            if (payTypeList == null)
                payTypeList = new List<PayType>();

            foreach (var item in shift_list)
            {
                var outitem = new Dictionary<string, object>();
                outitem["Id"] = item.Id;
                outitem["PosId"] = item.PosId;
                outitem["DjNum"] = item.DjNum;
                outitem["ShiftId"] = item.ShiftId;
                outitem["ShiftName"] = ShiftBll.GetById(item.ShiftId).Name;
                outitem["NightDate"] = item.NightDate;

                List<PosConsume> data = PosConsumeBll.GetListByPosShift(item.Id);

                outitem["childrenIds"] = string.Join(",", data.Select(a => a.Id).Distinct().ToArray());

                decimal roomAmount = 0; 

                foreach (var consume in data)
                {
                    
                    var details = PosConsumeDetailBll.GetList(consume.Id);
                    var sk_list = PosConsumeSkBll.GetList(consume.Id); 
                     
                     
                    if (consume.RoomRegId == 0) //计算转房间账
                    {
                        foreach (var detail in details)
                        {
                            if (!detail.IsFree && detail.ToRoomRegId != 0)
                            {
                                roomAmount += detail.Amount;
                            }
                        }
                        if (!outitem.ContainsKey("RoomAmount"))
                        {
                            outitem["RoomAmount"] = roomAmount;
                        }
                        else
                        {
                            outitem["RoomAmount"] = Convert.ToDecimal(outitem["RoomAmount"]) + roomAmount;
                        }
                    }else
                    {
                        if (!outitem.ContainsKey("RoomAmount"))
                        {
                            outitem["RoomAmount"] = consume.AmountSum;
                        }
                        else
                        {
                            outitem["RoomAmount"] = Convert.ToDecimal(outitem["RoomAmount"]) + consume.AmountSum;
                        }

                    }
                    if (!outitem.ContainsKey("Amount"))
                    {
                        outitem["Amount"] = consume.AmountSum;
                    }
                    else
                    {
                        outitem["Amount"] = Convert.ToDecimal(outitem["Amount"]) + consume.AmountSum;
                    }
                    if (!outitem.ContainsKey("JzskMoney"))
                    {
                        outitem["JzskMoney"] = consume.JzskMoney;
                    }
                    else
                    {
                        outitem["JzskMoney"] = Convert.ToDecimal(outitem["JzskMoney"]) + consume.JzskMoney;
                    }



                    foreach (var o in sk_list)
                    {
                        foreach (var payType in payTypeList)
                        {
                            if (o.PayTypeId == payType.Id)
                            {
                                var key = "Amount" + o.PayTypeName;
                                if (!outitem.ContainsKey(key))
                                {
                                    outitem[key] = o.Money;
                                }
                                else
                                {
                                    outitem[key] = Convert.ToDecimal(outitem[key]) + o.Money;
                                }
                            }
                        }
                    }

                }

                outdata.Add(outitem);
            }


           

            return Newtonsoft.Json.JsonConvert.SerializeObject(outdata);
        }
        public string GetJJList(long posId,string isToday, PosConsumeSearchInfo search)
        {
            var user = UserContext.CurrentUser;
            List<PosConsume> data = new List<PosConsume>();
            if (search.hasValue())
            {
                data = PosConsumeBll.GetListByPos(posId, search);
            }
            else if(isToday == "Y")
            {
                data = PosConsumeBll.GetTodayListByPos(posId);
            }
            else
            {
                data = PosConsumeBll.GetListByPos(posId);
            }

            data = data.Where(a => a.PosShiftId == 0 && a.CwState == 1).ToList();

            var payTypeList = PayTypeBll.GetList(user.HotelId, true, true);
            if (payTypeList == null)
                payTypeList = new List<PayType>();
            var outdata = new List<Dictionary<string, object>>();

            foreach(var item in data)
            {
              
                var outitem = new Dictionary<string, object>();
                outitem["Id"] = item.Id;
                outitem["PosId"] = item.PosId;
                if (item.RoomRegId != 0)
                {
                    var roomReg = RoomRegBll.GetById(item.RoomRegId);  
                    outitem["RoomNo"] = roomReg.RoomNO;
                    outitem["RoomRegIdStr"] = item.RoomRegId.ToString(); 
                    outitem["RoomRegDanJuNum"] = roomReg.DanJuNum;

                }
                else
                {
                   
                }
                 
                var details = PosConsumeDetailBll.GetList(item.Id);
                var sk_list = PosConsumeSkBll.GetList(item.Id);
                var tk_list = PosConsumeTkBll.GetList(item.Id);

                decimal roomAmount = 0;
                if(item.RoomRegId == 0)
                {
                    foreach (var detail in details)
                    {
                        if (!detail.IsFree && detail.ToRoomRegId != 0)
                        {
                            roomAmount += detail.Amount;
                        }
                    }
                    outitem["RoomAmount"] = roomAmount; 
                }
                else
                {
                    outitem["RoomAmount"] = item.AmountSum;
                }

                foreach (var o in sk_list)
                {
                    foreach(var payType in payTypeList)
                    {
                        if(o.PayTypeId == payType.Id)
                        {
                            var key = "Amount" + o.PayTypeName;
                            if (!outitem.ContainsKey(key))
                            {
                                outitem[key] = o.Money;
                            }
                            else
                            {
                                outitem[key] = Convert.ToDecimal(outitem[key]) + o.Money;
                            } 
                        }
                    } 
                }
                outitem["NightDate"] = item.NightDate;
                outitem["OrderNo"] = item.OrderNo;
                outitem["OrderDate"] = item.OrderDate;
                outitem["OutTime"] = item.OutTime;
                outitem["Amount"] = item.Amount; 
                outitem["JzskMoney"] = item.JzskMoney;
                outdata.Add(outitem);
            }

   

            return Newtonsoft.Json.JsonConvert.SerializeObject(outdata);
        }


        public string GetShiftDetails(long PosShiftId)
        {

            var user = UserContext.CurrentUser;
            List<PosConsume> data = new List<PosConsume>();
            data = PosConsumeBll.GetListByPosShift(PosShiftId);
            var outdata = new List<Dictionary<string, object>>();
            var payTypeList = PayTypeBll.GetList(user.HotelId, true, true);
            if (payTypeList == null)
                payTypeList = new List<PayType>();

            foreach (var item in data)
            {
                var outitem = new Dictionary<string, object>();
                outitem["Id"] = item.Id;
                outitem["PosId"] = item.PosId;
                if (item.RoomRegId != 0)
                {
                    var roomReg = RoomRegBll.GetById(item.RoomRegId);
                    outitem["RoomNo"] = roomReg.RoomNO;
                    outitem["RoomRegIdStr"] = item.RoomRegId.ToString();
                    outitem["RoomRegDanJuNum"] = roomReg.DanJuNum;
                }
                else
                {

                }

                var details = PosConsumeDetailBll.GetList(item.Id);
                var sk_list = PosConsumeSkBll.GetList(item.Id);
                var tk_list = PosConsumeTkBll.GetList(item.Id);

                decimal roomAmount = 0;
                if (item.RoomRegId == 0)
                {
                    foreach (var detail in details)
                    {
                        if (!detail.IsFree && detail.ToRoomRegId != 0)
                        {
                            roomAmount += detail.Amount;
                        }
                    }
                    outitem["RoomAmount"] = roomAmount;
                }else
                {
                    outitem["RoomAmount"] = item.AmountSum;
                }

                foreach (var o in sk_list)
                {
                    foreach (var payType in payTypeList)
                    {
                        if (o.PayTypeId == payType.Id)
                        {
                            var key = "Amount" + o.PayTypeName;
                            if (!outitem.ContainsKey(key))
                            {
                                outitem[key] = o.Money;
                            }
                            else
                            {
                                outitem[key] = Convert.ToDecimal(outitem[key]) + o.Money;
                            }
                        }
                    }
                }
                outitem["NightDate"] = item.NightDate;
                outitem["OrderNo"] = item.OrderNo;
                outitem["OrderDate"] = item.OrderDate;
                outitem["OutTime"] = item.OutTime;
                outitem["Amount"] = item.Amount;
                outitem["JzskMoney"] = item.JzskMoney;
                outdata.Add(outitem);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(outdata);
        }

        public ActionResult _SelHotel()
        {
            return View();
        }
        public ActionResult _SelHotel2()
        {
            return View();
        }
        public ActionResult _DetailView(long id,long roomRegId)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            var data = PosConsumeBll.GetById(id);
            ViewBag.Pos = PosDefineBll.GetById(data.PosId);
            ViewBag.PosId = data.PosId;
            ViewBag.RoomRegId = roomRegId;
            return View(data);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long posId, string isReadonly,string fromType ,long id = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss"); 
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = posId;
            ViewBag.Id = id;
            ViewBag.FomType = fromType;
            ViewBag.CurrentOrderNo = PosConsumeBll.GetNewOrderNo(posId);
            var pos = PosDefineBll.GetById(posId);
            ViewBag.Pos = pos;
            if (fromType == "History")
            {
                ViewBag.ReturnPage = "/PosConsume/History?id=" + pos.ProjectNo;
            }
            if (id == 0)
            {
                 
                return View(new PosConsume()
                {
                    OrderNo = PosConsumeBll.GetNewOrderNo(posId),
                    OpUserName = UserContext.CurrentUser.UserName,
                    PosId = posId
                });

            }
            var info = PosConsumeBll.GetById(id);
            if (info.PosId == 0) info.PosId = posId;
            ViewBag.IsReadonly = isReadonly;
            return View(info);
        }
        


        [HttpGet]
        public ActionResult _PosPrintContent(long id)
        {
            var info = PosConsumeBll.GetById(id);
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm");
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = info.PosId;
            ViewBag.CurrentUserName = UserContext.CurrentUser.Name;
            var hotel = HotelBll.GetById(UserContext.CurrentUser.HotelId);

            if(hotel != null)
            { 
                ViewBag.HotelName = hotel.Name;
            }

            var models = PosConsumeDetailBll.GetList(id);
            models = models.Where(a => a.ToRoomRegId == 0 && a.SourceRoomRegId == 0).ToList(); //没有入账房间 而且没有部分转入账房间
             
            ViewBag.DetailPrice = models.Select(a => a.Price).Sum();
            ViewBag.DetailDiscountPrice = models.Select(a => a.DiscountPrice).Sum();
            ViewBag.DetailAmount = models.Select(a => a.Amount).Sum();
            ViewBag.Detail = models;
            var pos = PosDefineBll.GetById(info.PosId);
            ViewBag.Pos = pos;
            ViewBag.PosName = pos.MenuName;
            return View(info);
        }

        [HttpGet]
        public ActionResult _EditContent(long posId, string isReadonly, string fromType, long id = 0)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            ViewBag.PosId = posId;
             
            ViewBag.CurrentOrderNo = PosConsumeBll.GetNewOrderNo(posId);
            var pos = PosDefineBll.GetById(posId);
            ViewBag.Pos = pos;
            if (fromType == "History")
            {
                ViewBag.ReturnPage = "/PosConsume/History?id=" + pos.ProjectNo;
            }
            if (id == 0)
            {

                return View(new PosConsume()
                {
                    OrderNo = PosConsumeBll.GetNewOrderNo(posId),
                    OpUserName = UserContext.CurrentUser.UserName,
                    PosId = posId
                });

            }
            var info = PosConsumeBll.GetById(id);
            if (info.PosId == 0) info.PosId = posId;
            ViewBag.IsReadonly = isReadonly;
            return View(info);
        }



        public string getEditPageDetailList(long id)
        {

            var info = PosConsumeBll.GetById(id);
            var models = PosConsumeDetailBll.GetList(id);
            models = models.Where(a => a.ToRoomRegId == 0 && a.SourceRoomRegId == 0).ToList(); //没有入账房间 而且没有部分转入账房间
            

            return JsonConvert.SerializeObject(models);
        }


        public string getdetailList(long id)
        {
            var models = PosConsumeDetailBll.GetList(id);
            models = models.Where(a => a.ToRoomRegId == 0).ToList();
            return JsonConvert.SerializeObject(models);
        }

        public string getZZDetailList(long id)
        {
            var models = PosConsumeDetailBll.GetZZDetailList(id);
            return JsonConvert.SerializeObject(models);
        }

        public string GetZZListForZz(long id)
        {
            var models = PosConsumeDetailBll.GetList(id); 
            models = models.Where(a => a.ToRoomRegId == 0).ToList();
            models = models.Where(a => a.IsFree == false).ToList(); //非免单项目才显示在转账里面
            return JsonConvert.SerializeObject(models);
        }
        public string GetZZListForZzCancel(long id)
        {
            var models = PosConsumeDetailBll.GetList(id);
            models = models.Where(a => a.ToRoomRegId != 0).ToList();
            models = models.Where(a => a.IsFree == false).ToList(); //非免单项目才显示在转账里面
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 房间财务里面显示的消费明细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDetailListForRoomDetail(long id,long roomRegId)
        {
            var models = PosConsumeDetailBll.GetList(id);
            models = models.Where(a => a.ToRoomRegId == roomRegId || a.SourceRoomRegId == roomRegId).ToList();
 


            var info = PosConsumeBll.GetById(id);
           
            return JsonConvert.SerializeObject(models);
        }

        public string GetListByIds(long[] Ids)
        {
            var data = PosConsumeBll.GetListByIds(Ids);
            var user = UserContext.CurrentUser;
            var payTypeList = PayTypeBll.GetList(user.HotelId, true, true);
            if (payTypeList == null)
                payTypeList = new List<PayType>();
            var outdata = new List<Dictionary<string, object>>();

            foreach (var item in data)
            {

                var outitem = new Dictionary<string, object>();
                outitem["Id"] = item.Id;
                outitem["PosId"] = item.PosId;
                if (item.RoomRegId != 0)
                {
                    var roomReg = RoomRegBll.GetById(item.RoomRegId);
                    outitem["RoomNo"] = roomReg.RoomNO;
                    outitem["RoomRegIdStr"] = item.RoomRegId.ToString();
                    outitem["RoomRegDanJuNum"] = roomReg.DanJuNum;
                }
                else
                {

                }

                var details = PosConsumeDetailBll.GetList(item.Id);
                var sk_list = PosConsumeSkBll.GetList(item.Id);
                var tk_list = PosConsumeTkBll.GetList(item.Id);

                decimal roomAmount = 0;
                if (item.RoomRegId == 0)
                {
                    foreach (var detail in details)
                    {
                        if (!detail.IsFree && detail.ToRoomRegId != 0)
                        {
                            roomAmount += detail.Amount;
                        }
                    }
                    outitem["RoomAmount"] = roomAmount;
                }
                else
                {
                    outitem["RoomAmount"] = item.AmountSum;
                }

                foreach (var o in sk_list)
                {
                    foreach (var payType in payTypeList)
                    {
                        if (o.PayTypeId == payType.Id)
                        {
                            var key = "Amount" + o.PayTypeName;
                            if (!outitem.ContainsKey(key))
                            {
                                outitem[key] = o.Money;
                            }
                            else
                            {
                                outitem[key] = Convert.ToDecimal(outitem[key]) + o.Money;
                            }
                        }
                    }
                }
                outitem["NightDate"] = item.NightDate;
                outitem["OrderNo"] = item.OrderNo;
                outitem["OrderDate"] = item.OrderDate;
                outitem["OutTime"] = item.OutTime;
                outitem["Amount"] = item.Amount;
                outitem["JzskMoney"] = item.JzskMoney;
                outdata.Add(outitem);
            }
            return JsonConvert.SerializeObject(outdata);
        }

        public string GetList(long posId)
        {
            var models = PosConsumeBll.GetListByPos(posId);
            return JsonConvert.SerializeObject(models);
        }

 

        public string GetPager(int page, int rows ,long posId,string searchName,string isToday,string isJz, PosConsumeSearchInfo search)
        {
 
            if(isToday == "Y")
            {
                var models2 = PosConsumeBll.GetTodayPagerByPos(isJz,page, rows, posId);
                return JsonConvert.SerializeObject(models2);
            }
            var models = PosConsumeBll.GetPagerByPos(isJz, page,rows,posId, searchName, search); 

            return JsonConvert.SerializeObject(models);
        }
 
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult DelDetails(long[] Ids)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeBll.DelDetails(Ids);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }

            return Json(apiResult);
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult MD(long[] Ids)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeBll.MD(Ids);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }

            return Json(apiResult);
        }


        
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult PB(long[] Ids,long ShiftId)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeBll.PB(Ids, ShiftId);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }

            return Json(apiResult);
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonResult ZzSave(long PosConsumeId, long toRoomRegId, List<PosConsumeDetail> fyList)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeBll.ZzSave(PosConsumeId, toRoomRegId, fyList, user.Id, user.Name);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message; 
            }

            return Json(apiResult);
        }


        [ValidateInput(false)]
        [HttpPost]
        public JsonResult ZzCancelSave(long PosConsumeId,  List<PosConsumeDetail> fyList)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeBll.ZzCancelSave(PosConsumeId, fyList, user.Id, user.Name);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }

            return Json(apiResult);
        }


        
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(PosConsume) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(PosConsume model,string detailJson)
        {
            var apiResult = new APIResult();
            try
            {
                model.Details = JsonConvert.DeserializeObject<List<PosConsumeDetail>>(detailJson);

                PosConsumeBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);

                apiResult.SeqId = model.Id;
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message; 
 
            }

            return Json(apiResult);
        }

        public JsonResult SaveDetails(long id, string detailJson)
        {
            var apiResult = new APIResult();
            try
            {
                PosConsume model = PosConsumeBll.GetById(id);
                model.Details = JsonConvert.DeserializeObject<List<PosConsumeDetail>>(detailJson);

                PosConsumeBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
                 
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;

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
                PosConsumeBll.DeleteById(id);
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
        public JsonResult JzSave(long consumeId,   int jstype, decimal yhMoney, int type, decimal yisMoney,
    List<JzViewHelp> jzList, List<RoomRegZw> dwjzList, string remark, string sgdh)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                PosConsumeBll.JzSave(consumeId, jstype, yhMoney, type, yisMoney, jzList, dwjzList, remark, sgdh, user.HotelId, user.Id, user.Name);
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