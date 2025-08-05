namespace GraphQLAgentApp.Models.GraphQL
{
    /// <summary>
    /// GraphQL model for Customer exposed in API
    /// </summary>
    public class CustomerGraphQLModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        // GraphQL-specific computed properties
        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public string? FullAddress => $"{Address}, {City}, {State} {PostalCode}, {Country}".Trim(',', ' ');
        public string? ShortAddress => $"{City}, {State}".Trim(',', ' ');
    }
} 