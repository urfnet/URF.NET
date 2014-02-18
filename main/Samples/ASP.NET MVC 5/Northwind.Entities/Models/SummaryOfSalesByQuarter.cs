using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace Northwind.Entities.Models
{
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://schemas.datacontract.org/2004/07/TrackableEntities.Models")]
    public partial class SummaryOfSalesByQuarter : ITrackable
    {
        [DataMember]
        public Nullable<System.DateTime> ShippedDate { get; set; }
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public Nullable<decimal> Subtotal { get; set; }

        [DataMember]
        public TrackingState TrackingState { get; set; }
        [DataMember]
        public ICollection<string> ModifiedProperties { get; set; }
    }
}
