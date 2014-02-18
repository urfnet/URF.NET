using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace Northwind.Entities.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class Employee : ITrackable
    {
        public Employee()
        {
            this.Employees1 = new List<Employee>();
            this.Orders = new List<Order>();
            this.Territories = new List<Territory>();
        }

        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string TitleOfCourtesy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BirthDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> HireDate { get; set; }
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
        public string HomePhone { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public byte[] Photo { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public Nullable<int> ReportsTo { get; set; }
        [DataMember]
        public string PhotoPath { get; set; }
        [DataMember]
        public List<Employee> Employees1 { get; set; }
        [DataMember]
        public Employee Employee1 { get; set; }
        [DataMember]
        public List<Order> Orders { get; set; }
        [DataMember]
        public List<Territory> Territories { get; set; }

        [DataMember]
        public TrackingState TrackingState { get; set; }
        [DataMember]
        public ICollection<string> ModifiedProperties { get; set; }
    }
}
