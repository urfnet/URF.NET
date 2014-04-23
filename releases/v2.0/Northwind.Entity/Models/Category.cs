#region

using System.Collections.Generic;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class Category : EntityBase
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}