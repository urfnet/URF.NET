#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class OrderDetailsExtendedMap : EntityTypeConfiguration<OrderDetailsExtended>
    {
        public OrderDetailsExtendedMap()
        {
            // Primary Key
            HasKey(t => new {t.OrderID, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount});

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.UnitPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Order Details Extended");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.Discount).HasColumnName("Discount");
            Property(t => t.ExtendedPrice).HasColumnName("ExtendedPrice");
        }
    }
}