﻿using System;
using System.Collections.Generic;
using TripAPI.Models;

namespace TripAPI.RequestModels
{
    public class TripDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public List<CountryDTO> Countries { get; set; }
        public List<ClientDTO> Clients { get; set; }
    }
}