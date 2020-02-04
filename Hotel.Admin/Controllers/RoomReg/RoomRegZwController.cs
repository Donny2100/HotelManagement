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
    public class RoomRegZwController : AdminBaseController
    {
        // GET: RoomRegZw
        public ActionResult Index(long roomRegId)
        {
            var lflist = new List<Model.RoomReg>();

            var user = UserContext.CurrentUser;
            var treeList = RoomRegBll.GetLfTree(roomRegId, UserContext.CurrentUser.HotelId,ref lflist);
            //RoomRegBll.AutoQtfy(lflist, user.Id, user.Name, user.HotelId);
            var roomReg = new Model.RoomReg();
            decimal yszk = 0, yuszk = 0, yjs = 0, zfy = 0, zstk = 0, jy = 0, bfsk = 0;
            if (treeList.Count > 0)
            {
                //roomRegId = long.Parse(treeList[0].id);
                //获取房间的登记信息
                roomReg = RoomRegBll.GetById(roomRegId);
                if (roomReg != null)
                {
                    yszk += roomReg.Yszk;
                    yuszk += roomReg.Yuszk;
                    yjs += roomReg.Yjs;
                    zfy += roomReg.Money;
                    zstk += roomReg.Zstk;
                    jy += roomReg.Jy;
                    bfsk += roomReg.YeBfSkMoney + roomReg.FyeBfSkMoney;
                }
                ViewBag.RoomReg = roomReg;
                //获取所有联房的房间的财务汇总
                for (var i = 1; i < treeList.Count; i++)
                {
                    var item = RoomRegBll.GetById(long.Parse(treeList[i].id));
                    if (item != null)
                    {
                        yszk += item.Yszk;
                        yuszk += item.Yuszk;
                        yjs += item.Yjs;
                        zfy += item.Money;
                        zstk += item.Zstk;
                        jy += item.Jy;
                        bfsk += item.YeBfSkMoney + item.FyeBfSkMoney;
                    }
                }
            }
            //在treelist上面加一个全部
            //var root = new RoomRegTree()
            //{
            //    id = "0",
            //    @checked = false,
            //    children = treeList,
            //    text = "全部"
            //};
            //ViewBag.TreeList = new List<RoomRegTree>() { root };
            ViewBag.TreeList = treeList;
            ViewBag.Zzw = new RoomRegZzwHelp() { yszk = yszk, yuszk = yuszk, yjs = yjs, zfy = zfy, zstk = zstk, jy = jy, bfsk = bfsk };
            //获取未结账房间数，为了结账时方式的判断
            //lflist = lflist.Where(m => m.CwState != 3 && m.CwState != 4 && m.CwState != 2).ToList();
            lflist = lflist.Where(m => m.CwState == 1).ToList();
            ViewBag.WjsRoomCount = lflist.Count;
            return View(roomReg);
        }

        public RoomRegZzwHelp getthis(long roomRegId) {
            var lflist = new List<Model.RoomReg>();
            var treeList = RoomRegBll.GetLfTree(roomRegId, UserContext.CurrentUser.HotelId, ref lflist);
            var roomReg = new Model.RoomReg();
            decimal yszk = 0, yuszk = 0, yjs = 0, zfy = 0, zstk = 0, jy = 0, bfsk = 0;
            if (treeList.Count > 0)
            {
                //roomRegId = long.Parse(treeList[0].id);
                //获取房间的登记信息
                roomReg = RoomRegBll.GetById(roomRegId);
                if (roomReg != null)
                {
                    yszk += roomReg.Yszk;
                    yuszk += roomReg.Yuszk;
                    yjs += roomReg.Yjs;
                    zfy += roomReg.Money;
                    zstk += roomReg.Zstk;
                    jy += roomReg.Jy;
                    bfsk += roomReg.YeBfSkMoney + roomReg.FyeBfSkMoney;
                }
                ViewBag.RoomReg = roomReg;
                //获取所有联房的房间的财务汇总
                for (var i = 1; i < treeList.Count; i++)
                {
                    var item = RoomRegBll.GetById(long.Parse(treeList[i].id));
                    if (item != null)
                    {
                        yszk += item.Yszk;
                        yuszk += item.Yuszk;
                        yjs += item.Yjs;
                        zfy += item.Money;
                        zstk += item.Zstk;
                        jy += item.Jy;
                        bfsk += item.YeBfSkMoney + item.FyeBfSkMoney;
                    }
                }
            }

            RoomRegZzwHelp rrom= new RoomRegZzwHelp() { yszk = yszk, yuszk = yuszk, yjs = yjs, zfy = zfy, zstk = zstk, jy = jy, bfsk = bfsk };
                
           
            return rrom;
        }
         
        public JsonResult RefreshTree(long roomRegId)
        {
            var lflist = new List<Model.RoomReg>();
            var treeList = RoomRegBll.GetLfTree(roomRegId, UserContext.CurrentUser.HotelId,ref lflist);

            var roomReg = new Model.RoomReg();
            decimal yszk = 0, yuszk = 0, yjs = 0, zfy = 0, zstk = 0, jy = 0, bfsk = 0;
            if (treeList.Count > 0)
            {
                //roomRegId = long.Parse(treeList[0].id);
                //获取房间的登记信息
                roomReg = RoomRegBll.GetById(roomRegId);
                if (roomReg != null)
                {
                    yszk += roomReg.Yszk;
                    yuszk += roomReg.Yuszk;
                    yjs += roomReg.Yjs;
                    zfy += roomReg.Money;
                    zstk += roomReg.Zstk;
                    jy += roomReg.Jy;
                    bfsk += roomReg.YeBfSkMoney + roomReg.FyeBfSkMoney;
                }
                ViewBag.RoomReg = roomReg;
                //获取所有联房的房间的财务汇总
                for (var i = 1; i < treeList.Count; i++)
                {
                    var item = RoomRegBll.GetById(long.Parse(treeList[i].id));
                    if (item != null)
                    {
                        yszk += item.Yszk;
                        yuszk += item.Yuszk;
                        yjs += item.Yjs;
                        zfy += item.Money;
                        zstk += item.Zstk;
                        jy += item.Jy;
                        bfsk += item.YeBfSkMoney + item.FyeBfSkMoney;
                    }
                }
            }
            //在treelist上面加一个全部
            var root = new RoomRegTree()
            {
                id = "0",
                @checked = false,
                children = treeList,
                text = "全部"
            };
            var tree = new List<RoomRegTree>() { root };
            var zzw = new RoomRegZzwHelp() { yszk = yszk, yuszk = yuszk, yjs = yjs, zfy = zfy, zstk = zstk, jy = jy, bfsk = bfsk };
            //获取未结账房间数，为了结账时方式的判断
            //lflist = lflist.Where(m => m.CwState != 3 && m.CwState != 4 && m.CwState != 2).ToList();
            lflist = lflist.Where(m => m.CwState == 1).ToList();
            return Json(new { zzw = zzw, tree = tree, wjsRoomCount = lflist.Count });
        }

        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public string GetFyList(long roomRegId)
        {
            var list = RoomRegZwBll.GetFyList(roomRegId);
            return JsonConvert.SerializeObject(list);
        }

        #region 费用明细的右键操作

        public ActionResult _Remark(long id, int rtype, string remark)
        {
            return View(new RoomRegZw() { Id = id.ToString(), RType = rtype, Remark = remark });
        }

        public ActionResult _KdRemark(long id, int rtype, string kdRemark)
        {
            return View(new RoomRegZw() { Id = id.ToString(), RType = rtype, KdRemark = kdRemark });
        }

        public ActionResult _Cffy(long id, int rtype)
        {
            var data = new RoomRegZw();
            if (rtype == 1)
            {
                var model = RoomRegFfRecordBll.GetById(id);
                data.Id = model.Id.ToString();
                data.DjNum = string.Empty;
                data.Remark = model.Remark;
                data.KdRemark = model.KdRemark;
                data.Money = model.Money.ToString();
            }
            else if (rtype == 2)
            {
                var model = RoomRegGoodsDetailsBll.GetById(id);
                data.Id = model.Id.ToString();
                data.DjNum = string.Empty;
                data.Remark = model.Remark;
                data.KdRemark = model.KdRemark;
                data.Money = model.Money.ToString();
            }
            else if (rtype == 3)
            {
                var model = RoomRegSwpcDetailsBll.GetById(id);
                data.Id = model.Id.ToString();
                data.DjNum = string.Empty;
                data.Remark = model.Remark;
                data.KdRemark = model.KdRemark;
                data.Money = model.Money.ToString();
            }
            else if (rtype == 4)
            {
                var model = RoomRegQtfyBll.GetById(id);
                data.Id = model.Id.ToString();
                data.DjNum = string.Empty;
                data.Remark = model.Remark;
                data.KdRemark = model.KdRemark;
                data.Money = model.Money.ToString();
            }

            return View(data);
        }

        /// <summary>
        /// 编辑备注
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditRemark(long id, int rtype, string remark)
        {
            var apiResult = new APIResult();
            try
            {
                if (rtype == 1)
                    RoomRegFfRecordBll.EditRemark(id, remark);
                else if (rtype == 2)
                    RoomRegGoodsDetailsBll.EditRemark(id, remark);
                else if (rtype == 3)
                    RoomRegSwpcDetailsBll.EditRemark(id, remark);
                else if (rtype == 4)
                    RoomRegQtfyBll.EditRemark(id, remark);
                else if (rtype == 5)
                    RoomRegHcDetailBll.EditRemark(id, remark);
                else if (rtype == 6)
                    RoomRegYhBll.EditRemark(id, remark);
                else if (rtype == 7)
                    RoomRegJfyhBll.EditRemark(id, remark);
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
        /// 编辑客单备注
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditKdRemark(long id, int rtype, string kdRemark)
        {
            var apiResult = new APIResult();
            try
            {
                if (rtype == 1)
                    RoomRegFfRecordBll.EditKdRemark(id, kdRemark);
                else if (rtype == 2)
                    RoomRegGoodsDetailsBll.EditKdRemark(id, kdRemark);
                else if (rtype == 3)
                    RoomRegSwpcDetailsBll.EditKdRemark(id, kdRemark);
                else if (rtype == 4)
                    RoomRegQtfyBll.EditKdRemark(id, kdRemark);
                else if (rtype == 5)
                    RoomRegHcDetailBll.EditKdRemark(id, kdRemark);
                else if (rtype == 6)
                    RoomRegYhBll.EditKdRemark(id, kdRemark);
                else if (rtype == 7)
                    RoomRegJfyhBll.EditKdRemark(id, kdRemark);
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
        /// 拆分费用
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditCffy(long id, int rtype, decimal money1, decimal money2)
        {
            var user = UserContext.CurrentUser;
            var apiResult = new APIResult();
            try
            {
                if (rtype == 1)
                    RoomRegFfRecordBll.EditCffy(id, money1, money2, user.Id, user.Name);
                else if (rtype == 2)
                    RoomRegGoodsDetailsBll.EditCffy(id, money1, money2, user.Id, user.Name);
                else if (rtype == 3)
                    RoomRegSwpcDetailsBll.EditCffy(id, money1, money2, user.Id, user.Name);
                else if (rtype == 4)
                    RoomRegQtfyBll.EditCffy(id, money1, money2, user.Id, user.Name);
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

        /// <summary>
        /// 获取收退款明细
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <param name="type">1:收退款   2：总收退款</param>
        /// <returns></returns>
        public string GetStkList(long roomRegId, int type)
        {
            var datas = new List<RoomRegStkViewHelp>();
            //获取收款数据
            var skList = RoomRegSkBll.GetList(roomRegId, type);
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
                    //var rtype = item.RType == StkTypeEnum.银行卡预收款 ? "预收账款" : item.RType.ToString();
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

            //获取退款数据
            var tkList = RoomRegTkBll.GetList(roomRegId, type);
            if (tkList != null && tkList.Count > 0)
            {
                var sum = tkList.Sum(m => m.Money);
                var tk = new RoomRegStkViewHelp()
                {
                    Type = 0,
                    Id = Guid.NewGuid().ToString(),
                    Name = "退款",
                    Money = $"汇总：{sum}",
                    children = new List<RoomRegStkViewHelp>()
                };
                foreach (var item in tkList)
                {
                    tk.children.Add(new RoomRegStkViewHelp()
                    {
                        Type = 2,
                        Id = item.Id.ToString(),
                        Name = string.Empty,
                        DjNum = item.DjNum,
                        SgDh = item.SgDh,
                        FsTime = item.FsTime,
                        PayTypeName = item.PayTypeName,
                        RType = item.RType.ToString(),
                        Money = item.Money.ToString(),
                        HandlerName = item.HandlerName,
                        Remark = item.Remark,
                        KdRemark = item.KdRemark,
                    });
                }
                datas.Add(tk);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(datas);
        }

        #region 转账

        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public string GetFyListForZz(long roomRegId)
        {
            var list = RoomRegZwBll.GetFyListForZz(roomRegId);
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取收款明细
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public string GetSkListForZz(long roomRegId)
        {
            var datas = new List<RoomRegSk>();
            //获取收款数据
            var skList = RoomRegSkBll.GetList(roomRegId).Where(m => m.RType == StkTypeEnum.预收账款).ToList();
            if (skList != null && skList.Count > 0)
            {
                foreach (var item in skList)
                {
                    datas.Add(item);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(datas);
        }

        /// <summary>
        /// 获取退款明细
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public string GetTkListForZz(long roomRegId)
        {
            var datas = new List<RoomRegTk>();
            //获取退款数据
            var tkList = RoomRegTkBll.GetList(roomRegId).Where(m => m.RType == StkTypeEnum.预收退款).ToList();
            if (tkList != null && tkList.Count > 0)
            {
                foreach (var item in tkList)
                {
                    datas.Add(item);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(datas);
        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public ActionResult _Zz(long roomRegId)
        {
            ViewBag.RoomRegId = roomRegId;
            return View();
        }

        /// <summary>
        /// 转账的保存
        /// </summary>
        /// <param name="fromRoomRegId"></param>
        /// <param name="toRoomRegId"></param>
        /// <param name="fyList"></param>
        /// <param name="skList"></param>
        /// <param name="tkList"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult ZzSave(long fromRoomRegId, long toRoomRegId, List<RoomRegZw> fyList, List<RoomRegSk> skList, List<RoomRegTk> tkList)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.ZzSave(fromRoomRegId, toRoomRegId, fyList, skList, tkList, user.Id, user.Name);
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
        /// 获取已转账明细
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        public string GetYzzList(long roomRegId)
        {
            if (roomRegId == 0)
                roomRegId = -1;
            var datas = new List<RoomRegYzzHelp>();
            //获取费用数据
            var fyList = RoomRegZwBll.GetFyListForYzz(roomRegId);
            datas.AddRange(fyList);
            //获取收款数据
            var skList = RoomRegSkBll.GetListForYzz(roomRegId);
            if (skList != null && skList.Count > 0)
            {
                var sum = skList.Sum(m => m.Money);
                var sk = new RoomRegYzzHelp()
                {
                    Id = Guid.NewGuid().ToString(),
                    ZzTime = string.Empty,
                    Name = "收款",
                    Price = string.Empty,
                    Quantity = string.Empty,
                    Money = $"汇总：{sum}",
                    DjNum = string.Empty,
                    KdRemark = string.Empty,
                    Remark = string.Empty,
                    ZzHandler = string.Empty,
                    YsTime = string.Empty,
                    children = new List<RoomRegYzzHelp>()
                };
                foreach (var item in skList)
                {
                    sk.children.Add(new RoomRegYzzHelp()
                    {
                        RType = FyAndStkTypeEnum.收款,
                        Id = item.Id.ToString(),
                        RoomRegId = item.RoomRegId.ToString(),
                        ZzTime = TypeConvert.IntToDateTime(item.ZzTime).ToString("yyyy-MM-dd HH:mm"),
                        Name = string.Empty,
                        Price = string.Empty,
                        Quantity = string.Empty,
                        Money = item.Money.ToString(),
                        DjNum = string.Empty,
                        KdRemark = string.Empty,
                        Remark = item.Remark,
                        ZzHandler = item.ZzHandler,
                        YsTime = item.YsTime,
                        OldRoomRegId = item.OldRoomRegId.ToString(),
                        Desc = $"从房间【房号：{item.OldRoomNO},登记单：{item.OldDjdNum}】转到房间【{item.RoomNO},登记单：{item.DjdNum}】"
                    });
                }
                datas.Add(sk);
            }
            //获取退款数据
            var tkList = RoomRegTkBll.GetListYzz(roomRegId);
            if (tkList != null && tkList.Count > 0)
            {
                var sum = tkList.Sum(m => m.Money);
                var tk = new RoomRegYzzHelp()
                {
                    Id = Guid.NewGuid().ToString(),
                    ZzTime = string.Empty,
                    Name = "退款",
                    Price = string.Empty,
                    Quantity = string.Empty,
                    Money = $"汇总：{sum}",
                    DjNum = string.Empty,
                    KdRemark = string.Empty,
                    Remark = string.Empty,
                    ZzHandler = string.Empty,
                    YsTime = string.Empty,
                    children = new List<RoomRegYzzHelp>()
                };
                foreach (var item in tkList)
                {
                    tk.children.Add(new RoomRegYzzHelp()
                    {
                        RType = FyAndStkTypeEnum.退款,
                        Id = item.Id.ToString(),
                        RoomRegId = item.RoomRegId.ToString(),
                        ZzTime = TypeConvert.IntToDateTime(item.ZzTime).ToString("yyyy-MM-dd HH:mm"),
                        Name = string.Empty,
                        Price = string.Empty,
                        Quantity = string.Empty,
                        Money = item.Money.ToString(),
                        DjNum = string.Empty,
                        KdRemark = string.Empty,
                        Remark = item.Remark,
                        ZzHandler = item.ZzHandler,
                        YsTime = item.YsTime,
                        OldRoomRegId = item.OldRoomRegId.ToString(),
                        Desc = $"从房间【房号：{item.OldRoomNO},登记单：{item.OldDjdNum}】转到房间【{item.RoomNO},登记单：{item.DjdNum}】"
                    });
                }
                datas.Add(tk);
            }
            return JsonConvert.SerializeObject(datas);
        }

        /// <summary>
        /// 转账撤销的保存
        /// </summary>
        /// <returns></returns>
        public JsonResult ZzCxSave(List<RoomRegYzzHelp> models)
        {
            var apiResult = new APIResult();
            try
            {
                RoomRegZwBll.ZzCxSave(models);
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

        #region 部分结账

        public string GetFyListForBfjz(long roomRegId)
        {
            var list = RoomRegZwBll.GetFyListForBfjz(roomRegId);
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 部分结账页面--与结账页面大体相同
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public ActionResult _Bfjz(long roomRegId)
        {
            var user = UserContext.CurrentUser;
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                roomReg = new Model.RoomReg() { Id = roomRegId, HotelId = user.HotelId };
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
            return View(roomReg);
        }

        /// <summary>
        /// 结账明细
        /// </summary>
        /// <returns></returns>
        public ActionResult _BfjzJzmx()
        {
            return View();
        }

        /// <summary>
        /// 部分结账的保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BfjzSave(long roomRegId, List<RoomRegZw> models, decimal yhMoney, int type, decimal yisMoney, List<JzViewHelp> jzList, List<RoomRegZw> dwjzList)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.BfjzSave(roomRegId, models, yhMoney, type, yisMoney, jzList, dwjzList, user.HotelId, user.Id, user.Name);
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

        #region 已结账

        public string GetFyListForYjz(long roomRegId)
        {
            #region 获取部分结账单  和  明细

            var bfjzList = RoomRegBfskdBll.GetList(roomRegId);

            #endregion

            #region 获取结账单  和  明细

            //var jzdList = RoomRegJzdBll.GetList(roomRegId);

            #endregion

            var datas = new List<dynamic>();
            foreach (var item in bfjzList)
            {
                datas.Add(item);
            }
            //foreach (var item in jzdList)
            //{
            //    datas.Add(item);
            //}

            return JsonConvert.SerializeObject(datas);
        }

        /// <summary>
        /// 部分结算的撤销
        /// </summary>
        /// <param name="rtype"></param>
        /// <param name="jzdId"></param>
        /// <param name="isCxZt"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BfjsCx(long jzdId)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                //撤销部分结账
                RoomRegZwBll.BfjzCx(jzdId, user.HotelId, user.Id, user.Name);
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
        /// 结账的撤销
        /// </summary>
        /// <param name="rtype"></param>
        /// <param name="jzdId"></param>
        /// <param name="isCxZt"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JzCx(long roomRegId, bool isCxZt, string reason)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                //撤销结账
                RoomRegZwBll.JzCx(roomRegId, isCxZt, reason, user.HotelId, user.Id, user.Name);
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

        #region 结账

        /// <summary>
        /// 结账页面
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        public ActionResult _Jz(long roomRegId,int jstype)
        {
            var user = UserContext.CurrentUser;
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                roomReg = new Model.RoomReg() { Id = roomRegId, HotelId = user.HotelId };
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
            //获取未结账的联房数据
            //获取未结算明细
            //var wjsmxList = new List<RoomRegZw>();
            if (jstype == (int)JzType.将其他所有联房费用数据转到该房间有该房间结账)
            {
                var lfList = RoomRegBll.Fetch($"where ZfDjId={roomReg.ZfDjId}");
                //lfList = lfList.Where(m => m.CwState != 3 && m.CwState != 4 && m.CwState != 2).ToList();
                lfList = lfList.Where(m => m.CwState == 1).ToList();
                if (lfList == null || lfList.Count == 0)
                    return Content("无未结算的联房数据");
                foreach (var lf in lfList)
                {
                    //var wjsmx = RoomRegZwBll.GetFyListForBfjz(lf.Id);
                    //if (wjsmx != null)
                    //    wjsmxList.AddRange(wjsmx);
                    //计算汇总
                    if (lf.Id == roomRegId)
                        continue;
                    //roomReg.Yszk += lf.Yszk;
                    roomReg.FfMoney += lf.FfMoney;
                    roomReg.SpMoney += lf.SpMoney;
                    roomReg.SwpcMoney += lf.SwpcMoney;
                    roomReg.QtMoney += lf.QtMoney;
                    roomReg.HcMoney += lf.HcMoney;
                    roomReg.YhMoney += lf.YhMoney;
                    roomReg.ExpYhMoney += lf.ExpYhMoney;

                    roomReg.ZskMoney += lf.ZskMoney;
                    roomReg.ZtkMoney += lf.ZtkMoney;
                }
            }
           
            //ViewBag.WjsmxList = wjsmxList;
            //获取信用卡预授权
            var xykysqList = RoomRegXykBll.GetYsq(roomRegId);
            if (xykysqList == null || xykysqList.Count == 0)
                xykysqList = new List<RoomRegXyk>();
            ViewBag.Xykysq = xykysqList;
            ViewBag.jstype = jstype;
            return View(roomReg);
        }

        /// <summary>
        /// 单独结账，并将该房间的全部费用转到主房间，由主房间结账，点结账后不需要弹出结账界面
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JzWithoutPay(long roomRegId)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.JzWithoutPay(roomRegId, user.Id, user.Name);
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
        /// 结账的保存,当前房间单独结账，并取消联房
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JzSave(long roomRegId, int jstype, decimal yhMoney, int type, decimal yisMoney,
            List<JzViewHelp> jzList, List<RoomRegZw> dwjzList, string remark, string sgdh)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.JzSave(roomRegId,jstype, yhMoney, type, yisMoney, jzList, dwjzList, remark, sgdh, user.HotelId, user.Id, user.Name);
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
        /// 所有联房房间费用转到该房间，由该房间结账
        /// </summary>
        /// <param name="roomRegId"></param>
        /// <param name="jstype"></param>
        /// <param name="yhMoney"></param>
        /// <param name="type"></param>
        /// <param name="yisMoney"></param>
        /// <param name="jzList"></param>
        /// <param name="dwjzList"></param>
        /// <param name="remark"></param>
        /// <param name="sgdh"></param>
        /// <returns></returns>
        public JsonResult JzSaveAllLf(long roomRegId, int jstype, decimal yhMoney, int type, decimal yisMoney,
            List<JzViewHelp> jzList, List<RoomRegZw> dwjzList, string remark, string sgdh)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.JzSaveAllLf(roomRegId, jstype, yhMoney, type, yisMoney, jzList, dwjzList, remark, sgdh, user.HotelId, user.Id, user.Name);
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
        /// 单独房间的结账
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JzSaveSingle(long roomRegId, int jstype, decimal yhMoney, int type, decimal yisMoney,
            List<JzViewHelp> jzList, List<RoomRegZw> dwjzList, string remark, string sgdh)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.JzSaveSingle(roomRegId, jstype, yhMoney, type, yisMoney, jzList, dwjzList, remark, sgdh, user.HotelId, user.Id, user.Name);
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
        /// 结账明细
        /// </summary>
        /// <returns></returns>
        public ActionResult _JzJzmx()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 未结退房
        /// </summary>
        /// <param name="roomRegId"></param>
        public JsonResult Wjtf(long roomRegId)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.Wjtf(roomRegId);
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
        /// 未结退房
        /// </summary>
        /// <param name="roomRegId"></param>
        public JsonResult CxWjtf(long roomRegId, string reason)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.CxWjtf(roomRegId, reason, user.Id, user.Name, user.HotelId);
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

        public JsonResult SaveRoomReg(long Id, string remark)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                RoomRegZwBll.SaveRoomReg(Id, remark);
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
        public string GetRoomReg(long roomRegId)
        {
            var roomReg = RoomRegBll.GetById(roomRegId);
            return Newtonsoft.Json.JsonConvert.SerializeObject(roomReg);
        }
    }
}