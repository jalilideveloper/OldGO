using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GOWEB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Timer aTimer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

         
            aTimer = new System.Timers.Timer(1800000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += ATimer_Elapsed; ;
            aTimer.Enabled = true;

        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Controllers.SyncController s = new Controllers.SyncController();
            s.GetData();
        }

      

    }
}
