using System;
using System.Collections.Generic;

namespace LandRegistry.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Registry = new HashSet<Registry>();
        }

        public int OwnId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Inn { get; set; }
        public string ConNum { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
