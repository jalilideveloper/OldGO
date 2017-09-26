using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GOWEB.Models;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.IO;
using System.Web.Hosting;
using GOWEB.Controllers;

namespace GOWEB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Timer UpdateNewsTimer;
        private static Timer UpdateSitremaps;
        private const String SiteMapNodeName = "url";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            UpdateNewsTimer = new System.Timers.Timer(900000);
            // Hook up the Elapsed event for the timer. 
            UpdateNewsTimer.Elapsed += UpdateNews;
            UpdateNewsTimer.Enabled = true;



            UpdateSitremaps = new System.Timers.Timer(45000);
            // Hook up the Elapsed event for the timer. 
            UpdateSitremaps.Elapsed += UpdateXml;
            UpdateSitremaps.Enabled = true;



        }


 

        private void UpdateNews(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                Controllers.SyncController s = new Controllers.SyncController();
                s.GetData();
            }
        }

        private void UpdateXml(object sender, ElapsedEventArgs e)
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
                    UpdateAllNewsSitemap(db, virPath, settings);

                    // All Number Pages
                    UpdateArticleSitemap(virPath, settings);

                    // All Magaznie 
                    UpdateMagazine(db, virPath, settings);


                    // All MagaznieSelected 
                    UpdateMagazineSelected(db, virPath, settings);

                }
            }

        }

        private static void UpdateMagazineSelected(greenopt_GONewsEntities db, string virPath, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
            if (File.Exists(virPath + "MagazineSelectedSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "MagazineSelectedSitemap.xml", settings))
                {
                    var magazines = db.spgo_GetAllMagazine().ToList();
                    writer.WriteStartDocument();

                    writer.WriteStartElement("urlset");

                    foreach (var item in magazines)
                    {
                        DTController dt = new DTController();
                        int countMagazineSelected = Convert.ToInt32(dt.GetAllPages(item.MagazineID));

                        for (int i = 0; i < countMagazineSelected; i++)
                        {
                            writer.WriteStartElement("url");
                            //--------------------------------------
                            writer.WriteStartElement("loc");
                            writer.WriteString("https://greenoptimizer.com/Home/MagazineSelected/" + item.MagazineID);
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
                XDocument xDocument = XDocument.Load(virPath + "MagazineSelectedSitemap.xml");
                XElement root = xDocument.Element("urlset");
                IEnumerable<XElement> rows = root.Descendants("url");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("url",
                  new XElement("loc", newItem.loc),
                   new XElement("lastmod", newItem.lastmod)));
                xDocument.Save(virPath + "MagazineSelectedSitemap.xml");
            }
        }

        private static void UpdateMagazine(greenopt_GONewsEntities db, string virPath, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
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
                        writer.WriteString("https://greenoptimizer.com/Home/Magazine/" + item.MagazineID);
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
                XDocument xDocument = XDocument.Load(virPath + "MagazineSitemap.xml");
                XElement root = xDocument.Element("urlset");
                IEnumerable<XElement> rows = root.Descendants("url");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("url",
                  new XElement("loc", newItem.loc),
                   new XElement("lastmod", newItem.lastmod)));
                xDocument.Save(virPath + "MagazineSitemap.xml");
            }
        }

        private static void UpdateArticleSitemap(string virPath, XmlWriterSettings settings, XmlSitemaps newItem = null)
        {
           
            if (File.Exists(virPath + "ArticleSitemap.xml") == false)
            {

                using (XmlWriter writer = XmlWriter.Create(virPath + "ArticleSitemap.xml", settings))
                {
                    writer.WriteStartDocument();


                    DTController dt = new DTController();
                    int countAllNews = Convert.ToInt32(dt.GetAllPages(0));

                    writer.WriteStartElement("urlset");
                    for (int i = 0; i < countAllNews; i++)
                    {

                        writer.WriteStartElement("url");
                        //--------------------------------------
                        writer.WriteStartElement("loc");
                        writer.WriteString("https://greenoptimizer.com/Home/ArticleShow/" + i);
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
                XDocument xDocument = XDocument.Load(virPath + "ArticleSitemap.xml");
                XElement root = xDocument.Element("urlset");
                IEnumerable<XElement> rows = root.Descendants("url");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("url",
                     new XElement("loc", newItem.loc),
                   new XElement("lastmod", newItem.lastmod)));
                xDocument.Save(virPath + "ArticleSitemap.xml");
            }
        }

        private static void UpdateAllNewsSitemap(greenopt_GONewsEntities db, string virPath, XmlWriterSettings settings, XmlSitemaps  newItem = null)
        {
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
                        writer.WriteString("https://greenoptimizer.com/Home/ArticleDetails/" + item.NewsID + "|" + item.Title.Replace(" ", "-").Replace("+", "-").Replace("?", "-").Replace("*", "-").Replace(";", "-").Replace(",", "-").Replace(".", "-").Replace(":", "-").Replace("؛", "-").Replace("؟", "-").Replace("»", "-").Replace("«", "-").Replace("!", "-").ToString());
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
                XDocument xDocument = XDocument.Load(virPath + "AllNewsSitemap.xml");
                XElement root = xDocument.Element("urlset");
                IEnumerable<XElement> rows = root.Descendants("url");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("url",
                   new XElement("loc", newItem.loc),
                   new XElement("lastmod", newItem.lastmod)));
                xDocument.Save(virPath + "AllNewsSitemap.xml");
            }
        }

    

        public class XmlSitemaps
        {

            public string loc { get; set; }

            public string lastmod { get; set; }


        }

    }
}
