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

        public void UpdateNews(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                Controllers.SyncController s = new Controllers.SyncController();
                s.GetData();
            }
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
                if (newItem != null)
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
                if (newItem != null)
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
                if (newItem != null)
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
        }

        public static void UpdateAllNewsSitemap(greenopt_GONewsEntities db, XmlWriterSettings settings, XmlSitemaps newItem = null )
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
                if (newItem != null)
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
        }



        public class XmlSitemaps
        {

            public string loc { get; set; }

            public string lastmod { get; set; }


        }
    }
}