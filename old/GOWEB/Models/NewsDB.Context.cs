﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GOWEB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class greenopt_GONewsEntities : DbContext
    {
        public greenopt_GONewsEntities()
            : base("name=greenopt_GONewsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblLanguage> tblLanguages { get; set; }
        public virtual DbSet<tblMagazine> tblMagazines { get; set; }
        public virtual DbSet<tblMenu> tblMenus { get; set; }
        public virtual DbSet<tblNew> tblNews { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblPage> tblPages { get; set; }
    
        public virtual int sp_InsertNews(string title, string descriptions, Nullable<System.DateTime> pubDate, string imageUrl, Nullable<int> magazineID, Nullable<long> viewNumber, Nullable<System.DateTime> dateInserted, string linkUrl)
        {
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var descriptionsParameter = descriptions != null ?
                new ObjectParameter("Descriptions", descriptions) :
                new ObjectParameter("Descriptions", typeof(string));
    
            var pubDateParameter = pubDate.HasValue ?
                new ObjectParameter("PubDate", pubDate) :
                new ObjectParameter("PubDate", typeof(System.DateTime));
    
            var imageUrlParameter = imageUrl != null ?
                new ObjectParameter("ImageUrl", imageUrl) :
                new ObjectParameter("ImageUrl", typeof(string));
    
            var magazineIDParameter = magazineID.HasValue ?
                new ObjectParameter("MagazineID", magazineID) :
                new ObjectParameter("MagazineID", typeof(int));
    
            var viewNumberParameter = viewNumber.HasValue ?
                new ObjectParameter("ViewNumber", viewNumber) :
                new ObjectParameter("ViewNumber", typeof(long));
    
            var dateInsertedParameter = dateInserted.HasValue ?
                new ObjectParameter("DateInserted", dateInserted) :
                new ObjectParameter("DateInserted", typeof(System.DateTime));
    
            var linkUrlParameter = linkUrl != null ?
                new ObjectParameter("LinkUrl", linkUrl) :
                new ObjectParameter("LinkUrl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InsertNews", titleParameter, descriptionsParameter, pubDateParameter, imageUrlParameter, magazineIDParameter, viewNumberParameter, dateInsertedParameter, linkUrlParameter);
        }
    
        public virtual ObjectResult<spgo_GetAllMagazine_Result> spgo_GetAllMagazine()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetAllMagazine_Result>("spgo_GetAllMagazine");
        }
    
        public virtual ObjectResult<spgo_GetAllNews_Result> spgo_GetAllNews()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetAllNews_Result>("spgo_GetAllNews");
        }
    
        public virtual ObjectResult<spgo_GetAllRecentNewsTopView_Result> spgo_GetAllRecentNewsTopView()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetAllRecentNewsTopView_Result>("spgo_GetAllRecentNewsTopView");
        }
    
        public virtual ObjectResult<spgo_GetLatestNewsOrderByDate_Result> spgo_GetLatestNewsOrderByDate(Nullable<int> startpageid)
        {
            var startpageidParameter = startpageid.HasValue ?
                new ObjectParameter("startpageid", startpageid) :
                new ObjectParameter("startpageid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetLatestNewsOrderByDate_Result>("spgo_GetLatestNewsOrderByDate", startpageidParameter);
        }
    
        public virtual ObjectResult<spgo_GetLatestNewsOrderByDateByMGID_Result> spgo_GetLatestNewsOrderByDateByMGID(Nullable<int> mgID, Nullable<int> startpageid)
        {
            var mgIDParameter = mgID.HasValue ?
                new ObjectParameter("mgID", mgID) :
                new ObjectParameter("mgID", typeof(int));
    
            var startpageidParameter = startpageid.HasValue ?
                new ObjectParameter("startpageid", startpageid) :
                new ObjectParameter("startpageid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetLatestNewsOrderByDateByMGID_Result>("spgo_GetLatestNewsOrderByDateByMGID", mgIDParameter, startpageidParameter);
        }
    
        public virtual ObjectResult<spgo_GetNewsByID_Result> spgo_GetNewsByID(Nullable<int> newsID)
        {
            var newsIDParameter = newsID.HasValue ?
                new ObjectParameter("NewsID", newsID) :
                new ObjectParameter("NewsID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetNewsByID_Result>("spgo_GetNewsByID", newsIDParameter);
        }
    
        public virtual ObjectResult<spgo_GetNewsByMgIDOrderByNewsID_Result> spgo_GetNewsByMgIDOrderByNewsID(Nullable<int> mgID)
        {
            var mgIDParameter = mgID.HasValue ?
                new ObjectParameter("mgID", mgID) :
                new ObjectParameter("mgID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_GetNewsByMgIDOrderByNewsID_Result>("spgo_GetNewsByMgIDOrderByNewsID", mgIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> spgo_GetPageNumbers(Nullable<int> magazineID)
        {
            var magazineIDParameter = magazineID.HasValue ?
                new ObjectParameter("MagazineID", magazineID) :
                new ObjectParameter("MagazineID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spgo_GetPageNumbers", magazineIDParameter);
        }
    
        public virtual ObjectResult<spgo_LatestNewsPublishDate_Result> spgo_LatestNewsPublishDate()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_LatestNewsPublishDate_Result>("spgo_LatestNewsPublishDate");
        }
    
        public virtual ObjectResult<spgo_LatestNewsViewNumber_Result> spgo_LatestNewsViewNumber()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spgo_LatestNewsViewNumber_Result>("spgo_LatestNewsViewNumber");
        }
    }
}
