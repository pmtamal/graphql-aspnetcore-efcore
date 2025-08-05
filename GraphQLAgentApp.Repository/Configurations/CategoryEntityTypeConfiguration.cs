using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLAgentApp.Repository.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var staticCreatedAt = new DateTime(2020, 1, 1);

            builder.HasData(
                new Category { Id = 1, Name = "Programming", Description = "Books about programming and software development", CreatedAt = staticCreatedAt },
                new Category { Id = 2, Name = "Software Architecture", Description = "Books about software architecture and design patterns", CreatedAt = staticCreatedAt },
                new Category { Id = 3, Name = "Testing", Description = "Books about software testing and quality assurance", CreatedAt = staticCreatedAt },
                new Category { Id = 4, Name = "DevOps", Description = "Books about DevOps and continuous delivery", CreatedAt = staticCreatedAt },
                new Category { Id = 5, Name = "JavaScript", Description = "Books about JavaScript and web development", CreatedAt = staticCreatedAt },
                new Category { Id = 6, Name = "Legacy Code", Description = "Books about working with legacy code", CreatedAt = staticCreatedAt },
                new Category { Id = 7, Name = "Clean Code", Description = "Books about writing clean and maintainable code", CreatedAt = staticCreatedAt },
                new Category { Id = 8, Name = "Domain-Driven Design", Description = "Books about domain-driven design", CreatedAt = staticCreatedAt }
            );
        }
    }
} 