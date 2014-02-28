using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Entities.Models.Mapping
{
    public class OrderSubtotalMap : EntityTypeConfiguration<OrderSubtotal>
    {
        public OrderSubtotalMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderID);

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OrderSubtotals");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.Subtotal).HasColumnName("Subtotal");
        }
    }
}
