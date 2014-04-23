#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            HasKey(t => t.CategoryID);

            // Properties
            Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("Categories");
            Property(t => t.CategoryID).HasColumnName("Category ID");
            Property(t => t.CategoryName).HasColumnName("Category Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Picture).HasColumnName("Picture");
        }
    }
}