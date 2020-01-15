using System.Collections.Generic;

namespace LandRegistry.Models
{
    public partial class CadEng
    {
        public CadEng()
        {
            Registry = new HashSet<Registry>();
        }

        public int CeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
