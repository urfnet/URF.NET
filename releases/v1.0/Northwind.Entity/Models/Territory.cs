#region

using System.Collections.Generic;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class Territory : EntityBase
    {
        public Territory()
        {
            Employees = new List<Employee>();
        }

        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}