namespace GraphQLAgentApp.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object for Review used by Service layer
    /// </summary>
    public class ReviewDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        // Navigation properties
        public BookDto? Book { get; set; }
        public CustomerDto? Customer { get; set; }
    }
} 