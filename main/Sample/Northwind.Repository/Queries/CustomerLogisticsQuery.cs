#region

using Northwind.Entitiy.Models;
using Repository.Pattern.Repositories;

#endregion

namespace Northwind.Repository.Queries
{
    public class CustomerLogisticsQuery : QueryObject<Customer>
    {
        public CustomerLogisticsQuery FromCountry(string country)
        {
            Add(x => x.Country == country);
            return this;
        }

        public CustomerLogisticsQuery LivesInCity(string city)
        {   
            Add(x => x.City == city);
            return this;
        }
    }
}