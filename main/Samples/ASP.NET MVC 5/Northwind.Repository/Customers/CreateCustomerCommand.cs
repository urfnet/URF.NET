#region

using Northwind.Entities.Models;

#endregion

namespace Northwind.Repository.Customers
{
    //This could/should be a DTO, for simplicity leaving this as the domain entity for now.
    public class CreateCustomerCommand
    {
        public Customer Customer { get; set; }
    }
}