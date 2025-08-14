using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAgentApp.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;

        public UserProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfileDto>> GetAllAsync()
        {
            var profiles = await _context.UserProfiles
                .Select(p => new UserProfileDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Address = p.Address,
                    City = p.City,
                    State = p.State,
                    PostalCode = p.PostalCode,
                    Country = p.Country,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    Bio = p.Bio,
                    ProfilePicture = p.ProfilePicture,
                    Website = p.Website,
                    LinkedIn = p.LinkedIn,
                    Twitter = p.Twitter,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            return profiles;
        }

        public async Task<UserProfileDto?> GetByIdAsync(int id)
        {
            var profile = await _context.UserProfiles
                .Where(p => p.Id == id)
                .Select(p => new UserProfileDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Address = p.Address,
                    City = p.City,
                    State = p.State,
                    PostalCode = p.PostalCode,
                    Country = p.Country,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    Bio = p.Bio,
                    ProfilePicture = p.ProfilePicture,
                    Website = p.Website,
                    LinkedIn = p.LinkedIn,
                    Twitter = p.Twitter,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();

            return profile;
        }

        public async Task<UserProfileDto?> GetByUserIdAsync(int userId)
        {
            var profile = await _context.UserProfiles
                .Where(p => p.UserId == userId)
                .Select(p => new UserProfileDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone,
                    Address = p.Address,
                    City = p.City,
                    State = p.State,
                    PostalCode = p.PostalCode,
                    Country = p.Country,
                    DateOfBirth = p.DateOfBirth,
                    Gender = p.Gender,
                    Bio = p.Bio,
                    ProfilePicture = p.ProfilePicture,
                    Website = p.Website,
                    LinkedIn = p.LinkedIn,
                    Twitter = p.Twitter,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();

            return profile;
        }

        public async Task<UserProfileDto> CreateAsync(UserProfileDto userProfileDto)
        {
            var profile = new UserProfile
            {
                UserId = userProfileDto.UserId,
                FirstName = userProfileDto.FirstName,
                LastName = userProfileDto.LastName,
                Phone = userProfileDto.Phone,
                Address = userProfileDto.Address,
                City = userProfileDto.City,
                State = userProfileDto.State,
                PostalCode = userProfileDto.PostalCode,
                Country = userProfileDto.Country,
                DateOfBirth = userProfileDto.DateOfBirth,
                Gender = userProfileDto.Gender,
                Bio = userProfileDto.Bio,
                ProfilePicture = userProfileDto.ProfilePicture,
                Website = userProfileDto.Website,
                LinkedIn = userProfileDto.LinkedIn,
                Twitter = userProfileDto.Twitter,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();

            userProfileDto.Id = profile.Id;
            userProfileDto.CreatedAt = profile.CreatedAt;
            return userProfileDto;
        }

        public async Task<UserProfileDto> UpdateAsync(UserProfileDto userProfileDto)
        {
            var profile = await _context.UserProfiles.FindAsync(userProfileDto.Id);
            if (profile == null)
                throw new ArgumentException("UserProfile not found");

            profile.FirstName = userProfileDto.FirstName;
            profile.LastName = userProfileDto.LastName;
            profile.Phone = userProfileDto.Phone;
            profile.Address = userProfileDto.Address;
            profile.City = userProfileDto.City;
            profile.State = userProfileDto.State;
            profile.PostalCode = userProfileDto.PostalCode;
            profile.Country = userProfileDto.Country;
            profile.DateOfBirth = userProfileDto.DateOfBirth;
            profile.Gender = userProfileDto.Gender;
            profile.Bio = userProfileDto.Bio;
            profile.ProfilePicture = userProfileDto.ProfilePicture;
            profile.Website = userProfileDto.Website;
            profile.LinkedIn = userProfileDto.LinkedIn;
            profile.Twitter = userProfileDto.Twitter;
            profile.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return userProfileDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null)
                return false;

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await _context.UserProfiles
                .AnyAsync(p => p.UserId == userId);
        }
    }
}
