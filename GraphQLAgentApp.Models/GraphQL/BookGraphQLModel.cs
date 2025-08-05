namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Book exposed in API
    /// </summary>
    public class BookGraphQLModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public string? Publisher { get; set; }
        public int Pages { get; set; }
        public string? Language { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        
        // Navigation properties
        public AuthorGraphQLModel? Author { get; set; }
        public CategoryGraphQLModel? Category { get; set; }
        public List<ReviewGraphQLModel> Reviews { get; set; } = new List<ReviewGraphQLModel>();
        
        // GraphQL-specific computed properties
        public string FullTitle => $"{Title} by {Author?.FullName ?? "Unknown Author"}";
        public string DisplayName => Title.ToUpper();
        public int TitleLength => Title.Length;
        public string FormattedPrice => $"${Price:F2}";
        public bool IsInStock => StockQuantity > 0;
        public bool IsLowStock => StockQuantity > 0 && StockQuantity <= 5;
        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        public string StarRating => new string('★', (int)Math.Round(AverageRating)) + new string('☆', 5 - (int)Math.Round(AverageRating));
        public int ReviewCount => Reviews.Count;
        public string? ShortDescription => Description?.Length > 150 ? Description[..150] + "..." : Description;
    }
}