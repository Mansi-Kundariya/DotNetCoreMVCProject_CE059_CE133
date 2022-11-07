using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class Location
    {
        public Location()
        {
            Activities = new HashSet<Activity>();
        }

        public short LocationId { get; set; }
        public string Name { get; set; }
        public short CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
