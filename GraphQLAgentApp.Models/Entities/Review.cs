namespace GraphQLAgentApp.Models.Entities
{
    /// <summary>
    /// Database entity for Book Review
    /// </summary>
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; } // 1-5 stars
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
} 