using System;
using System.Collections.Generic;
using Repository;
using Repository.Pattern.Infrastructure;

namespace Northwind.Data.Models
{
    public partial class Region : EntityBase
    {
        public Region()
        {
            this.Territories = new List<Territory>();
        }

        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
