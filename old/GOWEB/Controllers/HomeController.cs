using GOWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GOWEB.Controllers
{
    public class HomeController : Controller
    {

        //[OutputCache(Duration = 10, VaryByParam = "none" ,Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index(string name)
        {
          //  CheckNewData();
            return View();
        }
        public ActionResult AddAds()
        {
            //  CheckNewData();
            return View();
        }
        public ActionResult AddMenu()
        {
            //  CheckNewData();
            return View();
        }

        [ActionName("سئو-سایت")]
        public ActionResult سئو_سایت()
        {
            return View();
        }
        //public ActionResult SEO()
        //{
        //    return View();
        //}
        [ActionName("طراحی-سایت")]
        public ActionResult طراحی_سایت()
        {

            return View();

        }

        //public ActionResult WebDesign()
        //{
        //    return View();

        //}

        [ActionName("نمونه-طراحی-سایت-و-سئو")]
        public ActionResult نمونه_طراحی_سایت_و_سئو()
        {
            return View();

        }

        //public ActionResult portfolio()
        //{
        //    return View();

        //}


        [ActionName("اخبار-فناوری-و-تکنولوژی")]
        public ActionResult اخبار_فناوری_و_تکنولوژی()
        {
            return View();
        }
        //public ActionResult Article()
        //{
        //   // ArticleShow(1);
        //    return View();

        //}





        [ActionName("صفحه-بهینه-سازی-سایت")]
        public ActionResult صفحه_بهینه_سازی_سایت(string  id)
        {
            string ids = id.Split('|')[1]; 
            ViewData["NumberTake"] = int.Parse(ids) * 15;
            ViewBag.NumberTake = int.Parse(ids);

            return View("اخبار-فناوری-و-تکنولوژی");
        }
        //public ActionResult ArticleShow(int id)
        //{

        //    ViewData["NumberTake"] = id * 15;
        //    return View("Article");

        //}




        //public ActionResult ArticleDetails(string id)
        //{
        //    ViewData["NewsID"] = Convert.ToInt32(id.Split('|')[0]);
        //    return View();
        //}

        [ActionName("جزئیات-خبر-فناوری")]
        public ActionResult جزئیات_خبر_فناوری(string id)
        {
            string ids = id.Substring(2, id.Length - 2);
            ViewData["NewsID"] = Convert.ToInt32(ids.Split('|')[0]);
            return View();
        }






        [ActionName("مجله-اینترنتی")]
        public ActionResult مجله_اینترنتی(string id)
        {
            
            int mgID  = Utility.lstMag.Find(p => p.SiteName.Contains(id.Split('|')[1])).MagazineID;           
            ViewData["MagazineName"] = id;
            ViewBag.MGID = mgID;
            return View("اخبار-فناوری-و-تکنولوژی");
        }
        //public ActionResult Magazine(string id)
        //{

        //    ViewData["MagazineName"] = id;
        //    return View("Article");
        //}



        [ActionName("صفحه-مجله-اینترنتی")]
        public ActionResult صفحه_مجله_اینترنتی(string id)
        {
            id = id.Substring(2, id.Length - 2);
            int mgID = Convert.ToInt32(id.Split('-')[0]);
            int num = Convert.ToInt32(id.Split('-')[1]);

            ViewData["NumberLoadMagazine"] = num * 15;
            ViewData["SelectedMagazineID"] = mgID;
            ViewBag.MGID = mgID;
            ViewBag.NumMagazine = num;
            return View("اخبار-فناوری-و-تکنولوژی");
        }


        //public ActionResult MagazineSelected(string id)
        //{

        //    int mgID = Convert.ToInt32(id.Split('-')[0]);
        //    int num = Convert.ToInt32(id.Split('-')[1]);

        //    ViewData["NumberLoadMagazine"] = num * 15;
        //    ViewData["SelectedMagazineID"] = mgID;

        //   return View("Article");
        //}


        [ActionName("درباره-ما-سئو-سایت")]
        public ActionResult درباره_ما_سئو_سایت()
        {

            return View();
        }



        //public ActionResult AboutUs()
        //{

        //    return View();
        //}

        public static List<listNews> tagList = new List<listNews>();
        public ActionResult tag(string id)
        {

            ViewBag.TagSearched = id;
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                var q = db.spgo_Search(id).Take(30).Where(p => p.Title.Contains(id)).Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber, Description = p.Descriptions, PubDate = p.PubDate.ToString(), SiteTitle = p.MagazineID.ToString() }).ToList();;
                tagList = q;
            }
            return View();
        }


        [ActionName("ارتباط-با-ما-سئو-سایت")]
        public ActionResult ارتباط_با_ما_سئو_سایت()
        {
            return View();
        }


        //public ActionResult ContactUs()
        //{

        //    return View();
        //}
        
    }
}
