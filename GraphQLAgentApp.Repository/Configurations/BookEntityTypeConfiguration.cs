using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLAgentApp.Repository.Configurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            var staticCreatedAt = new DateTime(2020, 1, 1);

            builder.HasData(
                new Book { Id = 1, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 3, Title = "Domain-Driven Design", Author = "Eric Evans", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 4, Title = "Refactoring", Author = "Martin Fowler", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 5, Title = "Test-Driven Development", Author = "Kent Beck", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 6, Title = "Patterns of Enterprise Application Architecture", Author = "Martin Fowler", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 7, Title = "Working Effectively with Legacy Code", Author = "Michael Feathers", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 8, Title = "The Clean Coder", Author = "Robert C. Martin", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 9, Title = "Continuous Delivery", Author = "Jez Humble", CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 10, Title = "You Don't Know JS", Author = "Kyle Simpson", CreatedAt = staticCreatedAt, UpdatedAt = null }
            );
        }
    }
}
