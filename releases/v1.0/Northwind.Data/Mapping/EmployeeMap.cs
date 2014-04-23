#region

using System.Data.Entity.ModelConfiguration;
using Northwind.Entity.Models;

#endregion

namespace Northwind.Data.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            HasKey(t => t.EmployeeID);

            // Properties
            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.Title)
                .HasMaxLength(30);

            Property(t => t.TitleOfCourtesy)
                .HasMaxLength(25);

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

            Property(t => t.HomePhone)
                .HasMaxLength(24);

            Property(t => t.Extension)
                .HasMaxLength(4);

            Property(t => t.PhotoPath)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("Employees");
            Property(t => t.EmployeeID).HasColumnName("Employee ID");
            Property(t => t.LastName).HasColumnName("Last Name");
            Property(t => t.FirstName).HasColumnName("First Name");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.TitleOfCourtesy).HasColumnName("TitleOfCourtesy");
            Property(t => t.BirthDate).HasColumnName("BirthDate");
            Property(t => t.HireDate).HasColumnName("HireDate");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.Region).HasColumnName("Region");
            Property(t => t.PostalCode).HasColumnName("PostalCode");
            Property(t => t.Country).HasColumnName("Country");
            Property(t => t.HomePhone).HasColumnName("HomePhone");
            Property(t => t.Extension).HasColumnName("Extension");
            Property(t => t.Photo).HasColumnName("Photo");
            Property(t => t.Notes).HasColumnName("Notes");
            Property(t => t.ReportsTo).HasColumnName("ReportsTo");
            Property(t => t.PhotoPath).HasColumnName("PhotoPath");

            // Relationships
            HasMany(t => t.Territories)
                .WithMany(t => t.Employees)
                .Map(m =>
                {
                    m.ToTable("EmployeeTerritories");
                    m.MapLeftKey("EmployeeID");
                    m.MapRightKey("TerritoryID");
                });

            HasOptional(t => t.Employee1)
                .WithMany(t => t.Employees1)
                .HasForeignKey(d => d.ReportsTo);
        }
    }
}