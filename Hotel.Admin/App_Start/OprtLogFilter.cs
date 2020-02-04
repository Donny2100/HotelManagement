using Hotel.Bll;
using NIU.Common.BLL;
using NIU.Core;
using NIU.Core.Log;
using NIU.Forum.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Admin.App_Start
{
    /// <summary>
    /// 用户操作日志
    /// </summary>
    public class OprtLogFilter : FilterAttribute, IActionFilter
    {
        private IOprtLogBL logService = ContainerHelper.ResolvePerHttpRequest<IOprtLogBL>();

        /// <summary>
        /// 是否记录日志，默认为不记录
        /// </summary>
        public bool IsRecordLog = false;

        /// <summary>
        /// 动作
        /// </summary>
        public string Method = string.Empty;

        /// <summary>
        /// 是否为form提交，若是则设置为true，默认为false
        /// </summary>
        public bool IsFormPost = false;

        /// <summary>
        /// 如果是form提交（IsFormPost为true），则需要设置此字段，此字段代表请求方法的参数类型集合
        /// </summary>
        public Type[] Entitys = null;

        public OprtLogType LogWay = OprtLogType.未知;

        /// <summary>
        /// 若是软删除则为false，否则为true
        /// </summary>
        public bool IsFromCache = false;

        /// <summary>
        /// Action执行后
        /// </summary>
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            var resultFlag = false;
            OprtLogType LogType = LogWay;
            if (!IsRecordLog)
                return;
            try
            {
                var result = ((System.Web.Mvc.JsonResult)filterContext.Result).Data;
                dynamic d = result;
                if (result.ToString().Contains("Ret"))
                {
                    if (d.Ret == 0)
                        resultFlag = true;
                }
                else if (result.ToString().Contains("type"))
                {
                    if (d.type == 0)
                        resultFlag = true;
                }
                //object obj= JsonConvert.DeserializeObject(result);
                string controller = filterContext.Controller.ToString();
                string action = filterContext.ActionDescriptor.ActionName;

                Type type = Type.GetType(controller);
                ParameterInfo[] parasInfo = null;
                if (IsFormPost)
                    parasInfo = type.GetMethod(action, Entitys).GetParameters();
                else
                    parasInfo = type.GetMethod(action).GetParameters();

                if (parasInfo == null || parasInfo.Length == 0)
                    return;

                StringBuilder content = new StringBuilder();
                if (!IsFormPost)
                {
                    foreach (var item in parasInfo)
                    {
                        //判断是新增还是修改
                        if (LogWay == OprtLogType.新增和修改)
                        {
                            if (item.Name.ToLower().ToString() == "id")
                            {
                                string id = filterContext.HttpContext.Request[item.Name].ToString();
                                if (id == "0" || id == "")
                                {
                                    LogType = OprtLogType.新增;
                                    Method = OprtLogType.新增.ToString();
                                }
                                else
                                {
                                    LogType = OprtLogType.修改;
                                    Method = OprtLogType.修改.ToString();
                                }
                            }
                        }

                        if (LogWay == OprtLogType.删除 && IsFromCache)
                        {
                            content.Append(DelLogCache.Get());
                            continue;
                        }

                        //获取内容
                        if (string.IsNullOrEmpty(filterContext.HttpContext.Request[item.Name]))
                            continue;
                        content.Append(item.Name);
                        content.Append(":");
                        content.Append(filterContext.HttpContext.Request[item.Name].ToString());
                        content.Append(";");
                    }
                }
                else
                {
                    foreach (var item in parasInfo)
                    {
                        PropertyInfo[] fileds = Entitys[0].GetProperties();
                        foreach (var f in fileds)
                        {
                            //判断是新增还是修改
                            if (LogWay == OprtLogType.新增和修改)
                            {
                                if (f.Name.ToLower().ToString() == "id")
                                {
                                    string id = filterContext.HttpContext.Request[f.Name].ToString();
                                    if (id == "0" || id == "")
                                    {
                                        LogType = OprtLogType.新增;
                                        Method = OprtLogType.新增.ToString();
                                    }
                                    else
                                    {
                                        LogType = OprtLogType.修改;
                                        Method = OprtLogType.修改.ToString();
                                    }
                                }
                            }

                            if (LogWay == OprtLogType.删除 && IsFromCache)
                            {
                                content.Append(DelLogCache.Get());
                                continue;
                            }

                            if (string.IsNullOrEmpty(filterContext.HttpContext.Request[f.Name]))
                                continue;
                            content.Append(f.Name);
                            content.Append(":");
                            content.Append(filterContext.HttpContext.Request[f.Name].ToString());
                            content.Append(";");
                        }
                    }
                }

                //根据controler获取菜单的配置，找出控制器对应的中文名
                string page = string.Empty;
                var menuList = MenuBll.GetAllList().Where(m => m.MenuController != null);
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var menu = menuList.FirstOrDefault(m => m.MenuController.ToLower() == controllerName.ToLower());
                if (menu != null)
                    page = menu.Name;

                var user = UserContext.CurrentUser;
                if (string.IsNullOrWhiteSpace(Method))
                    Method = LogType.ToString();

                var model = new OprtLog
                {
                    Id =IdBuilder.NextLongID(),
                    AppKey = string.Empty,
                    HotelId = user == null ? 0 : user.HotelId,
                    UserId = user == null ? 0 : user.Id,
                    UserName = user == null ? string.Empty : user.UserName,
                    Controller = controllerName,
                    Action = action,
                    Page = page,
                    Method = Method,
                    Content = content.ToString().Length > 2000 ? content.ToString().Substring(0, 2000) : content.ToString(),
                    Result = resultFlag,
                    LogType = (int)LogType,
                    CDate = TypeConvert.DateTimeToInt(DateTime.Now),
                };

                logService.AddOrUpdate(model);
            }
            catch (Exception ex)
            {
                LogFactory.GetLogger().Log(LogLevel.Error, ex);
            }
        }

        /// <summary>
        /// Action执行前
        /// </summary>
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}