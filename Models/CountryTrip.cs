﻿namespace TripAPI.Models
{
    public class CountryTrip
    {
        public int IdCountry { get; set; }
        public Country Country { get; set; }

        public int IdTrip { get; set; }
        public Trip Trip { get; set; }
    }
}
