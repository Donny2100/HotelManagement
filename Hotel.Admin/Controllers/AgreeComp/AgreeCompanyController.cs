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
    public class AgreeCompanyController : AdminBaseController
    {
        // GET: AgreeCompany
        #region 协议单位

        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetPager(int page, int rows, long khlxId = 0, long khztId = 0, long khhyId = 0, long guestSourceId = 0, string searchName = null)
        {
            var pager = AgreeCompanyBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, searchName, khlxId, khztId, khhyId, guestSourceId);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(string id = "")
        {
            if (string.IsNullOrWhiteSpace(id))
                return View(new AgreeCompany() { HotelId = UserContext.CurrentUser.HotelId });
            var info = AgreeCompanyBll.GetById(id);
            return View(info);
        }

        public string GetList()
        {
            var models = AgreeCompanyBll.GetList(UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(models);
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompany) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(AgreeCompany model)
        {
            var apiResult = new APIResult();
            try
            {
                AgreeCompanyBll.AddOrUpdate(model, UserContext.CurrentUser.HotelId);
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
        public ActionResult Delete(string id)
        {
            var apiResult = new APIResult();
            try
            {
                AgreeCompanyBll.Delete(id);
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

        #region 客户协议

        /// <summary>
        /// 获取客户协议列表
        /// </summary>
        /// <param name="agreeCompId"></param>
        /// <returns></returns>
        public string GetKhxyList(string agreeCompId)
        {
            var datas = AgreeCompanyKhxyBll.GetList(UserContext.CurrentUser.HotelId, agreeCompId);
            return JsonConvert.SerializeObject(datas);
        }

        public ActionResult _KhxyEdit(string agreeCompId = "", long id = 0)
        {
            if (id <= 0)
                return View(new AgreeCompanyKhxy() { AgreeCompId = agreeCompId, HotelId = UserContext.CurrentUser.HotelId });
            var info = AgreeCompanyKhxyBll.GetById(id);
            return View(info);
        }

        /// <summary>
        /// 保存协议主项
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompanyKhxy) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditXyzx(AgreeCompanyKhxy model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompanyKhxyBll.AddOrUpdateXyzx(model, user.Id, user.Name, user.HotelId);
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
        /// 保存协议内容
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompanyKhxy) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditXynr(AgreeCompanyKhxy model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompanyKhxyBll.AddOrUpdateXynr(model, user.Id, user.Name, user.HotelId);
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
        /// 获取协议房价列表
        /// </summary>
        /// <param name="agreeCompId"></param>
        /// <returns></returns>
        public string GetXyfjList(string agreeCompId)
        {
            var datas = AgreeCompanyFjxyBll.GetXyfjList(agreeCompId, UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(datas);
        }

        /// <summary>
        /// 保存协议房价
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(AgreeCompany) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditXyfj(List<AgreeCompanyFjxy> models)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompanyFjxyBll.AddOrUpdateFjxy(models, user.Id, user.Name, user.HotelId);
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
        public ActionResult KhxyDelete(long id)
        {
            var apiResult = new APIResult();
            try
            {
                AgreeCompanyKhxyBll.DeleteById(id);
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

        #region 联系人

        public string GetContactList(string agreeCompId)
        {
            var datas = AgreeCompanyContactBll.GetList(UserContext.CurrentUser.HotelId, agreeCompId);
            return JsonConvert.SerializeObject(datas);
        }

        public ActionResult _ContactEdit(string agreeCompId = "", long id = 0)
        {
            if (id <= 0)
                return View(new AgreeCompanyContact() { AgreeCompId = agreeCompId, HotelId = UserContext.CurrentUser.HotelId });
            var info = AgreeCompanyContactBll.GetById(id);
            return View(info);
        }

        /// <summary>
        /// 保存联系人
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompanyContact) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult EditContact(AgreeCompanyContact model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompanyContactBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
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
        public ActionResult ContactDelete(long id)
        {
            var apiResult = new APIResult();
            try
            {
                AgreeCompanyContactBll.DeleteById(id);
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

        #region 佣金

        public string GetCommisionPager(int page, int rows, string agreeCompId, int state = -1, DateTime? startDate = null, DateTime? endDate = null)
        {
            var datas = AgreeCompCommisionRecordBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, agreeCompId, state, startDate, endDate);
            return JsonConvert.SerializeObject(datas);
        }

        public ActionResult _FhCommisionHandler(string agreeCompId = "", long id = 0)
        {
            if (id <= 0)
                return View(new AgreeCompCommisionRecord() { AgreeCompId = agreeCompId, HotelId = UserContext.CurrentUser.HotelId });
            var info = AgreeCompCommisionRecordBll.GetById(id);
            return View(info);
        }

        /// <summary>
        /// 保存 返还佣金 
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompanyContact) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult FhCommisionHandler(AgreeCompCommisionRecord model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompCommisionRecordBll.FhCommisionHandler(model, user.Id, user.Name);
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

        #region 收退款

        public string GetStkPager(int page, int rows, string agreeCompId, int rtype = 0)
        {
            var pager = AgreeCompStkRecordBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, agreeCompId, rtype);
            return JsonConvert.SerializeObject(pager);
        }

        public ActionResult _SkEdit(string agreeCompId = "", long id = 0)
        {
            return View(new AgreeCompStkRecord() { AgreeCompId = agreeCompId, FsDate = DateTime.Now, HotelId = UserContext.CurrentUser.HotelId });
        }

        /// <summary>
        /// 保存收款
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompStkRecord) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult SkEdit(AgreeCompStkRecord model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompStkRecordBll.AddSk(model, user.HotelId, user.Id, user.Name);
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

        public ActionResult _TkEdit(string agreeCompId = "", long id = 0)
        {
            return View(new AgreeCompStkRecord() { AgreeCompId = agreeCompId, FsDate = DateTime.Now, HotelId = UserContext.CurrentUser.HotelId });
        }

        /// <summary>
        /// 保存退款
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(AgreeCompStkRecord) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult TkEdit(AgreeCompStkRecord model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompStkRecordBll.AddTk(model, user.HotelId, user.Id, user.Name);
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

        #region 未结算明细

        public string GetWjsmxPager(int page, int rows, string agreeCompId)
        {
            var pager = AgreeCompWjsmxRecordBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, agreeCompId, 0);
            return JsonConvert.SerializeObject(pager);
        }

        public ActionResult _WjsmxJs(long id)
        {
            var model = AgreeCompWjsmxRecordBll.GetById(id);

            //获取支付方式
            var payTypeList = PayTypeBll.GetList(UserContext.CurrentUser.HotelId, true, true);
            var datas = new List<object>();
            foreach (var item in payTypeList)
            {
                datas.Add(new AgreeCompWjsmxJsDetail { WjsmxId = id, PayTypeId = item.Id, PayTypeName = item.Name, Money = 0 });
            }
            ViewBag.PayList = datas;
            return View(model);
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="model"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public JsonResult WjsmxJs(AgreeCompWjsmxRecord model, List<AgreeCompWjsmxJsDetail> details)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompWjsmxRecordBll.Js(model, details, user.Id, user.Name, user.HotelId);
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
        /// 商品入账
        /// </summary>
        /// <param name="agreeCompId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _WjsmxSprzEdit(string agreeCompId = "", long id = 0)
        {
            return View(AgreeCompWjsmxRecordBll.GetDetails(id, agreeCompId, UserContext.CurrentUser.HotelId));
        }

        /// <summary>
        /// 费用入账
        /// </summary>
        /// <param name="agreeCompId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _WjsmxFyrzEdit(string agreeCompId = "", long id = 0)
        {
            if (id == 0)
                return View(new AgreeCompWjsmxRecord() { AgreeCompId = agreeCompId, FsDate = DateTime.Now, RType = 2, HotelId = UserContext.CurrentUser.HotelId });
            else
                return View(AgreeCompWjsmxRecordBll.GetById(id));
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, Entitys = new Type[] { typeof(AgreeCompWjsmxRecord) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult WjsmxEdit(AgreeCompWjsmxRecord model, List<AgreeCompWjsmxRecordDetail> details = null)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompWjsmxRecordBll.AddOrUpdate(model, details, user.Id, user.Name, user.HotelId);
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
        /// 红冲费用
        /// </summary>
        /// <returns></returns>
        public ActionResult _WjsmxHch(long wjsmxId)
        {
            var wjsmx = AgreeCompWjsmxRecordBll.GetById(wjsmxId);
            ViewBag.AgreeCompId = wjsmx.AgreeCompId;
            ViewBag.AgreeCompShortName = wjsmx.AgreeCompShortName;
            return View(new AgreeCompWjsmxHchDetail() { WjsmxId = wjsmxId, HotelId = UserContext.CurrentUser.HotelId });
        }

        /// <summary>
        /// 红冲保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult WjsmxHch(AgreeCompWjsmxHchDetail model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompWjsmxRecordBll.WjsmxHch(model, user.Id, user.Name, user.HotelId);
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
        /// 查看红冲明细
        /// </summary>
        /// <param name="wjsmxId"></param>
        /// <returns></returns>
        public ActionResult _HchDetail(long wjsmxId)
        {
            var list = AgreeCompWjsmxRecordBll.GetHchList(wjsmxId);
            return View(list);
        }

        #endregion

        #region 已结算明细

        /// <summary>
        /// 已结算分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="agreeCompId"></param>
        /// <returns></returns>
        public string GetYjsmxPager(int page, int rows, string agreeCompId)
        {
            var pager = AgreeCompWjsmxRecordBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, agreeCompId, 1);
            return JsonConvert.SerializeObject(pager);
        }

        /// <summary>
        /// 结账收款明细
        /// </summary>
        /// <param name="wjsmxId"></param>
        /// <returns></returns>
        public ActionResult _Jzskmx(long wjsmxId)
        {
            var model = AgreeCompWjsmxRecordBll.GetById(wjsmxId);
            var list = AgreeCompWjsmxRecordBll.GetJzskmx(wjsmxId);
            ViewBag.PayList = list;
            return View(model);
        }

        #endregion

        #region 转账

        public string GetYskAndWjsList(string agreeCompId)
        {
            var list = new List<AgreeCompZzRecord>();
            long hotelId = UserContext.CurrentUser.HotelId;
            //获取未结算
            var wjsList = AgreeCompWjsmxRecordBll.GetWjsList(agreeCompId, hotelId);
            if (wjsList != null && wjsList.Count > 0)
            {
                foreach (var item in wjsList)
                {
                    list.Add(new AgreeCompZzRecord() { DetalId = item.Id, ZType = 3, RoomNO = item.RoomNO, Money = item.Money, FsDate = item.CDate, Remark = item.Remark });
                }
            }
            //获取预收款
            var yskList = AgreeCompStkRecordBll.GetList(agreeCompId, hotelId);
            if (yskList != null && yskList.Count > 0)
            {
                foreach (var item in yskList)
                {
                    list.Add(new
                    AgreeCompZzRecord()
                    {
                        DetalId = item.Id,
                        ZType = item.RType,
                        RoomNO = "",
                        Money = item.Money,
                        FsDate = TypeConvert.DateTimeToInt(item.FsDate),
                        Remark = item.Remark
                    });
                }
            }
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 搜索协议单位
        /// </summary>
        /// <returns></returns>
        public ActionResult _AgreeCompSearch()
        {
            return View();
        }

        public string GetAgreeCompList(string searchName = null)
        {
            var list = AgreeCompanyBll.GetList(UserContext.CurrentUser.HotelId, searchName);
            return JsonConvert.SerializeObject(list);
        }

        [HttpPost]
        public JsonResult ZzSave(string fromAgreeCompId, string toAgreeCompId, List<AgreeCompZzRecord> models)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                AgreeCompZzRecordBll.ZzSave(fromAgreeCompId, toAgreeCompId, models, user.Id, user.Name, user.HotelId);
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

        #region 结账界面  协议单位的选择

        public ActionResult _AgreeCompSearchForJz()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 佣金界面选择协议单位
        /// </summary>
        /// <returns></returns>
        public ActionResult _AgreeCompSearchForYj()
        {
            return View();
        }
    }
}