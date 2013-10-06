#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            // Primary Key
            HasKey(t => new {t.CustomerName, t.Salesperson, t.OrderID, t.ShipperName, t.ProductID, t.ProductName, t.UnitPrice, t.Quantity, t.Discount});

            // Properties
            Property(t => t.ShipName)
                .HasMaxLength(40);

            Property(t => t.ShipAddress)
                .HasMaxLength(60);

            Property(t => t.ShipCity)
                .HasMaxLength(15);

            Property(t => t.ShipRegion)
                .HasMaxLength(15);

            Property(t => t.ShipPostalCode)
                .HasMaxLength(10);

            Property(t => t.ShipCountry)
                .HasMaxLength(15);

            Property(t => t.CustomerID)
                .IsFixedLength()
                .HasMaxLength(5);

            Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(40);

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

            Property(t => t.Salesperson)
                .IsRequired()
                .HasMaxLength(31);

            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ShipperName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(40);

            Property(t => t.UnitPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("Invoices");
            Property(t => t.ShipName).HasColumnName("ShipName");
            Property(t => t.ShipAddress).HasColumnName("ShipAddress");
            Property(t => t.ShipCity).HasColumnName("ShipCity");
            Property(t => t.ShipRegion).HasColumnName("ShipRegion");
            Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
            Property(t => t.ShipCountry).HasColumnName("ShipCountry");
            Property(t => t.CustomerID).HasColumnName("CustomerID");
            Property(t => t.CustomerName).HasColumnName("CustomerName");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.Region).HasColumnName("Region");
            Property(t => t.PostalCode).HasColumnName("PostalCode");
            Property(t => t.Country).HasColumnName("Country");
            Property(t => t.Salesperson).HasColumnName("Salesperson");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.OrderDate).HasColumnName("OrderDate");
            Property(t => t.RequiredDate).HasColumnName("RequiredDate");
            Property(t => t.ShippedDate).HasColumnName("ShippedDate");
            Property(t => t.ShipperName).HasColumnName("ShipperName");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.ProductName).HasColumnName("ProductName");
            Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            Property(t => t.Quantity).HasColumnName("Quantity");
            Property(t => t.Discount).HasColumnName("Discount");
            Property(t => t.ExtendedPrice).HasColumnName("ExtendedPrice");
            Property(t => t.Freight).HasColumnName("Freight");
        }
    }
}