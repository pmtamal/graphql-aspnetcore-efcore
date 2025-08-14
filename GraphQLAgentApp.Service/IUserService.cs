using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Service
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(string username, string email);
        Task<bool> IsAdminAsync(int userId);
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task<UserDto?> AuthenticateAsync(string username, string password);
    }
}
