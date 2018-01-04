using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOWEB.Controllers
{
    public class Error404Controller : Controller
    {
        // GET: Error404
        public ActionResult Index()
        {
            return View();
        }


        //404
        public ActionResult NotFound()
        {
            return View();
        }

        //403
        public ActionResult BadRequest()
        {
            return View();
        }

        //500
        public ActionResult ConstructoringInServer()
        {
            return View();
        }

        

    }
}