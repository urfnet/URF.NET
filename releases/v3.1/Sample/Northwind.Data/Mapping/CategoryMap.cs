#region

using System.Data.Entity.ModelConfiguration;

#endregion

namespace Northwind.Data.Models.Mapping
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
            Property(t => t.CategoryID).HasColumnName("CategoryID");
            Property(t => t.CategoryName).HasColumnName("CategoryName");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Picture).HasColumnName("Picture");
        }
    }
}