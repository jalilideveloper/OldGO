using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOWEB.Models
{
    public class Cart
    {
        public int ProductID { get; set; }
        public int DayRequest { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }

        public string LinkUrl { get; set; }
        public string Commnet { get; set; }

        public Cart() { }
        public Cart(int productId , int dayReqest, decimal price, string productName , string linkUrl,string comment)
        {
            ProductID = productId;
            DayRequest = dayReqest;
            Price = price;
            ProductName = productName;
            LinkUrl = linkUrl;
            Commnet = comment;

        }
    }
}