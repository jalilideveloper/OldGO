using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOWEB.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        [ActionName("طراحی-سایت-پلان-یک")]
        public ActionResult طراحی_سایت_پلان_یک()
        {
            return View();
        }

        [ActionName("چاپ-طراحی-سایت-پلان-یک")]
        public ActionResult چاپ_طراحی_سایت_پلان_یک()
        {
            return View();
        }
    }
}