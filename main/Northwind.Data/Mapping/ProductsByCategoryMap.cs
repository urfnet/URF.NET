#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class ProductsByCategoryMap : EntityTypeConfiguration<ProductsByCategory>
    {
        public ProductsByCategoryMap()
        {
            // Primary Key
            HasKey(t => new {t.CategoryName, t.ProductName, t.Discontinued});

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.QuantityPerUnit)
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("Products by Category");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
            Property(t => t.Discontinued).HasColumnName("Discontinued");
        }
    }
}