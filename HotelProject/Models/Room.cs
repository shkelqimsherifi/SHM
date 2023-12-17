using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelProject.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Number { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public HotelFloor oHotelFloor { get; set; }
        public Category oCategory { get; set; }
        public RoomState oRoomState { get; set; }
        public bool State_text { get; set; }

    }
}