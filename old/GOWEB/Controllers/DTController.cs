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

        public List<listNews> GetMostRecentNews()
        {
            //db.Database.Connection.Open();
            using (greenopt_GONewsEntities dbs = new greenopt_GONewsEntities())
            {
                return dbs.spgo_GetAllRecentNewsTopView().Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber }).ToList();
                //var json = JsonConvert.SerializeObject(GetMostRecent);
                //return json;
            }


        }



        public static List<listNews> GetAllNews(int id)
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                    List<listNews> lstNews = new List<listNews>();

                    var GetallNews = db.spgo_GetLatestNewsOrderByDate(id)
                        .Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber, Description = p.Descriptions, PubDate = p.PubDate.ToString(), SiteTitle = p.SiteTitle }).ToList();
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
                    return lstNews;
                        //var json = JsonConvert.SerializeObject(lstNews);
                        //return json;

                    }
                    else
                    {
                    return lstNews;
                    }
              
                
            }
        }


        public static string GetAllPages(int id)
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                //id == 0 means article page
                //id == !0 magazine id
                List<Numbers> lstNews = new List<Numbers>();

                int? GetallNews = db.spgo_GetPageNumbers(id).FirstOrDefault();
                
                string sectionPage = Convert.ToString(GetallNews / 15);

                return sectionPage;


            }
        }

        public static List<listNews> GetAllNewsMagazine(int id, int start)
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {


                List<listNews> lstNews = new List<listNews>();

                var GetallNews = db.spgo_GetLatestNewsOrderByDateByMGID(id, start)
                    .Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber, Description = p.Descriptions, PubDate = p.PubDate.ToString(), SiteTitle = p.SiteTitle }).ToList();
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
                    return lstNews;
                    //var json = JsonConvert.SerializeObject(lstNews);
                    //return json;

                }
                else
                {

                    return lstNews;
                }


            }
        }
        public List<MagazineObj> GetMagazines()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                return db.spgo_GetAllMagazine().Select(p => new MagazineObj { MagazineID = p.MagazineID, SiteName = p.SiteTitle, MagazineName = p.MagazineName }).ToList();
                //var json = JsonConvert.SerializeObject(getMagazine);
                //return json;
            }
        }



        public List<MagazineObj> GetDistinctMagazines()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                List<MagazineObj> getMagazine = db.spgo_GetAllMagazine().Select(p => new MagazineObj { MagazineID = p.MagazineID, SiteName = p.SiteTitle, MagazineName = p.MagazineName }).Distinct().ToList();
                return getMagazine;
                //var json = JsonConvert.SerializeObject(getMagazine);
                //return json;
            }

        }


        public List<listNews> GetLatestNewsByMagazineID(int id)

        {
            using (greenopt_GONewsEntities dbs = new greenopt_GONewsEntities())
            {
                return dbs.spgo_GetLatestNewsOrderByDateByMGID(id,0).Select(p => new listNews { NewsID = p.NewsID, NTitle = p.Title, MagazineName = p.MagazineName, ViewNumber = p.ViewNumber }).ToList();
                //var json = JsonConvert.SerializeObject(qGetNewOFMAgazine);
                //return json;
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

    public class Numbers
    {

        public int count { get; set; }

    }

}
