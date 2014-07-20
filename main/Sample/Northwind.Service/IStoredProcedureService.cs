#region

using System.Collections.Generic;
using Northwind.Entities.Models;

#endregion

namespace Northwind.Service
{
    public interface IStoredProcedureService
    {
        IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID);
        int CustOrdersDetail(int? orderID);
        IEnumerable<CustomerOrderDetail> CustomerOrderDetail(string customerID);
    }
}