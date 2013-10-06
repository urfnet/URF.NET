#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class ProductsAboveAveragePriceMap : EntityTypeConfiguration<ProductsAboveAveragePrice>
    {
        public ProductsAboveAveragePriceMap()
        {
            // Primary Key
            HasKey(t => t.ProductName);

            // Properties
            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Products Above Average Price");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.UnitPrice).HasColumnName("UnitPrice");
        }
    }
}