using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace Northwind.Entities.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class Region : ITrackable
    {
        public Region()
        {
            this.Territories = new List<Territory>();
        }

        [DataMember]
        public int RegionID { get; set; }
        [DataMember]
        public string RegionDescription { get; set; }
        [DataMember]
        public List<Territory> Territories { get; set; }

        [DataMember]
        public TrackingState TrackingState { get; set; }
        [DataMember]
        public ICollection<string> ModifiedProperties { get; set; }
    }
}
