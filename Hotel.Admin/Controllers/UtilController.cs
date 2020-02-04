using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.Controllers
{
    public class UtilController : Controller
    {
        // GET: Util


        #region 接口

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            ValidationCode vCode = new ValidationCode();
            var validateCode = new NIU.Core.VerifyCode { Length = 4, FontSize = 16, EnableChaos = true, EnableTwistImage = true };
            var code = validateCode.CreateVerifyCode();
            Session[vCode.SessionName] = code.ToUpper();
            Session.Timeout = 5;
            var bytes = validateCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        #endregion
    }
}