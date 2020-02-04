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
        public ActionResult _Yh(long consumeId,long roomRegId)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");

            ViewBag.ConsumeId = consumeId;
            ViewBag.RoomRegId = roomRegId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            var data = new PosConsume()
            {
                Id = consumeId
            };
            return View(data);
        }

        public JsonResult YhEdit(PosConsume model)
        {
            var apiResult = new APIResult();
            try
            { 
                PosConsumeBll.YhEdit(model);

                apiResult.SeqId = model.Id;
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