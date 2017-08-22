using GOWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GOWEB.Controllers
{
    public class DTController : Controller
    {

        public string GetMostRecentNews()
        {
            //db.Database.Connection.Open();
            using (greenopt_GONewsEntities dbs = new greenopt_GONewsEntities())
            {

                List<listNews> q = dbs.tblNews.Select(x => new listNews
                {
                    NewsID = x.NewsID,
                    NTitle = x.Title,
                    MagazineName = x.tblMagazine.MagazineName,
                    ViewNumber = x.ViewNumber
                })
                .OrderByDescending(o => o.ViewNumber).Take(15).ToList();
                dbs.Database.Connection.Close();
                var json = JsonConvert.SerializeObject(q);
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
                    var q = (from i in db.tblNews
                             select i).ToList().OrderByDescending(o => o.PubDate.Value.Year).Take(15);

                    foreach (var item in q)
                    {
                        System.Globalization.PersianCalendar persianCalandar =
                                                     new System.Globalization.PersianCalendar();
                        int year = persianCalandar.GetYear(Convert.ToDateTime(item.PubDate));
                        int month = persianCalandar.GetMonth(Convert.ToDateTime(item.PubDate));
                        int day = persianCalandar.GetDayOfMonth(Convert.ToDateTime(item.PubDate));
                        lstNews.Add(new listNews {SiteTitle =item.tblMagazine.SiteTitle, Description = item.Descriptions,MagazineName = item.tblMagazine.MagazineName, NewsID = item.NewsID, NTitle = item.Title, PubDate = year +"/"+ month +"/"+ day,ViewNumber = item.ViewNumber});
                    }

                    var json = JsonConvert.SerializeObject(lstNews);
                    return json;
                }
                else
                {
                    List<listNews> lstNews = new List<listNews>();
                    var q = (from i in db.tblNews
                             select i).ToList().OrderByDescending(o => o.PubDate.Value.Year).Skip(id).Take(15);

                    foreach (var item in q)
                    {
                        System.Globalization.PersianCalendar persianCalandar =
                                                     new System.Globalization.PersianCalendar();
                        int year = persianCalandar.GetYear(Convert.ToDateTime(item.PubDate));
                        int month = persianCalandar.GetMonth(Convert.ToDateTime(item.PubDate));
                        int day = persianCalandar.GetDayOfMonth(Convert.ToDateTime(item.PubDate));
                        lstNews.Add(new listNews { SiteTitle = item.tblMagazine.SiteTitle, Description = item.Descriptions, MagazineName = item.tblMagazine.MagazineName, NewsID = item.NewsID, NTitle = item.Title, PubDate = year + "/" + month + "/" + day, ViewNumber = item.ViewNumber });
                    }

                    var json = JsonConvert.SerializeObject(lstNews);
                    return json;
                }
            }
        }


        public string GetMagazines()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {

                //db.Database.Connection.Open();
                var q = db.tblMagazines.Where(p => p.tblNews.Count > 0).Select(x => new MagazineObj
                {
                    MagazineID = x.MagazineID,
                    SiteName = x.SiteTitle,
                    MagazineName = x.MagazineName
                }).ToList();
                var json = JsonConvert.SerializeObject(q);

                return json;
            }
        }



        public string GetDistinctMagazines()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                var q = db.tblMagazines.Where(p => p.tblNews.Count > 0).Select(x => new MagazineObj
                {
                    MagazineID = x.MagazineID,
                    SiteName = x.SiteTitle,
                    MagazineName = x.MagazineName
                }).Distinct().ToList();
                var json = JsonConvert.SerializeObject(q);

                return json;
            }

        }


        public string GetLatestNewsByMagazineID(int id)
        {
            using (greenopt_GONewsEntities dbs = new greenopt_GONewsEntities())
            {
                //db.Database.Connection.Open();
                List<listNews> queryNewsSelected = new List<listNews>();


                var qGetNewOFMAgazine = (from i in dbs.tblNews
                                         where i.MagazineID == id
                                         select i).OrderByDescending(o => o.PubDate.Value.Year).Take(15).ToList();

                foreach (var inside in qGetNewOFMAgazine)
                {
                    queryNewsSelected.Add(new listNews { NewsID = inside.NewsID, MagazineName = inside.tblMagazine.SiteTitle, NTitle = inside.Title, ViewNumber = inside.ViewNumber });
                }

                var json = JsonConvert.SerializeObject(queryNewsSelected);
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
