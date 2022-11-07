using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class Country
    {
        public Country()
        {
            Locations = new HashSet<Location>();
            Trips = new HashSet<Trip>();
        }

        public short CountryId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
