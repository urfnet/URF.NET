using System;
using System.Collections.Generic;
using Repository;

namespace Northwind.Data.Models
{
    public partial class CurrentProductList : EntityBase
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
