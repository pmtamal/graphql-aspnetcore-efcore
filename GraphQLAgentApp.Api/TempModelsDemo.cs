// This is a temporary demonstration file showing the Models project structure
// This file should be moved to: GraphQLAgentApp.Models\Entities\Book.cs

namespace GraphQLAgentApp.Models.Entities
{
    /// <summary>
    /// Database entity for Book
    /// </summary>
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}

// Service DTO - should be in: GraphQLAgentApp.Models\Dtos\BookDto.cs
namespace GraphQLAgentApp.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object for Book used by Service layer
    /// </summary>
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}

// GraphQL Model - should be in: GraphQLAgentApp.Models\GraphQL\BookGraphQLModel.cs
namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Book exposed in API
    /// </summary>
    public class BookGraphQLModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string FullTitle => $"{Title} by {Author}";
        public string DisplayName => Title.ToUpper();
    }
}