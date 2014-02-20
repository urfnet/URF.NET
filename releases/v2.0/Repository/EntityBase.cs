using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}