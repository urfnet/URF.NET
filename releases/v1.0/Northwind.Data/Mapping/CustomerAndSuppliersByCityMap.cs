#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class CustomerAndSuppliersByCityMap : EntityTypeConfiguration<CustomerAndSuppliersByCity>
    {
        public CustomerAndSuppliersByCityMap()
        {
            // Primary Key
            HasKey(t => new {t.CompanyName, t.Relationship});

            // Properties
            Property(t => t.City)
                .HasMaxLength(15);

            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.ContactName)
                .HasMaxLength(30);

            Property(t => t.Relationship)
                .IsRequired()
                .HasMaxLength(9);

            // Table & Column Mappings
            ToTable("Customer and Suppliers by City");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.ContactName).HasColumnName("ContactName");
            Property(t => t.Relationship).HasColumnName("Relationship");
        }
    }
}