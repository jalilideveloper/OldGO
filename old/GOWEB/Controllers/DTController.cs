using GOWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data.Entity;
using GOWEB.Models;

namespace GOWEB.Controllers
{
    public class DTController : Controller
    {

        public string GetMostRecentNews()
        {
            //db.Database.Connection.Open();
            using (greenopt_GONewsEntities dbs = new greenopt_GONewsEntities())
            {

                var GetMostRecent = dbs.spgo_GetAllRecentNewsTopView().Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber });
                var json = JsonConvert.SerializeObject(GetMostRecent);
                return json;
            }


        }



        public string GetAllNews(int id)
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                //List<tblNew> q = (List<tblNew>)ViewData["MyData"];
                if (id == 0)
                {

                    List<listNews> lstNews = new List<listNews>();

                    var GetallNews = db.spgo_GetAllRecentNewsTopView()
                        .Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber,Description = p.Descriptions, PubDate = p.PubDate.ToString() , SiteTitle = p.SiteTitle }).ToList();
                    if (GetallNews.Count > 0)
                    {


                        foreach (var item in GetallNews)
                        {
                            System.Globalization.PersianCalendar persianCalandar =
                                                         new System.Globalization.PersianCalendar();
                            if (item.PubDate != null)
                            {
                                int year = persianCalandar.GetYear(Convert.ToDateTime(item.PubDate));
                                int month = persianCalandar.GetMonth(Convert.ToDateTime(item.PubDate));
                                int day = persianCalandar.GetDayOfMonth(Convert.ToDateTime(item.PubDate));
                                lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, MagazineName = item.MagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = year.ToString() + "/" + month.ToString() + "/" + day.ToString(), ViewNumber = item.ViewNumber });
                            }
                            else
                            {
                                lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, MagazineName = item.MagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = "", ViewNumber = item.ViewNumber });
                            }
                        }
                        var json = JsonConvert.SerializeObject(lstNews);
                        return json;

                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    List<listNews> lstNews = new List<listNews>();
                    var GetallNews = db.spgo_GetAllRecentNewsTopView().Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber, Description = p.Descriptions, PubDate = p.PubDate.ToString(), SiteTitle = p.SiteTitle }).Skip(id).ToList();
                    if (GetallNews.Count > 0)
                    {


                        foreach (var item in GetallNews)
                        {
                            System.Globalization.PersianCalendar persianCalandar =
                                                         new System.Globalization.PersianCalendar();
                            int year = persianCalandar.GetYear(Convert.ToDateTime(item.PubDate));
                            int month = persianCalandar.GetMonth(Convert.ToDateTime(item.PubDate));
                            int day = persianCalandar.GetDayOfMonth(Convert.ToDateTime(item.PubDate));
                            lstNews.Add(new listNews { SiteTitle = item.SiteTitle, Description = item.Description, MagazineName = item.MagazineName, NewsID = item.NewsID, NTitle = item.NTitle, PubDate = year + "/" + month + "/" + day, ViewNumber = item.ViewNumber });
                        }

                        var json = JsonConvert.SerializeObject(lstNews);
                        return json;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }


        public string GetMagazines()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                var getMagazine = db.spgo_GetAllMagazine().Select(p => new MagazineObj { MagazineID = p.MagazineID, SiteName = p.SiteTitle, MagazineName = p.MagazineName });
                var json = JsonConvert.SerializeObject(getMagazine);
                return json;
            }
        }



        public string GetDistinctMagazines()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                var getMagazine = db.spgo_GetAllMagazine().Select(p => new MagazineObj { MagazineID = p.MagazineID, SiteName = p.SiteTitle, MagazineName = p.MagazineName }).Distinct().ToList();
                var json = JsonConvert.SerializeObject(getMagazine);
                return json;
            }

        }


        public string GetLatestNewsByMagazineID(int id)

        {
            using (greenopt_GONewsEntities dbs = new greenopt_GONewsEntities())
            {

                var qGetNewOFMAgazine = dbs.spgo_GetLatestNewsOrderByDateByMGID(id).Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber });
                var json = JsonConvert.SerializeObject(qGetNewOFMAgazine);
                return json;
            }
        }



        // GET: DT
        public ActionResult Index()
        {
            return View();
        }

        // GET: DT/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DT/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DT/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DT/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DT/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DT/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

    public class listNews
    {

        public int NewsID { get; set; }
        public string NTitle { get; set; }
        public string MagazineName { get; set; }
        public long? ViewNumber { get; set; }
        public string PubDate { get; set; }
        public string Description { get; set; }

        public string SiteTitle { get; set; }

    }


    public class MagazineObj
    {
        public int MagazineID { get; set; }
        public string SiteName { get; set; }

        public string MagazineName { get; set; }

    }
}
