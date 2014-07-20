#region

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace Northwind.Entities.Models
{ 
public partial class NorthwindContext : INorthwindStoredProcedures
{
    public IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID)
    {
        var customerIDParameter = customerID != null ?
            new SqlParameter("@CustomerID", customerID) :
            new SqlParameter("@CustomerID", typeof (string));

        return Database.SqlQuery<CustomerOrderHistory>("CustOrderHist @CustomerID", customerIDParameter);
    }

    public int CustOrdersDetail(int? orderID)
    {
        var orderIDParameter = orderID.HasValue ?
            new SqlParameter("@OrderID", orderID) :
            new SqlParameter("@OrderID", typeof (int));

        return Database.ExecuteSqlCommand("CustOrdersDetail @OrderId", orderIDParameter);
    }

    public IEnumerable<CustomerOrderDetail> CustomerOrderDetail(string customerID)
    {
        var customerIDParameter = customerID != null ?
            new SqlParameter("@CustomerID", customerID) :
            new SqlParameter("@CustomerID", typeof (string));

        return Database.SqlQuery<CustomerOrderDetail>("CustOrdersOrders @CustomerID", customerIDParameter);
    }

    public int EmployeeSalesByCountry(DateTime? beginningDate, DateTime? endingDate)
    {
        var beginningDateParameter = beginningDate.HasValue ?
            new SqlParameter("@Beginning_Date", beginningDate) :
            new SqlParameter("@Beginning_Date", typeof (DateTime));

        var endingDateParameter = endingDate.HasValue ?
            new SqlParameter("@Ending_Date", endingDate) :
            new SqlParameter("@Ending_Date", typeof (DateTime));

        return Database.ExecuteSqlCommand("EmployeeSalesByCountry @Beginning_Date, @Ending_Date", beginningDateParameter, endingDateParameter);
    }

    public int SalesByCategory(string categoryName, string ordYear)
    {
        var categoryNameParameter = categoryName != null ?
            new SqlParameter("@CategoryName", categoryName) :
            new SqlParameter("@CategoryName", typeof (string));

        var ordYearParameter = ordYear != null ?
            new SqlParameter("@OrdYear", ordYear) :
            new SqlParameter("@OrdYear", typeof (string));

        return Database.ExecuteSqlCommand("SalesByCategory @CategoryName, @OrdYear", categoryNameParameter, ordYearParameter);
    }

    public int SalesByYear(DateTime? beginningDate, DateTime? endingDate)
    {
        var beginningDateParameter = beginningDate.HasValue ?
            new SqlParameter("@Beginning_Date", beginningDate) :
            new SqlParameter("@Beginning_Date", typeof (DateTime));

        var endingDateParameter = endingDate.HasValue ?
            new SqlParameter("@Ending_Date", endingDate) :
            new SqlParameter("@Ending_Date", typeof (DateTime));

        return Database.ExecuteSqlCommand("SalesByYear @Beginning_Date, @Ending_Date", beginningDateParameter, endingDateParameter);
    }
}
}