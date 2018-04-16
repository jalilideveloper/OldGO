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
using GOWEB.Models;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.IO;
using System.Web.Hosting;
using GOWEB.Controllers;

namespace GOWEB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Timer UpdateNewsTimer;
        private static Timer UpdateSitremaps;
        private static Timer UpdateAllList;
        private const String SiteMapNodeName = "url";
  


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Utility.lstMag.Clear();
            Utility.lstMagUnDisc.Clear();
            Utility.lstNewsByMagazineID.Clear();
            Utility.lstTopNews.Clear();


            Utility.ServeDataForMagazine();
            Utility.GetMostNewsTop();
            Utility.GetAllMagazine();
            Utility.GetLastNewsByMagazineID();
            Utility.FillMenuList();



            UpdateNewsTimer = new System.Timers.Timer(20000);
            // Hook up the Elapsed event for the timer. 
            Utility u = new Utility();

            UpdateNewsTimer.Elapsed += u.UpdateNews;
            UpdateNewsTimer.Enabled = true;



            UpdateAllList = new System.Timers.Timer(700000);
            // Hook up the Elapsed event for the timer. 
            UpdateAllList.Elapsed += Utility.UpdateAllList;
            UpdateAllList.Enabled = true;





            UpdateSitremaps = new System.Timers.Timer(500000);
            // Hook up the Elapsed event for the timer. 
            UpdateSitremaps.Elapsed += u.UpdateXml;
            UpdateSitremaps.Enabled = true;

            //statics
            Application.Add("Online", 0);

        }  
        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Add("Login", "1");
            Application.Lock();
            Application["Online"] = (int)Application["Online"] + 1;
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Application.Lock();
            Application["Online"] = (int)Application["Online"] - 1;
            Application.UnLock();
        }

    }
}
