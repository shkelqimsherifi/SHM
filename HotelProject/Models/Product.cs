using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelProject.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public int Amount { get; set; }
        public bool State_text { get; set; }
    }
}