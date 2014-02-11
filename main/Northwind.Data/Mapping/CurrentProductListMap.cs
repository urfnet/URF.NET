#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

#endregion

namespace Northwind.Data.Models.Mapping
{
    public class CurrentProductListMap : EntityTypeConfiguration<CurrentProductList>
    {
        public CurrentProductListMap()
        {
            // Primary Key
            HasKey(t => new {t.ProductID, t.ProductName});

            // Properties
            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("CurrentProductList");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.ProductName).HasColumnName("ProductName");
        }
    }
}