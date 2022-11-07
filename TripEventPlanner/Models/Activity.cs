using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class Activity
    {
        public short ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal? Price { get; set; }
        public short? ActivityTypeId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public short? LocationId { get; set; }

        public virtual ActivityType ActivityType { get; set; }
        public virtual Location Location { get; set; }
    }
}