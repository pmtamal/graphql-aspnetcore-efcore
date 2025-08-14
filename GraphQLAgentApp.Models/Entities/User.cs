using System.ComponentModel.DataAnnotations;

namespace GraphQLAgentApp.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } = string.Empty;
        
        public bool IsAdmin { get; set; } = false;
        
        public bool IsActive { get; set; } = true;
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastLoginAt { get; set; }
        
        // Navigation properties
        public virtual UserProfile? Profile { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}
