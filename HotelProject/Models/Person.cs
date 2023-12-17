using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelProject.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string PersonName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Code { get; set; }
        public PersonType oPersonType { get; set; }
        public bool State_text { get; set; }
    }
}