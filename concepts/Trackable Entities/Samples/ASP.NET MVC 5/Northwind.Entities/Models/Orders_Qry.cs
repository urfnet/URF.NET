using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace Northwind.Entities.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class Orders_Qry : ITrackable
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public Nullable<int> EmployeeID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> RequiredDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ShippedDate { get; set; }
        [DataMember]
        public Nullable<int> ShipVia { get; set; }
        [DataMember]
        public Nullable<decimal> Freight { get; set; }
        [DataMember]
        public string ShipName { get; set; }
        [DataMember]
        public string ShipAddress { get; set; }
        [DataMember]
        public string ShipCity { get; set; }
        [DataMember]
        public string ShipRegion { get; set; }
        [DataMember]
        public string ShipPostalCode { get; set; }
        [DataMember]
        public string ShipCountry { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public TrackingState TrackingState { get; set; }
        [DataMember]
        public ICollection<string> ModifiedProperties { get; set; }
    }
}
