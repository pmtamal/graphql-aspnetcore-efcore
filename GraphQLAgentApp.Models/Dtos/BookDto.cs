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
        public DateTime? CreatedAt { get; set; }
    }
}