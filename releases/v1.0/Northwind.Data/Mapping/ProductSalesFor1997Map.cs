#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class ProductSalesFor1997Map : EntityTypeConfiguration<ProductSalesFor1997>
    {
        public ProductSalesFor1997Map()
        {
            // Primary Key
            HasKey(t => new {t.CategoryName, t.ProductName});

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Product Sales for 1997");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.ProductSales).HasColumnName("ProductSales");
        }
    }
}