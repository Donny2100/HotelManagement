using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Web.Http;
using Owin;



[assembly: OwinStartupAttribute(typeof(Hotel.Admin.Startup))]
namespace Hotel.Admin
{
    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            

        }
        public void Configuration(IAppBuilder app)
        {
            //实现SignalR
            app.MapSignalR();
        }
        public void runwpf(IOwinContext context)
        {
            //MvcApplication app = this.ApplicationInstance as MvcApplication;
            
            if (context.Request.Method=="POST")
            {
                
            }
        }
    }
}
