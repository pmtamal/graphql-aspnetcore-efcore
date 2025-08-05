using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLAgentApp.Repository.Configurations
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            var staticCreatedAt = new DateTime(2020, 1, 1);

            builder.HasData(
                new Author { Id = 1, FirstName = "Andrew", LastName = "Hunt", Biography = "Co-author of The Pragmatic Programmer", DateOfBirth = new DateTime(1964, 1, 1), Nationality = "American", CreatedAt = staticCreatedAt },
                new Author { Id = 2, FirstName = "Robert C.", LastName = "Martin", Biography = "Uncle Bob, software engineer and author", DateOfBirth = new DateTime(1952, 12, 5), Nationality = "American", CreatedAt = staticCreatedAt },
                new Author { Id = 3, FirstName = "Eric", LastName = "Evans", Biography = "Author of Domain-Driven Design", DateOfBirth = new DateTime(1965, 1, 1), Nationality = "American", CreatedAt = staticCreatedAt },
                new Author { Id = 4, FirstName = "Martin", LastName = "Fowler", Biography = "Software architect and author", DateOfBirth = new DateTime(1963, 12, 18), Nationality = "British", CreatedAt = staticCreatedAt },
                new Author { Id = 5, FirstName = "Kent", LastName = "Beck", Biography = "Software engineer and creator of Extreme Programming", DateOfBirth = new DateTime(1961, 3, 31), Nationality = "American", CreatedAt = staticCreatedAt },
                new Author { Id = 6, FirstName = "Michael", LastName = "Feathers", Biography = "Software consultant and author", DateOfBirth = new DateTime(1970, 1, 1), Nationality = "American", CreatedAt = staticCreatedAt },
                new Author { Id = 7, FirstName = "Jez", LastName = "Humble", Biography = "Software engineer and DevOps advocate", DateOfBirth = new DateTime(1975, 1, 1), Nationality = "British", CreatedAt = staticCreatedAt },
                new Author { Id = 8, FirstName = "Kyle", LastName = "Simpson", Biography = "JavaScript educator and author", DateOfBirth = new DateTime(1980, 1, 1), Nationality = "American", CreatedAt = staticCreatedAt }
            );
        }
    }
} 