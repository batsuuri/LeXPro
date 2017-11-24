using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using LeXPro.Core;
namespace LeXPro
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);    
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Main.apppath = Server.MapPath("");
            AppConfig.Init();
        }
        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception exception = Server.GetLastError();
                if (exception is HttpUnhandledException)
                {
                    exception = exception.InnerException;
                }
                if (exception != null)
                {
                    Main.ErrorLog(Request.QueryString.ToString(), exception);
                }
                Server.ClearError();
                //Response.Redirect("~/Error/Index");
            }
            catch (Exception ex)
            {
                //Main.ErrorLog("Handling application error ", ex);
            }
        }
    }
}