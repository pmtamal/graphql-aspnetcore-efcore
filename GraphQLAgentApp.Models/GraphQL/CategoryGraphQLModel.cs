namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Category exposed in API
    /// </summary>
    public class CategoryGraphQLModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        // GraphQL-specific computed properties
        public string DisplayName => Name.ToUpper();
        public string? ShortDescription => Description?.Length > 50 ? Description[..50] + "..." : Description;
    }
} 