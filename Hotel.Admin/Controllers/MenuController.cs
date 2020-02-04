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
    /// <summary>
    /// 菜单管理只能是平台用户中有权限的人才可以操作
    /// </summary>
    public class MenuController : AdminBaseController
    {
        // GET: Menu

        #region 试图

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
                return View(new Menu());
            var info = MenuBll.GetById(id);
            return View(info);
        }
        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        [OprtLogFilter(IsRecordLog = true, Method = "", IsFormPost = true, Entitys = new Type[] { typeof(Menu) }, LogWay = OprtLogType.新增和修改)]
        public JsonResult Edit(Menu model)
        {
            if (model.Id == 0)
            {
                model.CDate = TypeConvert.DateTimeToInt(DateTime.Now);
                model.HasChild = false;
            }
            var apiResult = new APIResult();
            try
            {
                if (model.Id == 0)
                {
                    model.Id = IdBuilder.NextLongID();
                    MenuBll.Insert(model);
                    //保存菜单按钮
                    if (!string.IsNullOrWhiteSpace(model.BtnNames))
                    {
                        string[] btnIdArr = model.BtnNames.Split(',');
                        if (btnIdArr.Length > 0)
                        {
                            foreach (var btnIdStr in btnIdArr)
                            {
                                long btnId = 0;
                                if (long.TryParse(btnIdStr,out btnId)) {
                                    var menuFuncBtn = new MenuFuncBtn() {
                                        Id=IdBuilder.NextLongID(),
                                        MenuId= model.Id,
                                        FuncBtnId = btnId,
                                        CDate=TypeConvert.DateTimeToInt(DateTime.Now)
                                    };
                                    MenuFuncBtnBll.AddOrUpdate(menuFuncBtn);
                                }
                            }
                        }
                    }
                }

                else
                {
                    MenuBll.Update(model);
                    //删除菜单按钮
                    MenuFuncBtnBll.Delete(model.Id);
                    //保存菜单按钮
                    if (!string.IsNullOrWhiteSpace(model.BtnNames))
                    {
                        string[] btnIdArr = model.BtnNames.Split(',');
                        if (btnIdArr.Length > 0)
                        {
                            foreach (var btnIdStr in btnIdArr)
                            {
                                long btnId = 0;
                                if (long.TryParse(btnIdStr, out btnId))
                                {
                                    var menuFuncBtn = new MenuFuncBtn()
                                    {
                                        Id = IdBuilder.NextLongID(),
                                        MenuId = model.Id,
                                        FuncBtnId = btnId,
                                        CDate = TypeConvert.DateTimeToInt(DateTime.Now)
                                    };
                                    MenuFuncBtnBll.AddOrUpdate(menuFuncBtn);
                                }
                            }
                        }
                    }
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

        #endregion

        #region 接口

        [HttpPost]
        public string GetList()
        {
            var parentList = GetMenuListBy(0);
            parentList.ForEach(m =>
            {
                var kids = GetMenuListBy(m.Id);
                m.children = kids;
            });
            return JsonConvert.SerializeObject(parentList);
            //return Json(parentList);
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
                MenuBll.Delete(id);
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

        public string GetMenuBtnList(long id) {
            var btnList = MenuBll.GetMenuBtnList(id);
            return JsonConvert.SerializeObject(btnList);
        }

        #endregion


        #region 方法

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Menu> GetMenuListBy(long pid)
        {
            //获取所有菜单
            var menuList = MenuBll.GetAllList();
            var datas = menuList.Where(m => m.Pid == pid).OrderBy(m => m.Seq).ToList();
            if (datas == null || !datas.Any())
                return new List<Menu>();
            return datas;
        }

        #endregion
    }
}