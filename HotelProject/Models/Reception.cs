using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelProject.Models
{
    public class Reception
    {
        public int ReceptionId { get; set; }
        public Person oPerson { get; set; }
        public Room oRoom { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateText { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureDateText { get; set; }

        public DateTime DateDepartureConfirmation { get; set; }
        public string DateDepartureConfirmationText { get; set; }

        public decimal StartingPrice { get; set; }
        public string StartingPriceText { get; set; }


        public decimal Advancement { get; set; }
        public string AdvancementText { get; set; }

        public decimal RemainingPrice { get; set; }
        public string RemainingPriceText { get; set; }

        public decimal TotalPaid { get; set; }
        public string TotalPaidText { get; set; }

        public decimal PenaltyCost { get; set; }
        public string PenaltyCostText { get; set; }

        public string Observation { get; set; }
        public bool State_text { get; set; }
        public List<Sale> oSale { get; set; }
    }
}