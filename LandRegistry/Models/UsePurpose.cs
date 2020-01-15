using System.Collections.Generic;

namespace LandRegistry.Models
{
    public partial class UsePurpose
    {
        public UsePurpose()
        {
            Registry = new HashSet<Registry>();
        }

        public int UpId { get; set; }
        public string Purpose { get; set; }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
//(земли сельскохозяйственного назначения, земли населенных пунктов, земли промышленности, 
//энергетики, транспорта, связи, радиовещания, телевидения, информатики, 
//земли для обеспечения космической деятельности, земли обороны,
//безопасности и земли иного специального назначения, земли особо охраняемых территорий и объектов,
//земли лесного фонда, земли водного фонда, земли запаса)
