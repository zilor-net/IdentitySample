using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentitySample.Models.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    Id = new Guid("4978e592-5f55-40a4-9664-ded725b1ecae"),
                    Name = "Zilor",
                    Age = 99,
                    Position = ".NET 架构师"
                },
                new Employee
                {
                    Id = new Guid("6ea414d0-7f17-49d2-b763-546aa9e3da49"),
                    Name = "Kevin",
                    Age = 88,
                    Position = ".NET 架构师"
                }
            );
        }
    }
}
