using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState EntityObjectState { get; set; } //TODO: Renamed since a possible coflict with State entity column
    }
}