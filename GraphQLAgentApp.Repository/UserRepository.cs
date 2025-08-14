using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAgentApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _context.Users
                .Include(u => u.Profile)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginAt = u.LastLoginAt,
                    Profile = u.Profile != null ? new UserProfileDto
                    {
                        Id = u.Profile.Id,
                        UserId = u.Profile.UserId,
                        FirstName = u.Profile.FirstName,
                        LastName = u.Profile.LastName,
                        Phone = u.Profile.Phone,
                        Address = u.Profile.Address,
                        City = u.Profile.City,
                        State = u.Profile.State,
                        PostalCode = u.Profile.PostalCode,
                        Country = u.Profile.Country,
                        DateOfBirth = u.Profile.DateOfBirth,
                        Gender = u.Profile.Gender,
                        Bio = u.Profile.Bio,
                        ProfilePicture = u.Profile.ProfilePicture,
                        Website = u.Profile.Website,
                        LinkedIn = u.Profile.LinkedIn,
                        Twitter = u.Profile.Twitter,
                        CreatedAt = u.Profile.CreatedAt,
                        UpdatedAt = u.Profile.UpdatedAt
                    } : null
                })
                .ToListAsync();

            return users;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginAt = u.LastLoginAt,
                    Profile = u.Profile != null ? new UserProfileDto
                    {
                        Id = u.Profile.Id,
                        UserId = u.Profile.UserId,
                        FirstName = u.Profile.FirstName,
                        LastName = u.Profile.LastName,
                        Phone = u.Profile.Phone,
                        Address = u.Profile.Address,
                        City = u.Profile.City,
                        State = u.Profile.State,
                        PostalCode = u.Profile.PostalCode,
                        Country = u.Profile.Country,
                        DateOfBirth = u.Profile.DateOfBirth,
                        Gender = u.Profile.Gender,
                        Bio = u.Profile.Bio,
                        ProfilePicture = u.Profile.ProfilePicture,
                        Website = u.Profile.Website,
                        LinkedIn = u.Profile.LinkedIn,
                        Twitter = u.Profile.Twitter,
                        CreatedAt = u.Profile.CreatedAt,
                        UpdatedAt = u.Profile.UpdatedAt
                    } : null
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .Where(u => u.Username == username)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginAt = u.LastLoginAt,
                    Profile = u.Profile != null ? new UserProfileDto
                    {
                        Id = u.Profile.Id,
                        UserId = u.Profile.UserId,
                        FirstName = u.Profile.FirstName,
                        LastName = u.Profile.LastName,
                        Phone = u.Profile.Phone,
                        Address = u.Profile.Address,
                        City = u.Profile.City,
                        State = u.Profile.State,
                        PostalCode = u.Profile.PostalCode,
                        Country = u.Profile.Country,
                        DateOfBirth = u.Profile.DateOfBirth,
                        Gender = u.Profile.Gender,
                        Bio = u.Profile.Bio,
                        ProfilePicture = u.Profile.ProfilePicture,
                        Website = u.Profile.Website,
                        LinkedIn = u.Profile.LinkedIn,
                        Twitter = u.Profile.Twitter,
                        CreatedAt = u.Profile.CreatedAt,
                        UpdatedAt = u.Profile.UpdatedAt
                    } : null
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .Where(u => u.Email == email)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginAt = u.LastLoginAt,
                    Profile = u.Profile != null ? new UserProfileDto
                    {
                        Id = u.Profile.Id,
                        UserId = u.Profile.UserId,
                        FirstName = u.Profile.FirstName,
                        LastName = u.Profile.LastName,
                        Phone = u.Profile.Phone,
                        Address = u.Profile.Address,
                        City = u.Profile.City,
                        State = u.Profile.State,
                        PostalCode = u.Profile.PostalCode,
                        Country = u.Profile.Country,
                        DateOfBirth = u.Profile.DateOfBirth,
                        Gender = u.Profile.Gender,
                        Bio = u.Profile.Bio,
                        ProfilePicture = u.Profile.ProfilePicture,
                        Website = u.Profile.Website,
                        LinkedIn = u.Profile.LinkedIn,
                        Twitter = u.Profile.Twitter,
                        CreatedAt = u.Profile.CreatedAt,
                        UpdatedAt = u.Profile.UpdatedAt
                    } : null
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash, // This should be hashed before calling this method
                IsAdmin = userDto.IsAdmin,
                IsActive = userDto.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.Id;
            userDto.CreatedAt = user.CreatedAt;
            return userDto;
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);
            if (user == null)
                throw new ArgumentException("User not found");

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.IsAdmin = userDto.IsAdmin;
            user.IsActive = userDto.IsActive;

            await _context.SaveChangesAsync();
            return userDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string username, string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username || u.Email == email);
        }

        public async Task<bool> IsAdminAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId && u.IsActive)
                .Select(u => u.IsAdmin)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
