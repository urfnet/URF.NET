using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Northwind.Entities.Models.Mapping
{
    public class AlphabeticalListOfProductMap : EntityTypeConfiguration<AlphabeticalListOfProduct>
    {
        public AlphabeticalListOfProductMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName, t.Discontinued, t.CategoryName });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.QuantityPerUnit)
                .HasMaxLength(20);

            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("AlphabeticalListOfProducts");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitsInStock).HasColumnName("UnitsInStock");
            this.Property(t => t.UnitsOnOrder).HasColumnName("UnitsOnOrder");
            this.Property(t => t.ReorderLevel).HasColumnName("ReorderLevel");
            this.Property(t => t.Discontinued).HasColumnName("Discontinued");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
        }
    }
}
