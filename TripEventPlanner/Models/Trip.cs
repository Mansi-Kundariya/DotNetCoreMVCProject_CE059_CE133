using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class Trip
    {
        public short TripId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public short? CountryId { get; set; }
        public short? UserId { get; set; }

        public virtual Country Country { get; set; }
        public virtual User User { get; set; }
    }
}
