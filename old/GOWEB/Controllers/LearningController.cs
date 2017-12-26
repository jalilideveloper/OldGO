using GOWEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GOWEB.Controllers
{
    public class LearningController : Controller
    {
        // GET: Learning
        public ActionResult Index(string id)
        {
            return View();
        }


        public ActionResult طراحی_سایت_سئو_سایت_بهینه_سازی_سایت(string id)
        {
            return View();
        }


        public string AddPage(tblPage p)
        {


            return "";

        }


        public string LoadMenu()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                List<MenuItem> x = db.tblMenus.ToList().Select(p => new MenuItem {MenuID = p.MenuID ,MenuName = p.MenuName }).ToList();
                var q = new JavaScriptSerializer().Serialize(Json(x));
                return q;
            }

        }
    }

    public class MenuItem
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
    }
    
}