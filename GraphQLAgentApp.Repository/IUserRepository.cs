using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Repository
{
    public interface IUserRepository
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
    }
}
