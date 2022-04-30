﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }

        public ICollection<HotelRoom> HotelRoom { get; set; }
        public ICollection<RoomAmenities> RoomAmenities { get; set; }
    }
}
