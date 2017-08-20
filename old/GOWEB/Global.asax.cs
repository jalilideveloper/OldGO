using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GOWEB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (Application["MyDate"] == null)
            {
                Application["MyDate"] = DateTime.Now;
            }

            //Main();


        }
        public  void Main()
        {
            TimeSpan ts =  DateTime.Now - (DateTime)Application["Mydate"];
            if (ts.Minutes >= 270)
            {
                Controllers.SyncController s = new Controllers.SyncController();
                s.GetData();
                Application["MyDate"] = DateTime.Now;

            }
            // wait

        }

    }
}
