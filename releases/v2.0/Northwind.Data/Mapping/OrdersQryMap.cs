#region

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class OrdersQryMap : EntityTypeConfiguration<OrdersQry>
    {
        public OrdersQryMap()
        {
            // Primary Key
            HasKey(t => new {t.OrderID, t.CompanyName});

            // Properties
            Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.CustomerID)
                .IsFixedLength()
                .HasMaxLength(5);

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

            Property(t => t.CompanyName)
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

            // Table & Column Mappings
            ToTable("Orders Qry");
            Property(t => t.OrderID).HasColumnName("OrderID");
            Property(t => t.CustomerID).HasColumnName("CustomerID");
            Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            Property(t => t.OrderDate).HasColumnName("OrderDate");
            Property(t => t.RequiredDate).HasColumnName("RequiredDate");
            Property(t => t.ShippedDate).HasColumnName("ShippedDate");
            Property(t => t.ShipVia).HasColumnName("ShipVia");
            Property(t => t.Freight).HasColumnName("Freight");
            Property(t => t.ShipName).HasColumnName("ShipName");
            Property(t => t.ShipAddress).HasColumnName("ShipAddress");
            Property(t => t.ShipCity).HasColumnName("ShipCity");
            Property(t => t.ShipRegion).HasColumnName("ShipRegion");
            Property(t => t.ShipPostalCode).HasColumnName("ShipPostalCode");
            Property(t => t.ShipCountry).HasColumnName("ShipCountry");
            Property(t => t.CompanyName).HasColumnName("CompanyName");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.Region).HasColumnName("Region");
            Property(t => t.PostalCode).HasColumnName("PostalCode");
            Property(t => t.Country).HasColumnName("Country");
        }
    }
}