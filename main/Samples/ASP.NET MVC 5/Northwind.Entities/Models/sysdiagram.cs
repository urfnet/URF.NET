using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace Northwind.Entities.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class sysdiagram : ITrackable
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int principal_id { get; set; }
        [DataMember]
        public int diagram_id { get; set; }
        [DataMember]
        public Nullable<int> version { get; set; }
        [DataMember]
        public byte[] definition { get; set; }

        [DataMember]
        public TrackingState TrackingState { get; set; }
        [DataMember]
        public ICollection<string> ModifiedProperties { get; set; }
    }
}
