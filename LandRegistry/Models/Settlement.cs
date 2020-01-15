using System.Collections.Generic;

namespace LandRegistry.Models
{
    public partial class Settlement
    {
        public Settlement()
        {
            Registry = new HashSet<Registry>();
        }

        public int SettlId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
