
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
        public ActionResult _Serve(long consumeId,decimal Amount,long roomRegId)
        {
            var now = DateTime.Now;
            ViewBag.CurrentTime = now.ToString("yyyy-MM-dd HH:mm:ss");

            ViewBag.ConsumeId = consumeId;
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;

            ViewBag.RoomRegId = roomRegId;
            var consume = PosConsumeBll.GetById(consumeId);
            var pos = PosDefineBll.GetById(consume.PosId);

            ViewBag.Amount = Amount;
            ViewBag.ServiceChargePer = pos.ServiceChargePer;
            ViewBag.DefaultAmount = Convert.ToInt32(Amount * (decimal)0.01 * pos.ServiceChargePer);

            var data = new PosConsume()
            {
                Id = consumeId
            };

            data.ServeAmount = Convert.ToInt32(Amount * (decimal)0.01 * pos.ServiceChargePer);

            return View(data);
        }

        public JsonResult ServeEdit(PosConsume model)
        {
            var apiResult = new APIResult();
            try
            { 
                PosConsumeBll.ServeEdit(model);

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