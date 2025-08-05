namespace GraphQLAgentApp.Models.Entities
{
    /// <summary>
    /// Database entity for Book
    /// </summary>
    public class Book
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
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Author Author { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}