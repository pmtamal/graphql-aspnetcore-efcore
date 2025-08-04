namespace GraphQLAgentApp.Api.GraphQL.Types
{
    public class BookType
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime? PublishedDate { get; set; }
        public string? Description { get; set; }
    }
}