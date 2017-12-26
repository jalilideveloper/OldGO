using GOWEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;



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


                 db.tblMenus.ToList();
            }

        }




    }
}