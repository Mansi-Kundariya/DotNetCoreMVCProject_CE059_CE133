using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            Activities = new HashSet<Activity>();
        }

        public short ActivityTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
