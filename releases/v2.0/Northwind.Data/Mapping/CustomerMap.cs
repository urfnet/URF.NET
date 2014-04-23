#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            HasKey(t => t.CustomerID);

            // Properties
            Property(t => t.CustomerID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.ContactName)
                .HasMaxLength(30);

            Property(t => t.ContactTitle)
                .HasMaxLength(30);

            Property(t => t.Address)
                .HasMaxLength(60);

            Property(t => t.City)
                .HasMaxLength(15);

            Property(t => t.Region)
                .HasMaxLength(15);

            Property(t => t.PostalCode)
                .HasMaxLength(10);

            Property(t => t.Country)
                .HasMaxLength(15);

            Property(t => t.Phone)
                .HasMaxLength(24);

            Property(t => t.Fax)
                .HasMaxLength(24);

            // Table & Column Mappings
            ToTable("Customers");
            Property(t => t.CustomerID).HasColumnName("Customer ID");
            Property(t => t.CompanyName).HasColumnName("Company Name");
            Property(t => t.ContactName).HasColumnName("Contact Name");
            Property(t => t.ContactTitle).HasColumnName("Contact Title");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.Region).HasColumnName("Region");
            Property(t => t.PostalCode).HasColumnName("PostalCode");
            Property(t => t.Country).HasColumnName("Country");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.Fax).HasColumnName("Fax");
        }
    }
}