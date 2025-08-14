using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Repository
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfileDto>> GetAllAsync();
        Task<UserProfileDto?> GetByIdAsync(int id);
        Task<UserProfileDto?> GetByUserIdAsync(int userId);
        Task<UserProfileDto> CreateAsync(UserProfileDto userProfileDto);
        Task<UserProfileDto> UpdateAsync(UserProfileDto userProfileDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int userId);
    }
}
