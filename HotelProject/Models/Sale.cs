using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelProject.Models
{
    public class Sale
    {
        public int SalesID { get; set; }
        public Reception oReception { get; set; }
        public decimal Total { get; set; }
        public string State_text { get; set; }
        public List<SaleDetail> oSaleDetail { get; set; }
    }
}