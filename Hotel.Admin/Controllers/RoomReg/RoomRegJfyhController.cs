using Hotel.Admin.App_Start;
using Hotel.Bll;
using Hotel.Model;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers.RoomReg
{
    public class RoomRegJfyhController : AdminBaseController
    {
        // GET: RoomRegJfyh
        public ActionResult Index(long roomRegId, long itemId = 0)
        {
            //获取前台优惠限额
            var hotelId = UserContext.CurrentUser.HotelId;
            var roomReg = RoomRegBll.GetById(roomRegId);
            if (roomReg == null)
                return Content("房间登记数据不存在");
            if (roomReg.CustomerType != 3)
                return Content("登记客人非会员，不可操作");
            if (string.IsNullOrWhiteSpace(roomReg.MemCompId))
                return Content("房间登记未选择会员");
            var member = MemberBll.GetById(long.Parse(roomReg.MemCompId));
            if (member == null)
                return Content("会员不存在");
            var memType = MemberTypeBll.GetById(member.MemberTypeId);
            if (memType == null)
                memType = new MemberType();
            ViewBag.MemberType = memType;
            if (itemId == 0)
            {
                //新增
                return View(new RoomRegJfyh()
                {
                    HotelId = hotelId,
                    Name = member.Name,
                    Sex = member.Sex,
                    Birth = member.Birth.ToString("yyyy-MM-dd"),
                    RoomRegId = roomRegId,
                    KyExp = (int)member.Exp
                });
            }
            else
            {
                //修改
                var model = RoomRegJfyhBll.GetById(itemId);
                if (model == null)
                    model = new RoomRegJfyh();
                return View(model);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(RoomRegJfyh) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(RoomRegJfyh model)
        {
            var apiResult = new APIResult();
            try
            {
                var user = UserContext.CurrentUser;
                RoomRegJfyhBll.AddOrUpdate(model, user.Id, user.Name, user.HotelId);
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
                RoomRegJfyhBll.DeleteById(id);
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