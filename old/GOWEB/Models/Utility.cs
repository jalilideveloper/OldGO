using GOWEB.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Linq;

namespace GOWEB.Models
{
    public class Utility
    {
        public static List<MagazineObj> lstMag = new List<MagazineObj>();
        public static List<MagazineObj> lstMagUnDisc = new List<MagazineObj>();
        public static List<listNews> lstTopNews = new List<listNews>();
        public static List<listNews> lstNewsByMagazineID = new List<listNews>();
       


        public void UpdateNews(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                Controllers.SyncController s = new Controllers.SyncController();
                s.GetData();
            }
        }
        

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static void UpdateAllList(object sender, ElapsedEventArgs e)
        {

            lstMag.Clear();
            lstMagUnDisc.Clear();
            lstNewsByMagazineID.Clear();
            lstTopNews.Clear();

            ServeDataForMagazine();
            GetMostNewsTop();
            GetAllMagazine();
            GetLastNewsByMagazineID();
        }


        public void UpdateXml(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
                {


                    var virPath = HostingEnvironment.MapPath("/");

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = Encoding.UTF8;
                    settings.Indent = true;


                    // All News
                    UpdateAllNewsSitemap(db, settings);

                    // All Number Pages
                    UpdateArticleSitemap(settings);

                    // All Magaznie 
                    UpdateMagazine(db, settings);


                    // All MagaznieSelected 
                    UpdateMagazineSelected(db, settings);

                }
            }

        }

        public static void ServeDataForMagazine()
        {
            if (lstMag.Count == 0)
            {
                DTController d = new DTController();
                lstMag = d.GetDistinctMagazines();
            }
        }
        public static void GetMostNewsTop()
        {
            if (lstTopNews.Count == 0)
            {
                DTController d = new DTController();
               lstTopNews = d.GetMostRecentNews();
              
            }
          

        }
        public static void GetAllMagazine()
        {
            if (lstMagUnDisc.Count == 0)
            {
                DTController d = new DTController();
                lstMagUnDisc = d.GetMagazines();

            }
         
        }


        public static void GetLastNewsByMagazineID()
        {
            if (lstNewsByMagazineID.Count == 0)
            {
                DTController d = new DTController();
                foreach (var item in lstMagUnDisc)
                {
                    foreach (var itemInside in d.GetLatestNewsByMagazineID(item.MagazineID))
                    {
                        lstNewsByMagazineID.Add(itemInside);
                    }    
                }
                
            }

        }


        







        private static void UpdateMagazineSelected(greenopt_GONewsEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "MagazineSelectedSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "MagazineSelectedSitemap.xml", settings))
                {
                    var magazines = db.spgo_GetAllMagazine().ToList();
                    writer.WriteStartDocument();

                    writer.WriteStartElement("urlset");

                    foreach (var item in magazines)
                    {
                       
                        int countMagazineSelected = Convert.ToInt32(DTController.GetAllPages(item.MagazineID));

                        for (int i = 0; i < countMagazineSelected; i++)
                        {
                            writer.WriteStartElement("url");
                            //--------------------------------------
                            writer.WriteStartElement("loc");
                            writer.WriteString("https://greenoptimizer.com/Home/صفحه_مجله_اینترنتی/a|" + item.MagazineID+"-"+ i);
                            writer.WriteEndElement();
                            //--------------------------------------
                            writer.WriteEndElement();
                        }


                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }


            }
            else
            {

                DeleteFile(virPath + "MagazineSelectedSitemap.xml");
                using (XmlWriter writer = XmlWriter.Create(virPath + "MagazineSelectedSitemap.xml", settings))
                {
                    var magazines = db.spgo_GetAllMagazine().ToList();
                    writer.WriteStartDocument();

                    writer.WriteStartElement("urlset");

                    foreach (var item in magazines)
                    {

                        int countMagazineSelected = Convert.ToInt32(DTController.GetAllPages(item.MagazineID));

                        for (int i = 0; i < countMagazineSelected; i++)
                        {
                            writer.WriteStartElement("url");
                            //--------------------------------------
                            writer.WriteStartElement("loc");
                            writer.WriteString("https://greenoptimizer.com/Home/صفحه_مجله_اینترنتی/a|" + item.MagazineID + "-" + i);
                            writer.WriteEndElement();
                            //--------------------------------------
                            writer.WriteEndElement();
                        }


                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }

            }
        }





        private static void UpdateMagazine(greenopt_GONewsEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "MagazineSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "MagazineSitemap.xml", settings))
                {
                    var magazines = db.spgo_GetAllMagazine().ToList();
                    writer.WriteStartDocument();

                    writer.WriteStartElement("urlset");

                    foreach (var item in magazines)
                    {
                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/مجله_اینترنتی/a|" + item.SiteTitle);
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }

            }
        }

        public static void UpdateArticleSitemap(XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "ArticleSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "ArticleSitemap.xml", settings))
                {
                    writer.WriteStartDocument();


                    int countAllNews = Convert.ToInt32(DTController.GetAllPages(0));

                    writer.WriteStartElement("urlset");
                    for (int i = 0; i < countAllNews; i++)
                    {

                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/صفحه_بهینه_سازی_سایت/p|" + i);
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }
            }
            else
            {

                DeleteFile(virPath + "ArticleSitemap.xml");

                using (XmlWriter writer = XmlWriter.Create(virPath + "ArticleSitemap.xml", settings))
                {
                    writer.WriteStartDocument();


                    int countAllNews = Convert.ToInt32(DTController.GetAllPages(0));

                    writer.WriteStartElement("urlset");
                    for (int i = 0; i < countAllNews; i++)
                    {

                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/صفحه_بهینه_سازی_سایت/p|" + i);
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }


            }
        }

        public static void UpdateAllNewsSitemap(greenopt_GONewsEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            var virPath = HostingEnvironment.MapPath("/");
            if (File.Exists(virPath + "AllNewsSitemap.xml") == false)
            {
                using (XmlWriter writer = XmlWriter.Create(virPath + "AllNewsSitemap.xml", settings))
                {
                    var q = db.spgo_GetAllNews();
                    writer.WriteStartDocument();


                    writer.WriteStartElement("urlset");
                    foreach (var item in q.ToList())
                    {
                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/جزئیات_خبر_فناوری/ a" + item.NewsID + "|" + item.Title.Replace(" ", "-").Replace("+", "-").Replace("?", "-").Replace("*", "-").Replace(";", "-").Replace(",", "-").Replace(".", "-").Replace(":", "-").Replace("؛", "-").Replace("؟", "-").Replace("»", "-").Replace("«", "-").Replace("!", "-").ToString());
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteStartElement("lastmod");
                        writer.WriteString(item.PubDate.ToString());
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }
            }
            else
            {


                DeleteFile(virPath + "AllNewsSitemap.xml");
                using (XmlWriter writer = XmlWriter.Create(virPath + "AllNewsSitemap.xml", settings))
                {
                    var q = db.spgo_GetAllNews();
                    writer.WriteStartDocument();


                    writer.WriteStartElement("urlset");
                    foreach (var item in q.ToList())
                    {
                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/جزئیات_خبر_فناوری/ a" + item.NewsID + "|" + item.Title.Replace(" ", "-").Replace("+", "-").Replace("?", "-").Replace("*", "-").Replace(";", "-").Replace(",", "-").Replace(".", "-").Replace(":", "-").Replace("؛", "-").Replace("؟", "-").Replace("»", "-").Replace("«", "-").Replace("!", "-").ToString());
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteStartElement("lastmod");
                        writer.WriteString(item.PubDate.ToString());
                        writer.WriteEndElement();
                        //--------------------------------------
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();

                }
            }
        }



        public class XmlSitemaps
        {

            public string loc { get; set; }

            public string lastmod { get; set; }


        }
    }
}