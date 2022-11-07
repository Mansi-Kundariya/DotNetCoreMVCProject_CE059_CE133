using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class TripActivity
    {
        public short TripId { get; set; }
        public short ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
