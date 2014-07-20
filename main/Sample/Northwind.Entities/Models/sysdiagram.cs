#region

using Repository.Pattern.Ef6;

#endregion

namespace Northwind.Entities.Models
{
    public class sysdiagram : Entity
    {
        public string name { get; set; }
        public int principal_id { get; set; }
        public int diagram_id { get; set; }
        public int? version { get; set; }
        public byte[] definition { get; set; }
    }
}