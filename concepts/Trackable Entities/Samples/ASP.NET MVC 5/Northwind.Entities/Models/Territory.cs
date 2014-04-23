using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace Northwind.Entities.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class Territory : ITrackable
    {
        public Territory()
        {
            this.Employees = new List<Employee>();
        }

        [DataMember]
        public string TerritoryID { get; set; }
        [DataMember]
        public string TerritoryDescription { get; set; }
        [DataMember]
        public int RegionID { get; set; }
        [DataMember]
        public Region Region { get; set; }
        [DataMember]
        public List<Employee> Employees { get; set; }

        [DataMember]
        public TrackingState TrackingState { get; set; }
        [DataMember]
        public ICollection<string> ModifiedProperties { get; set; }
    }
}
