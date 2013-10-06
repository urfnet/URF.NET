#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class CurrentProductListMap : EntityTypeConfiguration<CurrentProductList>
    {
        public CurrentProductListMap()
        {
            // Primary Key
            HasKey(t => new {t.ProductID, t.ProductName});

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            ToTable("Current Product List");
            this.Property(t => t.ProductID).HasColumnName("Product ID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
        }
    }
}