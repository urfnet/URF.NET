#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap()
        {
            // Primary Key
            HasKey(t => new {t.OrderID, t.ProductID});

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Order Details");
            Property(t => t.OrderID).HasColumnName("Order ID");
            Property(t => t.ProductID).HasColumnName("Product ID");
            Property(t => t.UnitPrice).HasColumnName("Unit Price");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.Discount).HasColumnName("Discount");

            // Relationships
            HasRequired(t => t.Order)
                .WithMany(t => t.Order_Details)
                .HasForeignKey(d => d.OrderID);
            HasRequired(t => t.Product)
                .WithMany(t => t.Order_Details)
                .HasForeignKey(d => d.ProductID);
        }
    }
}