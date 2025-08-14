namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Review exposed in API
    /// </summary>
    public class ReviewGraphQLModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        
        // Navigation properties
        public BookGraphQLModel? Book { get; set; }
        public UserGraphQLModel? User { get; set; }
        
        // GraphQL-specific computed properties
        public string StarRating => new string('★', Rating) + new string('☆', 5 - Rating);
        public bool IsPositiveReview => Rating >= 4;
        public bool IsNegativeReview => Rating <= 2;
        public string? ShortComment => Comment?.Length > 100 ? Comment[..100] + "..." : Comment;
    }
} 