using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

namespace Northwind.Data.Mapping
{
    public class SalesByCategoryMap : EntityTypeConfiguration<SalesByCategory>
    {
        public SalesByCategoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CategoryID, t.CategoryName, t.ProductName });

            // Properties
            this.Property(t => t.CategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("Sales by Category");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductSales).HasColumnName("ProductSales");
        }
    }
}
