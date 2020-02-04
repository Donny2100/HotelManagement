using NIU.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Windows;
using Microsoft.AspNet.SignalR;
using BillCount;

namespace Hotel.Admin
{
    public class WpfActionFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //没接到post请求，设置从新读数据库
            MvcApplication app = filterContext.HttpContext.ApplicationInstance as MvcApplication;
            if (filterContext.HttpContext.Request.HttpMethod == "POST")
            {
                //MessageBox.Show("Right");
                app.database_flag = 1;
            }
            

            //检查wpf计费程序的状态，并重新启动
            Thread t= (Thread)filterContext.HttpContext.Application["thread"];
            if (!t.IsAlive)
            {
                app.ThreadCreate();
            }
        }
    }
    public class MvcApplication : System.Web.HttpApplication
    {
        public BillCount.MainWindow newWindow;
        //是否需要从新从数据库读数据
        public int database_flag;
        Thread t;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new WpfActionFilterAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ThreadCreate();//以线程的方式创建wpf程序
            
            database_flag = 0;
            Main.Start();
        }

        //服务器到浏览器的消息推送函数
        public void sendMsgToClient(String name,String message)
        {
            
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalHub>();
            if (hubContext != null)
            {
                //hubContext.Clients.All.addNewMessageToPage(name, message);
            }
            else
            {
                MessageBox.Show("HIHIHI");
            }
        }

        //响wpf计费程序回数据刷新信号
        public int retFlag()
        {
            return database_flag;
        }

        //数据刷新设置为0：不刷新
        public void clearFlag()
        {
            database_flag = 0;
        }

        //创建wpf计费线程
        public void ThreadCreate()
        {

            t = new Thread(() =>
            {
                newWindow = new MainWindow();
                newWindow.Show();
                //newWindow.setMsgFunction(receiveMsg);

                newWindow.Closed += (sender2, e2) =>
                                        newWindow.Dispatcher.InvokeShutdown();
                newWindow.getFlag = retFlag;
                newWindow.setflag = clearFlag;
                newWindow.sendMsgToClient = sendMsgToClient;
                this.Application["window"] = newWindow;
                System.Windows.Threading.Dispatcher.Run();
                
            });
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
            
            this.Application["thread"] = t;
            
        }
        public bool ThreadState()
        {
            return t.IsAlive;
        }
    }
}
