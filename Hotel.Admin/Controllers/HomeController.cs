using Hotel.Bll;
using Hotel.Model;
using NIU.Common.BLL;
using NIU.Common.BLL.Right;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Threading;

namespace Hotel.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public ActionResult PrintTest()
        {
            return View();
        }
        public ActionResult _PrintTestContent()
        {
            return View();
        }
        public ActionResult Index()
        {
            List<Menu> topMenuList = new List<Menu>();//页面顶部菜单
            List<Menu> leftMenuList = new List<Menu>();//页面左侧菜单


            
            

            if (UserContext.CurrentUser.GId == 1)
            {
                //如果是平台管理员
                //获取父级菜单
                var parentMenuList = GetMenuListBy(0);
                //获取第一个菜单的子级
                var kidMenuList = new List<Menu>();
                if (parentMenuList != null && parentMenuList.Any())
                {
                    var first = parentMenuList[0];
                    kidMenuList = GetMenuListBy(first.Id);
                    if (kidMenuList == null)
                        kidMenuList = new List<Menu>();
                }
                ViewBag.User = UserContext.CurrentUser;
                ViewBag.KidMenuList = kidMenuList;
                topMenuList = parentMenuList;
            }
            else
            {
                //如果不是平台管理员
                //获取用户权限
                var groupAuthList = GroupAuthorityBll.GetGroupAuthorityList(UserContext.CurrentUser.GId);
                if (groupAuthList != null && groupAuthList.Any())
                {
                    //获取所有菜单
                    var menuList = MenuBll.GetMenus();
                    var parentList = menuList.Where(m => m.Pid == 0);
                    foreach (var menu in parentList)
                    {
                        //获取所有的子菜单
                        var kidList = menuList.Where(m => m.Pid == menu.Id);
                        if (kidList == null || !kidList.Any())
                            continue;
                        foreach (var auth in groupAuthList)
                        {
                            if (kidList.FirstOrDefault(m => m.Id == auth.MenuId) != null)
                            {
                                if (topMenuList.FirstOrDefault(m => m.Id == menu.Id) == null)
                                {
                                    topMenuList.Add(menu);
                                }
                            }
                        }
                    }
                    if (topMenuList.Any())
                    {
                        var first = topMenuList[0];
                        var kidList = menuList.Where(m => m.Pid == first.Id);
                        foreach (var auth in groupAuthList)
                        {
                            var menu = kidList.FirstOrDefault(m => m.Id == auth.MenuId);
                            if (menu != null && !leftMenuList.Exists(m => m.Id == menu.Id))
                            {
                                leftMenuList.Add(menu);
                            }
                        }
                    }
                }

                ViewBag.User = UserContext.CurrentUser;
                ViewBag.KidMenuList = leftMenuList;
            }


            ViewBag.PosEnabledMenus = string.Join(",", PosDefineBll.GetEnabledMenus().ToArray());
            return View(topMenuList);
        }

        public ActionResult Test()
        {
            List<Menu> topMenuList = new List<Menu>();//页面顶部菜单
            List<Menu> leftMenuList = new List<Menu>();//页面左侧菜单


            if (UserContext.CurrentUser.GId == 1)
            {
                //如果是平台管理员
                //获取父级菜单
                var parentMenuList = GetMenuListBy(0);
                //获取第一个菜单的子级
                var kidMenuList = new List<Menu>();
                if (parentMenuList != null && parentMenuList.Any())
                {
                    var first = parentMenuList[0];
                    kidMenuList = GetMenuListBy(first.Id);
                    if (kidMenuList == null)
                        kidMenuList = new List<Menu>();
                }
                ViewBag.User = UserContext.CurrentUser;
                ViewBag.KidMenuList = kidMenuList;
                topMenuList = parentMenuList;
            }
            else
            {
                //如果不是平台管理员
                //获取用户权限
                var groupAuthList = GroupAuthorityBll.GetGroupAuthorityList(UserContext.CurrentUser.GId);
                if (groupAuthList != null && groupAuthList.Any())
                {
                    //获取所有菜单
                    var menuList = MenuBll.GetMenus();
                    var parentList = menuList.Where(m => m.Pid == 0);
                    foreach (var menu in parentList)
                    {
                        //获取所有的子菜单
                        var kidList = menuList.Where(m => m.Pid == menu.Id);
                        if (kidList == null || !kidList.Any())
                            continue;
                        foreach (var auth in groupAuthList)
                        {
                            if (kidList.FirstOrDefault(m => m.Id == auth.MenuId) != null)
                            {
                                if (topMenuList.FirstOrDefault(m => m.Id == menu.Id) == null)
                                {
                                    topMenuList.Add(menu);
                                }
                            }
                        }
                    }
                    if (topMenuList.Any())
                    {
                        var first = topMenuList[0];
                        var kidList = menuList.Where(m => m.Pid == first.Id);
                        foreach (var auth in groupAuthList)
                        {
                            var menu = kidList.FirstOrDefault(m => m.Id == auth.MenuId);
                            if (menu != null && !leftMenuList.Exists(m => m.Id == menu.Id))
                            {
                                leftMenuList.Add(menu);
                            }
                        }
                    }
                }

                ViewBag.User = UserContext.CurrentUser;
                ViewBag.KidMenuList = leftMenuList;
            }

            return View(topMenuList);
        }

        [HttpGet]
        public JsonResult GetLeftMenu(long topMenuId)
        {

            if (UserContext.CurrentUser.GId == 1)
            {
                //如果是平台管理员
                //获取父级菜单
                var parentMenuList = GetMenuListBy(0);
                var kidMenuList = new List<Menu>();
                if (parentMenuList != null && parentMenuList.Any())
                {
                    if (topMenuId <= 0)
                    {
                        //获取第一个菜单的子级
                        var first = parentMenuList[0];
                        kidMenuList = GetMenuListBy(first.Id);
                        if (kidMenuList == null)
                            kidMenuList = new List<Menu>();
                    }
                    else
                    {
                        var first = parentMenuList.FirstOrDefault(m => m.Id == topMenuId);
                        if (first != null)
                        {
                            kidMenuList = GetMenuListBy(first.Id);
                            if (kidMenuList == null)
                                kidMenuList = new List<Menu>();
                        }
                    }
                }
                kidMenuList = kidMenuList.Where(a => a.IsStop == false).ToList();
                kidMenuList = kidMenuList.OrderBy(a => a.Seq).ToList();
                return Json(kidMenuList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Menu> leftMenuList = new List<Menu>();//页面左侧菜单
                //获取用户权限
                var groupAuthList = GroupAuthorityBll.GetGroupAuthorityList(UserContext.CurrentUser.GId);
                if (groupAuthList == null || !groupAuthList.Any())
                    return Json(leftMenuList, JsonRequestBehavior.AllowGet);
                //获取菜单
                var parent = MenuBll.GetById(topMenuId);
                //获取所有菜单
                var menuList = MenuBll.GetMenus();
                //获取所有的子菜单
                var kidList = menuList.Where(m => m.Pid == parent.Id);
                if (kidList != null && kidList.Any())
                {
                    foreach (var auth in groupAuthList)
                    {
                        var menu = kidList.FirstOrDefault(m => m.Id == auth.MenuId);
                        if (menu != null && !leftMenuList.Exists(m => m.Id == menu.Id))
                        {
                            leftMenuList.Add(menu);
                        }
                    }
                }
                leftMenuList = leftMenuList.Where(a => a.IsStop == false).ToList();
                leftMenuList = leftMenuList.OrderBy(a => a.Seq).ToList();
                return Json(leftMenuList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Logout()
        {
            NIU.Core.Cookies.RemoveCookies(Cookies._CookieAdminName);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult _PwdEdit()
        {
            return View(UserContext.CurrentUser);
        }

        #region 方法

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Menu> GetMenuListBy(long pid)
        {
            //获取所有菜单
            var menuList = MenuBll.GetMenus();
            var datas = menuList.Where(m => m.Pid == pid).OrderBy(m => m.Seq).ToList();
            if (datas == null || !datas.Any())
                return new List<Menu>();
            return datas;
        }



        #endregion
    }
}