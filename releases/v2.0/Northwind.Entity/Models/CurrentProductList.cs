

using Repository;

namespace Northwind.Entity.Models
{
    public partial class CurrentProductList : EntityBase
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}