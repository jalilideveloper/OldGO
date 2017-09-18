using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using GOWEB.Models;
using System.Threading.Tasks;

namespace GOWEB.Controllers
{
    public class SyncController : Controller
    {
        // GET: Sync


        public  void GetData()
        {
            try
            {
                using (greenopt_GONewsEntities db = new greenopt_GONewsEntities())
                {
                    IList<tblNew> TempList = new List<tblNew>();
                    Random rnd = new Random();
                    var qRss = db.spgo_GetAllMagazine();
                    foreach (var item in qRss)
                    {
                        IList<Item> lst = ParseRss(item.RssUrl);
                        //var q = (from i in db.tblNews
                        //         where i.MagazineID == item.MagazineID
                        //         select i).OrderByDescending(p=> p.NewsID).FirstOrDefault();
                        var q = db.spgo_GetNewsByMgIDOrderByNewsID(item.MagazineID).FirstOrDefault();
                        if (q != null)
                        {
                            foreach (var pItem in lst)
                            {
                                if (pItem.Title != q.Title)
                                {
                                    tblNew n = new tblNew();
                                    n.Title = pItem.Title;
                                    n.PubDate = pItem.PublishDate;
                                    n.Descriptions = pItem.Content;
                                    n.MagazineID = item.MagazineID;
                                    n.ViewNumber =  rnd.Next(500,900);
                                    n.DateInserted = DateTime.Now.Date;
                                    n.LinkUrl = pItem.Link;
                                    TempList.Add(n);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            var reverseList = TempList.Reverse().ToList();
                            foreach (var itemInside in reverseList)
                            {
                                using (var ctx = new greenopt_GONewsEntities())
                                {
                                    //do transaction here
                                
                                tblNew n = new tblNew();
                                n.Title = itemInside.Title;
                                n.PubDate = itemInside.PubDate;
                                n.Descriptions = itemInside.Descriptions;
                                n.MagazineID = item.MagazineID;
                                n.ViewNumber = rnd.Next(500, 900);
                                n.DateInserted = DateTime.Now.Date;
                                n.LinkUrl = itemInside.LinkUrl;
                                // db.tblNews.Add(n);
                                //await db.SaveChangesAsync();
                                ctx.sp_InsertNews(itemInside.Title, itemInside.Descriptions, itemInside.PubDate, itemInside.ImageUrl, itemInside.MagazineID, itemInside.ViewNumber, itemInside.DateInserted, itemInside.LinkUrl);
                                //db.SaveChanges();
                                }
                            }
                            TempList.Clear();
                        }
                        else if (q == null)
                        {
                            var SortedList = lst.Reverse().ToList();
                            foreach (var itemInside in SortedList)
                            {
                                using (var ctx = new greenopt_GONewsEntities())
                                {
                                    tblNew n = new tblNew();
                                    n.Title = itemInside.Title;
                                    n.PubDate = itemInside.PublishDate;
                                    n.Descriptions = itemInside.Content;
                                    n.MagazineID = item.MagazineID;
                                    n.ViewNumber = rnd.Next(500, 900);
                                    n.DateInserted = DateTime.Now.Date;
                                    n.LinkUrl = itemInside.Link;
                                    //db.tblNews.Add(n);
                                    //await db.SaveChangesAsync();
                                    ctx.sp_InsertNews(itemInside.Title, itemInside.Content, itemInside.PublishDate, "", item.MagazineID, rnd.Next(500, 900) , DateTime.Now.Date, itemInside.Link);
                                    //db.SaveChanges();
                                }
                            }
                        }
                    }
                    //return true;
                }
            }
            catch (Exception e)
            {
                string q = e.InnerException.ToString();
                
                //return false;
            }

        }




        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public virtual IList<Item> ParseRss(string url)
        {
            try
            {
                XDocument doc = XDocument.Load(url);
                // RSS/Channel/item
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new Item
                              {
                                  FeedType = FeedType.RSS,
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };
                return entries.ToList();
            }
            catch
            {
                return new List<Item>();
            }
        }
        public class Item
        {
            public string Link { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime? PublishDate { get; set; }
            public FeedType FeedType { get; set; }

            public Item()
            {
                Link = "";
                Title = "";
                Content = "";
                PublishDate = null;
                FeedType = FeedType.RSS;
            }
        }
        public enum FeedType
        {
            /// <summary>
            /// Really Simple Syndication format.
            /// </summary>
            RSS,
            /// <summary>
            /// RDF site summary format.
            /// </summary>
            RDF,
            /// <summary>
            /// Atom Syndication format.
            /// </summary>
            Atom
        }
    }
}