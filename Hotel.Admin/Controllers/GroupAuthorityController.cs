using Hotel.Bll;
using Newtonsoft.Json;
using NIU.Common.BLL;
using NIU.Common.BLL.Right;
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
    public class GroupAuthorityController : AdminBaseController
    {
        // GET: GroupAuthority
        public ActionResult Index()
        {
            ViewBag.HotelId = UserContext.CurrentUser.HotelId;
            return View();
        }

        public string GetList(long groupId = 0)
        {
            var data = GroupAuthorityBll.GetList(groupId);
            return JsonConvert.SerializeObject(data);
        }

        public JsonResult SaveAuth(long groupId, GroupAuthModel[] models)
        {
            var apiResult = new APIResult();
            if (groupId == 1)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "不可设置平台管理员权限";
                return Json(apiResult);
            }
            List<GroupAuthority> authList = new List<GroupAuthority>();
            if (models.Length == 0)
            {
                apiResult.Ret = -1;
                apiResult.Msg = "请选择权限";
                return Json(apiResult);
            }
            //获取所有菜单
            var menuList = MenuBll.GetMenus();
            //获取所有按钮
            var btnList = FuncBtnBll.GetList();

            try
            {
                foreach (var item in models)
                {
                    long menuId = item.id;
                    var menu = menuList.FirstOrDefault(m => m.Id == menuId);
                    if (menu == null)
                        continue;
                    string btnsStr = item.btnIdStr;
                    if (string.IsNullOrWhiteSpace(btnsStr))
                        continue;
                    string[] btnIdArray = btnsStr.Split(',');
                    if (btnIdArray.Length == 0)
                        continue;
                    foreach (var btnIdString in btnIdArray)
                    {
                        long btnId = 0;
                        if (long.TryParse(btnIdString, out btnId))
                        {
                            var funcBtn = btnList.FirstOrDefault(m => m.Id == btnId);
                            authList.Add(new GroupAuthority()
                            {
                                GroupId = groupId,
                                MenuId = menuId,
                                MenuController = menu.MenuController,
                                FuncBtnId = funcBtn.Id,
                                FuncBtnName = funcBtn.Name,
                                FuncBtnCode = funcBtn.Code,
                                Seq = funcBtn.Seq,
                                CDate = TypeConvert.DateTimeToInt(DateTime.Now)
                            });
                        }
                    }
                }

                if (authList.Count == 0)
                {
                    GroupAuthorityBll.Delete(groupId);
                }
                else {
                    GroupAuthorityBll.Delete(groupId);
                    authList.ForEach(m=> {
                        GroupAuthorityBll.AddOrUpdate(m);
                    });
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
    }

    public class GroupAuthModel
    {
        public long id { set; get; }

        public string btnIdStr { set; get; }
    }
}