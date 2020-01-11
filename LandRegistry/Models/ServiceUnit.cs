using System;
using System.Collections.Generic;

namespace LandRegistry.Models
{
    public partial class ServiceUnit
    {
        public ServiceUnit()
        {
            Registry = new HashSet<Registry>();
        }

        public int SuId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ConNum { get; set; }
        public string ChiefFullName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
