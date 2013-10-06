#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class ShipperMap : EntityTypeConfiguration<Shipper>
    {
        public ShipperMap()
        {
            // Primary Key
            HasKey(t => t.ShipperID);

            // Properties
            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.Phone)
                .HasMaxLength(24);

            // Table & Column Mappings
            ToTable("Shippers");
            Property(t => t.ShipperID).HasColumnName("Shipper ID");
            Property(t => t.CompanyName).HasColumnName("Company Name");
            Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}