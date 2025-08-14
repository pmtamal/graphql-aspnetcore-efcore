using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Repository;
using System.Security.Cryptography;
using System.Text;

namespace GraphQLAgentApp.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            // Hash the password before storing
            userDto.PasswordHash = HashPassword(userDto.PasswordHash);
            return await _userRepository.CreateAsync(userDto);
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            return await _userRepository.UpdateAsync(userDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string username, string email)
        {
            return await _userRepository.ExistsAsync(username, email);
        }

        public async Task<bool> IsAdminAsync(int userId)
        {
            return await _userRepository.IsAdminAsync(userId);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !user.IsActive)
                return false;

            // For now, we'll use a simple hash comparison
            // In production, you should use proper password hashing like BCrypt
            var hashedPassword = HashPassword(password);
            return hashedPassword == user.PasswordHash;
        }

        public async Task<UserDto?> AuthenticateAsync(string username, string password)
        {
            if (await ValidateCredentialsAsync(username, password))
            {
                var user = await _userRepository.GetByUsernameAsync(username);
                if (user != null)
                {
                    // Update last login time
                    user.LastLoginAt = DateTime.UtcNow;
                    await _userRepository.UpdateAsync(user);
                }
                return user;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
