namespace GraphQLAgentApp.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object for Book used by Service layer
    /// </summary>
    public class BookDto
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
        public DateTime? CreatedAt { get; set; }
        
        // Navigation properties
        public AuthorDto? Author { get; set; }
        public CategoryDto? Category { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }
}