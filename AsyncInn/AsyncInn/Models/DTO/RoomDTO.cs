﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.DTO
{
    public class RoomDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        public List<AmenityDTO> Amenities { get; set; }
    }
}
