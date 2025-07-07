using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeListApplication.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeListApplication.Core.Infrastructure
{
    public class ApplicationDbContext : IdentityUserContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Position).IsRequired().HasMaxLength(100);

                // Database seeder
                entity.HasData(
                    new Employee
                    {
                        Id = new Guid("a3e61343-b0a4-48f9-a5c6-fd6c93c3f60d"),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@company.com",
                        PhoneNumber = "123-456-7890",
                        Position = "Software Developer"
                    },
                    new Employee
                    {
                        Id = new Guid("7242f539-229a-4054-b247-e62b5fd17478"),
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@company.com",
                        PhoneNumber = "987-654-3210",
                        Position = "HR Manager"
                    }
                );
            });


        }
    }
}
