#region

using System.Collections.Generic;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class Region : EntityBase
    {
        public Region()
        {
            Territories = new List<Territory>();
        }

        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}