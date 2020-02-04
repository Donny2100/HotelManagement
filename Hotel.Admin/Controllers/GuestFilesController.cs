using Hotel.Bll;
using Hotel.Model;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class GuestFilesController : AdminBaseController
    {
        // GET: GuestFiles
        public ActionResult Index(string GuestType = nameof(GuestInfoCN))
        {
            ViewBag.CurrentGuestType = GuestType;
            return View();
        }

        public string GetPager(int page, int rows, string GuestType = nameof(GuestInfoCN), string searchName = "")
        {

            if (GuestType == nameof(GuestInfoCN))
            {
                var pager = GuestInfoCNBll.GetPager2(page, rows, UserContext.CurrentUser.HotelId, searchName);
                return JsonConvert.SerializeObject(pager);
            }
            else
            {
                var pager = GuestInfoENBll.GetPager2(page, rows, UserContext.CurrentUser.HotelId, searchName);
                return JsonConvert.SerializeObject(pager);
            }

        }


        /// <summary>
        /// 新增/编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(long id, string GuestType = nameof(GuestInfoCN))
        {
            GuestInfoCN infoCN = null;
            GuestInfoEN infoEN = null;
            var hotelId = UserContext.CurrentUser.HotelId;
            if (id == 0)
            {
                return View();
            }
            else
            {
                switch (GuestType)
                {
                    case nameof(GuestInfoCN):
                        infoCN = GuestInfoCNBll.SingleOrDefault(id);
                        if (infoCN != null)
                        {
                            List<Model.RoomReg> rrData = RoomRegBll.GetListBySql(GuestType, hotelId.ToString(), "GuestInfoId", infoCN.Id.ToString());
                            if (rrData != null)
                            {
                                infoCN.RzCount = rrData.Count;
                                infoCN.LjRzCount = rrData.Sum(t => t.RuzhuDays);
                                infoCN.LsXfPrice = rrData.Sum(t => t.Yszk);
                            }
                            else
                            {
                                infoCN.RzCount = 0;
                                infoCN.LjRzCount = 0;
                                infoCN.LsXfPrice = 0;
                            }
                        }
                        break;
                    case nameof(GuestInfoEN):

                        infoEN = GuestInfoENBll.SingleOrDefault(id);
                        if (infoEN != null)
                        {
                            List<Model.RoomReg> rrData2 = RoomRegBll.GetListBySql(GuestType, hotelId.ToString(), "GuestInfoId", infoEN.Id.ToString());
                            if (rrData2 != null)
                            {
                                infoEN.RzCount = rrData2.Count;
                                infoEN.LjRzCount = rrData2.Sum(t => t.RuzhuDays);
                                infoEN.LsXfPrice = rrData2.Sum(t => t.Yszk);
                            }
                            else
                            {
                                infoEN.RzCount = 0;
                                infoEN.LjRzCount = 0;
                                infoEN.LsXfPrice = 0;
                            }
                        }
                        break;
                }
            }
            ViewBag.infoCN = infoCN;
            ViewBag.infoEN = infoEN;
            return View();

        }
        /// <summary>
        /// 设置拉黑/白名单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reason"></param>
        /// <param name="isdisable"></param>
        /// <param name="GuestType"></param>
        /// <returns></returns>
        public string SetUserType(long id, string reason, string GuestType = nameof(GuestInfoCN))
        {
            if (GuestType == nameof(GuestInfoCN))
            {
                GuestInfoCNBll.SetDisable(new GuestInfoCN() { Id = id, DisableReason = reason });
            }
            else
            {
                GuestInfoENBll.SetDisable(new GuestInfoEN() { Id = id, DisableReason = reason });
            }
            return JsonConvert.SerializeObject("设置成功");
        }


        /// <summary>
        /// 保存新增-境内
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult EditCN(RoomRegGuestInfoCN model)
        {
            var apiResult = new APIResult();
            int code = 0;
            try
            {
                long guestInfoId = 0;
                model.HotelId = UserContext.CurrentUser.HotelId;
                GuestInfoCNBll.AddOrUpdateGuest(model, new Hotel.Model.RoomReg()
                {
                    CDate = 0,
                    RoomId = 0,
                    RoomNO = "",
                    RoomPrice = 0,
                    Id = 0,
                }, ref guestInfoId);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            if (code == -100)
                apiResult.Ret = code;
            return Json(apiResult);
        }

        /// <summary>
        /// 保存--境外
        /// </summary>
        [HttpPost]
        public JsonResult EditEN(RoomRegGuestInfoEN model)
        {
            var apiResult = new APIResult();
            int code = 0;
            try
            {
                long guestInfoId = 0;
                model.HotelId = UserContext.CurrentUser.HotelId;
                GuestInfoENBll.AddOrUpdateGuest(model, new Hotel.Model.RoomReg()
                {
                    CDate = 0,
                    RoomId = 0,
                    RoomNO = "",
                    RoomPrice = 0,
                    Id = 0,
                }, ref guestInfoId);
            }
            catch (Exception ex)
            {
                apiResult.Ret = -1;
                apiResult.Msg = ex.Message;
                if (!(ex is OperationExceptionFacade))
                    LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
            if (code == -100)
                apiResult.Ret = code;
            return Json(apiResult);
        }
        /// <summary>
        /// 历史入住搜索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type">GuestInfoCN,GuestInfoEN</param>
        /// <returns></returns>
        public string Search(int page, int rows, string key, string value, string type)
        {

            var hotelId = UserContext.CurrentUser.HotelId;


            //倒推-->guest_info_cn.ID-->Room_Reg_Guest_Info_CN.GuestInfoID---->RoomReg
            var datas = RoomRegBll.Page(page, rows, RoomRegBll.GetCommonSql(type, hotelId.ToString(), "GuestInfoId", value));
            List<Model.RoomReg> items = new List<Model.RoomReg>();

            if (datas != null && datas.Items != null)
            {
                if (type == nameof(GuestInfoCN))
                {
                    GuestInfoCN info = GuestInfoCNBll.SingleOrDefault($" where Id = '{value}'");
                    foreach (var item in datas.Items)
                    {
                        item.Name = info.Name;
                        item.Sex = info.Sex;
                        item.CertificateTypeName = info.CertificateTypeName;
                        item.CertificateNO = info.CertificateNO;
                        items.Add(item);
                    }
                }
                else
                {
                    GuestInfoEN info = GuestInfoENBll.SingleOrDefault($" where Id = '{value}'");
                    foreach (var item in datas.Items)
                    {
                        item.Name = info.FirstName + ' ' + info.LastName;
                        item.Sex = info.Sex;
                        item.CertificateTypeName = info.CertificateTypeName;
                        item.CertificateNO = info.CertificateNO;
                        items.Add(item);
                    } 
                }

            }

            return JsonConvert.SerializeObject(new Pager<Model.RoomReg>() { total = datas.TotalItems, rows = items });


            //if (type == nameof(GuestInfoCN))
            //{
            //    var datas = GuestInfoCNRzRecordBll.Page(page, rows, $"where LskrId = {value} ");
            //    var pager = new Pager<Hotel.Model.GuestInfoCNRzRecord>() { total = datas.TotalItems, rows = datas.Items };
            //    return JsonConvert.SerializeObject(pager);
            //}
            //else
            //{
            //    var datas = GuestInfoENRzRecordBll.Page(page, rows, $"where LskrId = {value} ");
            //    var pager = new Pager<Hotel.Model.GuestInfoENRzRecord>() { total = datas.TotalItems, rows = datas.Items };
            //    return JsonConvert.SerializeObject(pager);
            //} 
        }
        /// <summary>
        /// index导出excel
        /// </summary>
        /// <param name="GuestType"></param>
        /// <returns></returns>
        public JsonResult ToExcel(string GuestType = nameof(GuestInfoCN))
        {
            var tb = new DataTable();
            tb.Columns.Add("姓名");
            tb.Columns.Add("英文名");
            tb.Columns.Add("证件类型");
            tb.Columns.Add("证件号");
            tb.Columns.Add("性别");
            tb.Columns.Add("手机");
            tb.Columns.Add("地址");
            tb.Columns.Add("特殊要求");
            tb.Columns.Add("是否是黑名单");
            tb.Columns.Add("黑名单理由");
            tb.Columns.Add("备注");
            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
            switch (GuestType)
            {
                case nameof(GuestInfoCN):

                    var list = GuestInfoCNBll.GetList(user.HotelId);
                    foreach (var item in list)
                    {
                        tb.Rows.Add(new string[] {
                            item.Name,
                            "",
                            item.CertificateTypeName,
                            item.CertificateNO,
                            item.Sex,
                            item.Tel,
                            item.Address,
                             GuestFilesBll.GetTssXHtml(item.Xh,item.Tx,item.Sh,item.Zw,item.Sw,item.Ts,item.Qt),
                            item.IsDisable==true? "是":"否",
                            item.DisableReason,
                            item.Remark
                        });
                    }
                    break;

                case nameof(GuestInfoEN):

                    var list2 = GuestInfoENBll.GetList(user.HotelId);
                    foreach (var item in list2)
                    {
                        tb.Rows.Add(new string[] {
                            item.Name,
                            item.FirstName+ " "+ item.LastName,
                            item.CertificateTypeName,
                            item.CertificateNO,
                            item.Sex,
                            item.Tel,
                            "",
                            GuestFilesBll.GetTssXHtml(item.Xh,item.Tx,item.Sh,item.Zw,item.Sw,item.Ts,item.Qt),
                            item.IsDisable==true? "是":"否",
                            item.DisableReason,
                            item.Remark
                        });
                    }
                    break;
            }


            ExcelHelper.ExportByWeb(tb, "客人档案信息", "客人档案信息表.xls");
            return Json(apiResult);
        }

        /// <summary>
        /// 消费历史。导出excel
        /// </summary>
        /// <param name="GuestType"></param>
        /// <returns></returns>
        public JsonResult ToGestExcel(string GuestInfoID, string GuestType = nameof(GuestInfoCN))
        {
            var tb = new DataTable();
            tb.Columns.Add("单据号");
            tb.Columns.Add("房型"); 
            tb.Columns.Add("房间号");
            tb.Columns.Add("客户来源");
            tb.Columns.Add("客人姓名");
            tb.Columns.Add("性别");
            tb.Columns.Add("证件类型"); 
            tb.Columns.Add("证件编号");
            tb.Columns.Add("入住天数");
            tb.Columns.Add("应收账款");
            tb.Columns.Add("预收账款");
            tb.Columns.Add("入住时间");
            tb.Columns.Add("离店时间"); 

            var apiResult = new APIResult();
            var user = UserContext.CurrentUser;
        
            List<Hotel.Model.RoomReg> list = RoomRegBll.GetListBySql(GuestType, user.HotelId.ToString(), "GuestInfoId", GuestInfoID);
            if (list != null )
            {
                if (GuestType == nameof(GuestInfoCN))
                {
                    GuestInfoCN info = GuestInfoCNBll.SingleOrDefault($" where Id = '{GuestInfoID}'");
                    foreach (var item in list)
                    {
                        item.Name = info.Name;
                        item.Sex = info.Sex;
                        item.CertificateTypeName = info.CertificateTypeName;
                        item.CertificateNO = info.CertificateNO;
                        tb.Rows.Add(new string[] {
                            item.DanJuNum,
                            item.RoomTypeName,
                            item.RoomNO,
                            item.GuestSourceName,
                            item.Name,
                            item.Sex,
                            item.CertificateTypeName,
                            item.CertificateNO,
                            item.RuzhuDays.ToString(),
                            item.Yszk.ToString("F2"),
                            item.Yuszk.ToString("F2"),
                            TypeConvert.IntToDateTime(item.RegTime).ToString("yyyy-MM-dd HH:mm:ss"),
                            TypeConvert.IntToDateTime(item.LeaveTime).ToString("yyyy-MM-dd HH:mm:ss"), 
                        });
                    }
                }
                else
                {
                    GuestInfoEN info = GuestInfoENBll.SingleOrDefault($" where Id = '{GuestInfoID}'");
                    foreach (var item in list)
                    {
                        item.Name = info.FirstName + ' ' + info.LastName;
                        item.Sex = info.Sex;
                        item.CertificateTypeName = info.CertificateTypeName;
                        item.CertificateNO = info.CertificateNO;
                        tb.Rows.Add(new string[] {
                            item.DanJuNum,
                            item.RoomTypeName,
                            item.RoomNO,
                            item.GuestSourceName,
                            item.Name,
                            item.Sex,
                            item.CertificateTypeName,
                            item.CertificateNO,
                            item.RuzhuDays.ToString(),
                            item.Yszk.ToString("F2"),
                            item.Yuszk.ToString("F2"),
                            TypeConvert.IntToDateTime(item.RegTime).ToString("yyyy-MM-dd HH:mm:ss"),
                            TypeConvert.IntToDateTime(item.LeaveTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        });
                    }
                }

            }
            //if (GuestType == nameof(GuestInfoCN))
            //{

            //    var rows  =  GuestInfoCNRzRecordBll.Fetch($"where LskrId = '{GuestInfoID}' ");
            //    foreach (var item in rows)
            //    {
            //        tb.Rows.Add(new string[] {
            //                item.Name,
            //                "",
            //                //item.CertificateTypeName,
            //                //item.CertificateNO,
            //                item.RoomNO,
            //                TypeConvert.StrToDateTime( item.RegTime).ToString("yyyy-MM-dd HH:mm:ss"),
            //                TypeConvert.StrToDateTime(   item.LeaveTime).ToString("yyyy-MM-dd HH:mm:ss"),
            //            });
            //    }
            //}
            //else
            //{
            //    var rows = GuestInfoENRzRecordBll.Fetch($"where LskrId = '{GuestInfoID}' ");
            //    foreach (var item in rows)
            //    {
            //        tb.Rows.Add(new string[] {
            //                item.Name,
            //                item.FirstName+" "+item.LastName,
            //                //item.CertificateTypeName,
            //                //item.CertificateNO,
            //                item.RoomNO,
            //                TypeConvert.StrToDateTime( item.RegTime).ToString("yyyy-MM-dd HH:mm:ss"),
            //                TypeConvert.StrToDateTime(   item.LeaveTime).ToString("yyyy-MM-dd HH:mm:ss"),
            //            });
            //    }
            //}


            ExcelHelper.ExportByWeb(tb, "客人入住历史档案信息", "客人入住历史档案信息表.xls");
            return Json(apiResult);
        }
    }
}