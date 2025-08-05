namespace GraphQLAgentApp.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object for Author used by Service layer
    /// </summary>
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? Website { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        // Navigation properties
        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }
} 