using System;
using System.Collections.Generic;
using Repository.Pattern.Infrastructure;

namespace Northwind.Entitiy.Models
{
    public partial class Current_Product_List : Entity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
