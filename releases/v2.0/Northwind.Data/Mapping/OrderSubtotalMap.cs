#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class OrderSubtotalMap : EntityTypeConfiguration<OrderSubtotal>
    {
        public OrderSubtotalMap()
        {
            // Primary Key
            HasKey(t => t.OrderID);

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Order Subtotals");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.Subtotal).HasColumnName("Subtotal");
        }
    }
}