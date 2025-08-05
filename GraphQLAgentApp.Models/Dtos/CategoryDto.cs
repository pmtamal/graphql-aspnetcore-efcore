namespace GraphQLAgentApp.Models.Dtos
{
    /// <summary>
    /// Data Transfer Object for Category used by Service layer
    /// </summary>
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        // Navigation properties
        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }
} 