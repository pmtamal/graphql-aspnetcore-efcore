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
                new Book { Id = 1, Title = "The Pragmatic Programmer", ISBN = "978-0201616224", AuthorId = 1, CategoryId = 1, Description = "A guide to software development", PublicationYear = 1999, Publisher = "Addison-Wesley", Pages = 352, Language = "English", Price = 49.99m, StockQuantity = 25, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 2, Title = "Clean Code", ISBN = "978-0132350884", AuthorId = 2, CategoryId = 7, Description = "A handbook of agile software craftsmanship", PublicationYear = 2008, Publisher = "Prentice Hall", Pages = 464, Language = "English", Price = 44.99m, StockQuantity = 30, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 3, Title = "Domain-Driven Design", ISBN = "978-0321125217", AuthorId = 3, CategoryId = 8, Description = "Tackling complexity in the heart of software", PublicationYear = 2003, Publisher = "Addison-Wesley", Pages = 560, Language = "English", Price = 54.99m, StockQuantity = 20, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 4, Title = "Refactoring", ISBN = "978-0201485677", AuthorId = 4, CategoryId = 7, Description = "Improving the design of existing code", PublicationYear = 1999, Publisher = "Addison-Wesley", Pages = 448, Language = "English", Price = 39.99m, StockQuantity = 15, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 5, Title = "Test-Driven Development", ISBN = "978-0321146533", AuthorId = 5, CategoryId = 3, Description = "By example", PublicationYear = 2002, Publisher = "Addison-Wesley", Pages = 240, Language = "English", Price = 34.99m, StockQuantity = 18, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 6, Title = "Patterns of Enterprise Application Architecture", ISBN = "978-0321127426", AuthorId = 4, CategoryId = 2, Description = "Patterns for enterprise applications", PublicationYear = 2002, Publisher = "Addison-Wesley", Pages = 560, Language = "English", Price = 49.99m, StockQuantity = 12, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 7, Title = "Working Effectively with Legacy Code", ISBN = "978-0131177055", AuthorId = 6, CategoryId = 6, Description = "Strategies for working with legacy code", PublicationYear = 2004, Publisher = "Prentice Hall", Pages = 464, Language = "English", Price = 42.99m, StockQuantity = 8, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 8, Title = "The Clean Coder", ISBN = "978-0137081073", AuthorId = 2, CategoryId = 7, Description = "A code of conduct for professional programmers", PublicationYear = 2011, Publisher = "Prentice Hall", Pages = 256, Language = "English", Price = 39.99m, StockQuantity = 22, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 9, Title = "Continuous Delivery", ISBN = "978-0321601919", AuthorId = 7, CategoryId = 4, Description = "Reliable software releases through build, test, and deployment automation", PublicationYear = 2010, Publisher = "Addison-Wesley", Pages = 512, Language = "English", Price = 44.99m, StockQuantity = 16, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null },
                new Book { Id = 10, Title = "You Don't Know JS", ISBN = "978-1491904244", AuthorId = 8, CategoryId = 5, Description = "Up & Going", PublicationYear = 2015, Publisher = "O'Reilly Media", Pages = 88, Language = "English", Price = 19.99m, StockQuantity = 35, IsAvailable = true, CreatedAt = staticCreatedAt, UpdatedAt = null }
            );
        }
    }
}
