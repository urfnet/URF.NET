using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

namespace Northwind.Data.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderID);

            // Properties
            this.Property(t => t.CustomerID)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.ShipName)
                .HasMaxLength(40);

            this.Property(t => t.ShipAddress)
                .HasMaxLength(60);

            this.Property(t => t.ShipCity)
                .HasMaxLength(15);

            this.Property(t => t.ShipRegion)
                .HasMaxLength(15);

            this.Property(t => t.ShipPostalCode)
                .HasMaxLength(10);

            this.Property(t => t.ShipCountry)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Orders");
            this.Property(t => t.OrderID).HasColumnName("Order ID");
            this.Property(t => t.CustomerID).HasColumnName("Customer ID");
            this.Property(t => t.EmployeeID).HasColumnName("Employee ID");
            this.Property(t => t.OrderDate).HasColumnName("Order Date");
            this.Property(t => t.RequiredDate).HasColumnName("Required Date");
            this.Property(t => t.ShippedDate).HasColumnName("Shipped Date");
            this.Property(t => t.ShipVia).HasColumnName("Ship Via");
            this.Property(t => t.Freight).HasColumnName("Freight");
            this.Property(t => t.ShipName).HasColumnName("Ship Name");
            this.Property(t => t.ShipAddress).HasColumnName("Ship Address");
            this.Property(t => t.ShipCity).HasColumnName("Ship City");
            this.Property(t => t.ShipRegion).HasColumnName("Ship Region");
            this.Property(t => t.ShipPostalCode).HasColumnName("Ship Postal Code");
            this.Property(t => t.ShipCountry).HasColumnName("Ship Country");

            // Relationships
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.EmployeeID);
            this.HasOptional(t => t.Shipper)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ShipVia);

        }
    }
}
