using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState State { get; set; }
    }
}