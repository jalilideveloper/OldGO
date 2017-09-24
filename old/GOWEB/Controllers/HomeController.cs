using System;
using System.Web.Mvc;

namespace GOWEB.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
          //  CheckNewData();
            return View();
        }

      

        public ActionResult SEO()
        {
            return View();
        }
        
        public ActionResult WebDesign()
        {
            return View();

        }

        public ActionResult portfolio()
        {
            return View();

        }

        public ActionResult Article()
        {
           // ArticleShow(1);
            return View();

        }
        public ActionResult ArticleShow(int id)
        {

            ViewData["NumberTake"] = id * 15;
            return View("Article");

        }
        public ActionResult ArticleDetails(string id)
        {
            ViewData["NewsID"] = Convert.ToInt32(id.Split('|')[0]);
            return View();

        }


        public ActionResult Magazine(string id)
        {

            ViewData["MagazineName"] = id;
            return View("Article");
        }

        public ActionResult MagazineSelected(string id)
        {

            int mgID = Convert.ToInt32(id.Split('-')[0]);
            int num = Convert.ToInt32(id.Split('-')[1]);

            ViewData["NumberLoadMagazine"] = num * 15;
            ViewData["SelectedMagazineID"] = mgID;
            
           return View("Article");
        }


        public ActionResult AboutUs()
        {

            return View();
        }

        public ActionResult ContactUs()
        {

            return View();
        }
        
    }
}
