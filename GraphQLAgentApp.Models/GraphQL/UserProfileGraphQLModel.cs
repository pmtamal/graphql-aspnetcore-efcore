namespace GraphQLAgentApp.Models.GraphQL
{
    public class UserProfileGraphQLModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        // Personal Information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
        // Additional personal fields
        public string? Gender { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Website { get; set; }
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation property
        public UserGraphQLModel? User { get; set; }
        
        // GraphQL-specific computed properties
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string DisplayName => !string.IsNullOrEmpty(FullName) ? FullName : "User";
        public string? FullAddress => !string.IsNullOrEmpty(Address) 
            ? $"{Address}, {City}, {State} {PostalCode}, {Country}".Replace(", ,", ",").Replace("  ", " ").Trim()
            : null;
        public int Age => DateOfBirth.HasValue 
            ? DateTime.UtcNow.Year - DateOfBirth.Value.Year - (DateTime.UtcNow < DateOfBirth.Value.AddYears(DateTime.UtcNow.Year - DateOfBirth.Value.Year) ? 1 : 0)
            : 0;
        public bool HasSocialLinks => !string.IsNullOrEmpty(Website) || !string.IsNullOrEmpty(LinkedIn) || !string.IsNullOrEmpty(Twitter);
        public bool IsCompleteProfile => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Phone);
    }
}
