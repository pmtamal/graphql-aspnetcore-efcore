using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLAgentApp.Repository.Configurations
{
    public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.UserId)
                .IsRequired();
            
            builder.Property(p => p.FirstName)
                .HasMaxLength(50);
            
            builder.Property(p => p.LastName)
                .HasMaxLength(50);
            
            builder.Property(p => p.Phone)
                .HasMaxLength(20);
            
            builder.Property(p => p.Address)
                .HasMaxLength(200);
            
            builder.Property(p => p.City)
                .HasMaxLength(50);
            
            builder.Property(p => p.State)
                .HasMaxLength(50);
            
            builder.Property(p => p.PostalCode)
                .HasMaxLength(20);
            
            builder.Property(p => p.Country)
                .HasMaxLength(50);
            
            builder.Property(p => p.Gender)
                .HasMaxLength(10);
            
            builder.Property(p => p.Bio)
                .HasMaxLength(500);
            
            builder.Property(p => p.ProfilePicture)
                .HasMaxLength(200);
            
            builder.Property(p => p.Website)
                .HasMaxLength(100);
            
            builder.Property(p => p.LinkedIn)
                .HasMaxLength(100);
            
            builder.Property(p => p.Twitter)
                .HasMaxLength(100);
            
            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            // Unique constraint on UserId (one-to-one relationship)
            builder.HasIndex(p => p.UserId)
                .IsUnique();
        }
    }
}
