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

        [ActionName("طراحی-سایت-سئو-سایت-بهینه-سازی-سایت")]
        public ActionResult طراحی_سایت_سئو_سایت_بهینه_سازی_سایت(string id)
        {

            if (id != null && id != "")
            {
                using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
                {
                    //ViewBag.Data = db.tblPages.Where(p => p.tblMenu.MenuUrl == id).FirstOcdrDefault();
                    ViewBag.Data = (from i in db.tblPages
                                    join p in db.tblMenus on i.MenuID equals p.MenuID
                                    where p.MenuUrl == id
                                    select i).FirstOrDefault();
                                   
                }
            }


            return View();
        }


        public string AddPage(tblPage p)
        {
            try
            {
                using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
                {
                    if (p.PassCode == "123a@a.com")
                    {
                        db.tblPages.Add(new tblPage
                        {
                            Author = p.Author,
                            Date = DateTime.Now.Date,
                            Description = p.Description,
                            MenuID = p.MenuID,
                            MetaDescription = p.MetaDescription,
                            MetaKeyword = p.MetaKeyword,
                            MetaSubject = p.MetaSubject,
                            MetaTitle = p.MetaTitle,
                            Title = p.Title
                        });
                        db.SaveChanges();
                    }
                    return "True";
                }
            }
            catch (Exception ex)
            {

                var q = ex.Message;

                return "false";
            }


        }
        
         public string EditMenu(MenuItem p)
        {
            try
            {
                using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
                {
                    if (p.PassCode == "123a@a.com")
                    {
                        if (p.ParrentID == "0")
                        {

                            var q = db.tblMenus.Where(x => x.MenuID == p.MenuID).FirstOrDefault();
                            q.MenuName = p.MenuName;
                            q.MenuUrl = p.MenuUrl;
                            db.SaveChanges();
                        }
                        else
                        {
                            var q = db.tblMenus.Where(x => x.MenuID == p.MenuID).FirstOrDefault();
                            q.MenuName = p.MenuName;
                            q.MenuUrl = p.MenuUrl;
                            q.ParrentID =Convert.ToInt32( p.ParrentID);
                            db.SaveChanges();

                        }
                    }
                    else
                    {
                        return "false";
                    }
                    return "True";
                }
            }
            catch (Exception ex)
            {

                var q = ex.Message;

                return "false";
            }


        }
        public string AddMenu(MenuItem p)
        {
            try
            {
                using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
                {
                    if (p.PassCode == "123a@a.com")
                    {
                        if (p.HasChild)
                        {
                            db.tblMenus.Add(new tblMenu
                            {
                                MenuName = p.MenuName,
                                ParrentID = Convert.ToInt32(p.ParrentID),
                                MenuUrl = p.MenuUrl,
                                Priority = 0,
                                LanguageID = 4
                            });
                            db.SaveChanges();
                        }
                        else
                        {
                            db.tblMenus.Add(new tblMenu
                            {
                                MenuName = p.MenuName,
                                ParrentID = null,
                                MenuUrl = p.MenuUrl,
                                Priority = 0,
                                LanguageID = 4
                            });
                            db.SaveChanges();
                        }
                    }
                    else
                    {

                        return "false";
                        
                    }
                    return "True";
                }
            }
            catch (Exception ex)
            {

                var q = ex.Message;

                return "false";
            }


        }

        public string LoadMenu()
        {
            using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
            {
                List<MenuItem> x = db.tblMenus.ToList().Select(p => new MenuItem { MenuID = p.MenuID, MenuName = p.MenuName ,ParrentID = p.ParrentID.ToString() }).ToList();
                var q = new JavaScriptSerializer().Serialize(Json(x));
                return q;
            }

        }
    }

    public class MenuItem
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }

        public string ParrentID { get; set; }
        public string PassCode { get; set; }

        public bool HasChild { get; set; }

        public string MenuUrl { get; set; }





    }

}