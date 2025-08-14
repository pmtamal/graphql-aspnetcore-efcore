namespace GraphQLAgentApp.Models.Dtos
{
    public class UserProfileDto
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
    }
}
