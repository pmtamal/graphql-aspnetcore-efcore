using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLAgentApp.Repository.Configurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var staticCreatedAt = new DateTime(2020, 1, 1);

            builder.HasData(
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", Phone = "+1-555-0101", Address = "123 Main St", City = "New York", State = "NY", PostalCode = "10001", Country = "USA", DateOfBirth = new DateTime(1985, 5, 15), CreatedAt = staticCreatedAt },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", Phone = "+1-555-0102", Address = "456 Oak Ave", City = "Los Angeles", State = "CA", PostalCode = "90210", Country = "USA", DateOfBirth = new DateTime(1990, 8, 22), CreatedAt = staticCreatedAt },
                new Customer { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@email.com", Phone = "+1-555-0103", Address = "789 Pine Rd", City = "Chicago", State = "IL", PostalCode = "60601", Country = "USA", DateOfBirth = new DateTime(1982, 12, 10), CreatedAt = staticCreatedAt },
                new Customer { Id = 4, FirstName = "Alice", LastName = "Brown", Email = "alice.brown@email.com", Phone = "+1-555-0104", Address = "321 Elm St", City = "Houston", State = "TX", PostalCode = "77001", Country = "USA", DateOfBirth = new DateTime(1988, 3, 7), CreatedAt = staticCreatedAt },
                new Customer { Id = 5, FirstName = "Charlie", LastName = "Wilson", Email = "charlie.wilson@email.com", Phone = "+1-555-0105", Address = "654 Maple Dr", City = "Phoenix", State = "AZ", PostalCode = "85001", Country = "USA", DateOfBirth = new DateTime(1995, 11, 18), CreatedAt = staticCreatedAt }
            );
        }
    }
} 