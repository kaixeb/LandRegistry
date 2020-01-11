using System.Collections.Generic;

namespace LandRegistry.Models
{
    public partial class District
    {
        public District()
        {
            Registry = new HashSet<Registry>();
        }

        public int DisId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
