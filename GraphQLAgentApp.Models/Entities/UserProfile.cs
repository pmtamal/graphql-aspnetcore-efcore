using System.ComponentModel.DataAnnotations;

namespace GraphQLAgentApp.Models.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        
        // Foreign key to User (one-to-one relationship)
        public int UserId { get; set; }
        
        // Personal Information
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Phone { get; set; }
        
        [StringLength(200)]
        public string? Address { get; set; }
        
        [StringLength(50)]
        public string? City { get; set; }
        
        [StringLength(50)]
        public string? State { get; set; }
        
        [StringLength(20)]
        public string? PostalCode { get; set; }
        
        [StringLength(50)]
        public string? Country { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        // Additional personal fields for future extensibility
        [StringLength(10)]
        public string? Gender { get; set; }
        
        [StringLength(500)]
        public string? Bio { get; set; }
        
        [StringLength(200)]
        public string? ProfilePicture { get; set; }
        
        [StringLength(100)]
        public string? Website { get; set; }
        
        [StringLength(100)]
        public string? LinkedIn { get; set; }
        
        [StringLength(100)]
        public string? Twitter { get; set; }
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation property
        public virtual User User { get; set; } = null!;
    }
}
