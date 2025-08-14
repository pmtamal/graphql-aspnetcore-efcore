namespace GraphQLAgentApp.Models.GraphQL
{
    public class UserGraphQLModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        
        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        
        // Navigation properties
        public UserProfileGraphQLModel? Profile { get; set; }
        public List<OrderGraphQLModel> Orders { get; set; } = new List<OrderGraphQLModel>();
        public List<ReviewGraphQLModel> Reviews { get; set; } = new List<ReviewGraphQLModel>();
        
        // GraphQL-specific computed properties
        public string DisplayName => Profile?.DisplayName ?? Username;
        public bool IsNewUser => CreatedAt > DateTime.UtcNow.AddDays(-7);
        public string UserType => IsAdmin ? "Admin" : "User";
        public int OrderCount => Orders.Count;
        public int ReviewCount => Reviews.Count;
        public bool HasProfile => Profile != null;
        public bool IsProfileComplete => Profile?.IsCompleteProfile ?? false;
    }
}
