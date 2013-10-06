#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class TerritoryMap : EntityTypeConfiguration<Territory>
    {
        public TerritoryMap()
        {
            // Primary Key
            HasKey(t => t.TerritoryID);

            // Properties
            Property(t => t.TerritoryID)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.TerritoryDescription)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Territories");
            Property(t => t.TerritoryID).HasColumnName("TerritoryID");
            Property(t => t.TerritoryDescription).HasColumnName("TerritoryDescription");
            Property(t => t.RegionID).HasColumnName("RegionID");

            // Relationships
            HasRequired(t => t.Region)
                .WithMany(t => t.Territories)
                .HasForeignKey(d => d.RegionID);
        }
    }
}