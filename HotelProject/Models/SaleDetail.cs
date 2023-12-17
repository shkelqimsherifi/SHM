using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelProject.Models
{
    public class SaleDetail
    {
        public int SalesDetailId { get; set; }
        public Product oProduct { get; set; }
        public int SalesID { get; set; }
        public int Amount { get; set; }
        public decimal SubTotal { get; set; }
    }
}