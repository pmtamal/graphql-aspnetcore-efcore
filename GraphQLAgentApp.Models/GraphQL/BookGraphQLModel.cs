namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Book exposed in API
    /// </summary>
    public class BookGraphQLModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        
        // GraphQL-specific computed properties
        public string FullTitle => $"{Title} by {Author}";
        public string DisplayName => Title.ToUpper();
        public int TitleLength => Title.Length;
    }
}