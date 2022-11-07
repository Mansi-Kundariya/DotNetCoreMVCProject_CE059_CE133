using System;
using System.Collections.Generic;

#nullable disable

namespace TripEventPlanner.Models
{
    public partial class User
    {
        public User()
        {
            Trips = new HashSet<Trip>();
        }

        public short UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
