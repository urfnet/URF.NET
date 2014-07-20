using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Entities.Models.Mapping
{
    public class SalesTotalsByAmountMap : EntityTypeConfiguration<SalesTotalsByAmount>
    {
        public SalesTotalsByAmountMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderID, t.CompanyName });

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("SalesTotalsByAmount");
            this.Property(t => t.SaleAmount).HasColumnName("SaleAmount");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
        }
    }
}
