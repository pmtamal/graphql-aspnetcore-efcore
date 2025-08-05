namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Author exposed in API
    /// </summary>
    public class AuthorGraphQLModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? Website { get; set; }
        
        // GraphQL-specific computed properties
        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateOfBirth.HasValue ? DateTime.Now.Year - DateOfBirth.Value.Year : 0;
        public string? ShortBiography => Biography?.Length > 100 ? Biography[..100] + "..." : Biography;
    }
} 