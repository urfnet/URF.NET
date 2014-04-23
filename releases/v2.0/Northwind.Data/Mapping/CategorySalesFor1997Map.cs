#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class CategorySalesFor1997Map : EntityTypeConfiguration<CategorySalesFor1997>
    {
        public CategorySalesFor1997Map()
        {
            // Primary Key
            HasKey(t => t.CategoryName);

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("Category Sales for 1997");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.CategorySales).HasColumnName("CategorySales");
        }
    }
}