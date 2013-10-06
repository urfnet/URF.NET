using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

namespace Northwind.Data.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.QuantityPerUnit)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Products");
            this.Property(t => t.ProductID).HasColumnName("Product ID");
            this.Property(t => t.ProductName).HasColumnName("Product Name");
            this.Property(t => t.SupplierID).HasColumnName("Supplier ID");
            this.Property(t => t.CategoryID).HasColumnName("Category ID");
            this.Property(t => t.QuantityPerUnit).HasColumnName("Quantity Per Unit");
            this.Property(t => t.UnitPrice).HasColumnName("Unit Price");
            this.Property(t => t.UnitsInStock).HasColumnName("Units In Stock");
            this.Property(t => t.UnitsOnOrder).HasColumnName("Units On Order");
            this.Property(t => t.ReorderLevel).HasColumnName("Reorder Level");
            this.Property(t => t.Discontinued).HasColumnName("Discontinued");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
