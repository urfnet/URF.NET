using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

namespace Northwind.Data.Mapping
{
    public class SummaryOfSalesByQuarterMap : EntityTypeConfiguration<SummaryOfSalesByQuarter>
    {
        public SummaryOfSalesByQuarterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderID);

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Summary of Sales by Quarter");
            this.Property(t => t.ShippedDate).HasColumnName("ShippedDate");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.Subtotal).HasColumnName("Subtotal");
        }
    }
}
