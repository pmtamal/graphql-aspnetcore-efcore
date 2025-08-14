using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLAgentApp.Repository.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(u => u.IsAdmin)
                .HasDefaultValue(false);
            
            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);
            
            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            // Unique constraints
            builder.HasIndex(u => u.Username)
                .IsUnique();
            
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
