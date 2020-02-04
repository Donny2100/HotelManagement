using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class MemberController : AdminBaseController
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpGet]
        public ActionResult Edit(long id = 0)
        {
            if (id == 0)
                return View(new Member() { HotelId = UserContext.CurrentUser.HotelId });
            var info = MemberBll.GetById(id);
            return View(info);
        }

        public string GetPager(int page, int rows, string searchName = null)
        {
            var pager = MemberBll.GetPager(page, rows, UserContext.CurrentUser.HotelId, searchName);
            return JsonConvert.SerializeObject(pager);
        }



        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Member) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Member model)
        {
            var user = UserContext.CurrentUser;
            var apiResult = new APIResult();
            try
            {
                MemberBll.AddOrUpdate(model, user.HotelId, user.Id, user.Name);
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
                MemberBll.DeleteById(id);
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

        public JsonResult GetRechargeScheme(int money)
        {
            int zsMoney = 0;
            int zsExp = 0;
            //根据充值金额获取赠送积分和赠送金额
            var rechargeSchemeList = RechargeSchemeBll.GetList(UserContext.CurrentUser.HotelId).OrderByDescending(m => m.Money).ToList();
            if (rechargeSchemeList != null || rechargeSchemeList.Count != 0)
            {
                foreach (var item in rechargeSchemeList)
                {
                    if (money >= item.Money)
                    {
                        zsMoney = item.ZSMoney;
                        zsExp = item.ZSExp;
                        break;
                    }
                }
            }
            return Json(new { zsMoney = zsMoney, zsExp = zsExp }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _MemberRecharge(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Member) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult MemberRecharge(Member model)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                MemberRechargeRecordBll.AddOrUpdate(new MemberRechargeRecord()
                {
                    MemberId = model.Id,
                    PayTypeId = model.PayTypeId,
                    SkMoney = model.SkMoney,
                    CzMoney = model.CzMoney,
                }, user.HotelId, user.Id, user.Name);
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

        //积分兑换
        public ActionResult Jfdh(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Jfdh(long memberId, IEnumerable<MemberJfdhRecord> records)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                MemberJfdhRecordBll.AddList(memberId, records, user.HotelId, user.Id, user.Name);
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
        /// 积分调整
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Jftz(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(MemberJftzRecord) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Jftz(long id, string tzjf, string remark)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                try
                {
                    int.Parse(tzjf);
                }
                catch (Exception ex)
                {
                    throw new OperationExceptionFacade("调整积分必须是整数，且不可为0");
                }
                if (int.Parse(tzjf) == 0)
                    throw new OperationExceptionFacade("调整积分必须是整数，且不可为0");
                MemberJftzRecordBll.AddSingle(new MemberJftzRecord()
                {
                    MemberId = id,
                    TzExp = int.Parse(tzjf),
                    Remark = remark,
                    HandlerId = user.Id,
                    HandlerName = user.Name,
                    HotelId = user.HotelId,
                });
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
        /// 会员挂失
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Hygs(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Hygs(long id)
        {
            var apiResult = new APIResult();
            try
            {
                MemberBll.SetState(id, 2);
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
        /// 会员换卡
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Hyhk(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Hyhk(long id, string newCardNO, long payTypeId, string hkMoney)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                if (string.IsNullOrWhiteSpace(newCardNO))
                    throw new OperationExceptionFacade("新卡号不可为空");
                try
                {
                    decimal.Parse(hkMoney);
                }
                catch (Exception ex)
                {
                    throw new OperationExceptionFacade("收款金额数据格式错误");
                }
                if (decimal.Parse(hkMoney) <= 0)
                    throw new OperationExceptionFacade("收款金额数据格式错误");
                MemberBll.Hyhk(id, newCardNO, payTypeId, decimal.Parse(hkMoney), user.Id, user.Name, user.HotelId);
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
        /// 会员续卡
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Hyxk(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Hyxk(long id, string days, long payTypeId, string hkMoney)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                try
                {
                    int.Parse(days);
                }
                catch (Exception ex)
                {
                    throw new OperationExceptionFacade("延长天数格式错误");
                }
                if (int.Parse(days) <= 0)
                    throw new OperationExceptionFacade("延长天数格式错误");

                try
                {
                    decimal.Parse(hkMoney);
                }
                catch (Exception ex)
                {
                    throw new OperationExceptionFacade("收款金额数据格式错误");
                }
                if (decimal.Parse(hkMoney) <= 0)
                    throw new OperationExceptionFacade("收款金额数据格式错误");
                MemberBll.Hyxk(id, int.Parse(days), payTypeId, decimal.Parse(hkMoney), user.Id, user.Name, user.HotelId);
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
        /// 会员退卡
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Hytk(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Hytk(long id, string backMoney, long payTypeId, string remark)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                try
                {
                    decimal.Parse(backMoney);
                }
                catch (Exception ex)
                {
                    throw new OperationExceptionFacade("退款金额数据格式错误");
                }
                if (decimal.Parse(backMoney) <= 0)
                    throw new OperationExceptionFacade("退款金额数据格式错误");
                MemberBll.Hytk(id, payTypeId, decimal.Parse(backMoney), remark, user.Id, user.Name, user.HotelId);
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
        /// 会员升级
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Hysj(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Hysj(long id, long toMemberTypeId, string kcExp)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                try
                {
                    int.Parse(kcExp);
                }
                catch (Exception ex)
                {
                    throw new OperationExceptionFacade("扣除积分数据格式错误");
                }
                if (int.Parse(kcExp) <= 0)
                    throw new OperationExceptionFacade("扣除积分数据格式错误");
                MemberBll.Hysj(id, toMemberTypeId, int.Parse(kcExp), user.Id, user.Name, user.HotelId);
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
        /// 密码重置
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ActionResult _Pwd(long memberId)
        {
            var model = MemberBll.GetById(memberId);
            if (model == null)
                model = new Member() { HotelId = UserContext.CurrentUser.HotelId };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = false, LogWay = OprtLogType.新增和修改)]
        public JsonResult Pwd(long id, string pwd1, string pwd2)
        {
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            try
            {
                MemberBll.Pwd(id, pwd1, pwd2);
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
        /// 3、下载导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public APIResult DownLoadMemberExcelMode()
        {
            var apiResult = new APIResult();
            try
            {
                DataTable table = new DataTable();
                DataColumn cardNO = new DataColumn("会员卡号");
                DataColumn name = new DataColumn("姓名");
                DataColumn memberType = new DataColumn("会员类型");
                DataColumn tel = new DataColumn("手机号");

                table.Columns.Add(cardNO);
                table.Columns.Add(name);
                table.Columns.Add(memberType);
                table.Columns.Add(tel);
                DataRow row = table.NewRow();
                row["会员卡号"] = "必填";
                row["姓名"] = "必填";
                row["会员类型"] = "必填";
                row["手机号"] = "必填";
                table.Rows.Add(row);
                ExcelHelper.ExportByWeb(table, "会员导入模板", "会员导入模板.xls");
            }
            catch (OperationExceptionFacade ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }

        /// <summary>
        /// 导入会员excel数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ImportMember()
        {

            var apiResult = new APIResult();
            DataTable dt = new DataTable();
            try
            {
                dt = ExcelHelper.gjImport(Cookies.GetCookie("ExcelPath"));
                //dt.Rows.RemoveAt(0);
            }
            catch (Exception)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "导入失败,请重试";
                try
                {
                    Directory.Delete(Cookies.GetCookie("Way"), true);
                }
                catch
                {
                }
                return Json(apiResult);
            }
            if (dt.Rows.Count < 3)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "您选择的Excel文件不符合格式或者没有内容!";
                Directory.Delete(Cookies.GetCookie("Way"), true);
                return Json(apiResult);
            }
            string msg = string.Empty;
            int ret = MemberBll.DrExcel(dt, UserContext.CurrentUser.HotelId, ref msg);
            Directory.Delete(Cookies.GetCookie("Way"), true);
            apiResult.Ret = ret;
            apiResult.Msg = msg;
            return Json(apiResult);
        }

        //上传文件的路径
        //private static string ExcelPath;//EXCEL的路径，用于导入时候读取
        //private static string way;//EXCEL所在文件夹的路径，用于导入结束删除
        public void Upload(HttpPostedFileBase Filedata)
        {
            // 如果没有上传文件
            if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
            {
                return;
            }
            // 保存到 ~/photos 文件夹中，名称不变
            string nowTime;
            try
            {
                nowTime = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString() + UserContext.CurrentUser.Id;
            }
            catch
            {
                nowTime = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();
            }
            string filename = System.IO.Path.GetFileName(Filedata.FileName);
            string virtualPath = string.Format("~/uploads/{0}/{1}", nowTime, filename);
            // 文件系统不能使用虚拟路径
            string path = this.Server.MapPath(virtualPath);
            //判断文件路径是否存在，否则创建路径
            string way = this.Server.MapPath(string.Format("~/uploads/{0}", nowTime));
            if (!Directory.Exists(way))
            {
                Directory.CreateDirectory(way);
            }
            Filedata.SaveAs(path);//上传
            string excelPath = path;//上传文件的路径
            NIU.Core.Cookies.WriteCookie("ExcelPath", excelPath);
            NIU.Core.Cookies.WriteCookie("Way", way);
            return;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public JsonResult ToExcel()
        {
            var user = UserContext.CurrentUser;
            var list = MemberBll.GetList(user.HotelId);
            var tb = new DataTable();
            tb.Columns.Add("会员卡号");
            tb.Columns.Add("卡内码");
            tb.Columns.Add("状态");
            tb.Columns.Add("会员类型");
            tb.Columns.Add("余额");
            tb.Columns.Add("积分");
            tb.Columns.Add("入住次数");
            tb.Columns.Add("姓名");
            tb.Columns.Add("性别");
            tb.Columns.Add("生日");
            tb.Columns.Add("手机号");
            tb.Columns.Add("证件类型");
            tb.Columns.Add("证件号码");
            tb.Columns.Add("地址");
            tb.Columns.Add("营销人员");
            tb.Columns.Add("营销短信");
            tb.Columns.Add("长期有效");
            tb.Columns.Add("到期日期");
            tb.Columns.Add("操作员");
            tb.Columns.Add("发卡时间");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    tb.Rows.Add(new string[] {
                        item.CardNO,
                        item.CNM,
                        item.StateName,
                        item.MemberTypeName,
                        item.Money.ToString(),
                        item.Exp.ToString(),
                        item.Times.ToString(),
                        item.Name,
                        item.Sex,
                        item.Birth.ToShortDateString(),
                        item.Tel,
                        item.CertificateTypeName,
                        item.CertificateTypeNO,
                        item.Address,item.YxryName,
                        (item.IsNotYxSms?"不接收":"接受"),
                        (item.IsCqyx?"是":"否"),
                        (item.IsCqyx?"":item.ExpireDate.ToShortDateString()),
                        item.HandlerName,
                        NIU.Forum.Common.TypeConvert.IntToDateTime(item.CDate).ToString("yyyy-MM-dd hh:mm:ss")
                    });
                }
            }
            var apiResult = new APIResult();
            //try
            //{
            ExcelHelper.ExportByWeb(tb, "会员信息表", "会员信息表.xls");
            //}
            //catch (Exception ex)
            //{
            //    apiResult.Ret = -1;
            //    apiResult.Msg = ex.Message;
            //    if (!(ex is OperationExceptionFacade))
            //        LogFactory.GetLogger().Log(LogLevel.Error, ex);
            //}

            return Json(apiResult);
        }

        /// <summary>
        /// 拍照上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadPhoto()
        {
            var stream = Request.InputStream;

            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            System.Drawing.Bitmap bmp = new Bitmap(image);
            string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            var path = Server.MapPath($"~/upload/{UserContext.CurrentUser.HotelId}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            bmp.Save(path + "/" + filename);

            var result = new { error = false, filename = $"/upload/{UserContext.CurrentUser.HotelId}/{filename}", message = "" };

            return Json(result);
        }

        /// <summary>
        /// 选择会员
        /// </summary>
        /// <returns></returns>
        public ActionResult _MemberSearch()
        {
            return View();
        }

        /// <summary>
        /// 选择会员--结账
        /// </summary>
        /// <returns></returns>
        public ActionResult _MemberSearchForJz()
        {
            return View();
        }

        public string GetMemberList(string searchName = "")
        {
            var list = MemberBll.GetList(searchName, UserContext.CurrentUser.HotelId);
            return JsonConvert.SerializeObject(list);
        }
    }
}