//------------------------------------------------------------------------------
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
    
    public partial class spgo_GetLatestNewsOrderByDate_Result
    {
        public Nullable<long> RowNumber { get; set; }
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string MagazineName { get; set; }
        public Nullable<long> ViewNumber { get; set; }
        public Nullable<System.DateTime> PubDate { get; set; }
        public string Descriptions { get; set; }
        public Nullable<int> MagazineID { get; set; }
        public string ImageUrl { get; set; }
        public string LinkUrl { get; set; }
        public Nullable<System.DateTime> DateInserted { get; set; }
        public string SiteTitle { get; set; }
    }
}
