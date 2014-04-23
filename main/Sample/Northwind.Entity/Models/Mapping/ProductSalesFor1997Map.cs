using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Entities.Models.Mapping
{
    public class ProductSalesFor1997Map : EntityTypeConfiguration<ProductSalesFor1997>
    {
        public ProductSalesFor1997Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CategoryName, t.ProductName });

            // Properties
            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("ProductSalesFor1997");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductSales).HasColumnName("ProductSales");
        }
    }
}
