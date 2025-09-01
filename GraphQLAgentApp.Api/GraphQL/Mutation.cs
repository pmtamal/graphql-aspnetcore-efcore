using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Repository;
using HotChocolate;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Mutation(IMappingService mappingService)
    {
        public async Task<BookGraphQLModel> AddBook(
            [Service] IBookService service,
            string title, 
            int authorId, 
            int categoryId, 
            string isbn, 
            string description, 
            int publicationYear, 
            string publisher, 
            int pages, 
            string language, 
            decimal price, 
            int stockQuantity)
        {
            var bookDto = await service.AddAsync(title, authorId, categoryId, isbn, description, publicationYear, publisher, pages, language, price, stockQuantity);
            return mappingService.Map<BookGraphQLModel>(bookDto);
        }

        public async Task<UserGraphQLModel> Register(
            [Service] IUserService userService,
            [Service] IUserProfileRepository userProfileRepository,
            string username, 
            string email, 
            string password, 
            string firstName, 
            string lastName)
        {
            var userDto = new GraphQLAgentApp.Models.Dtos.UserDto
            {
                Username = username,
                Email = email,
                PasswordHash = password, // Will be hashed in service
                IsAdmin = false,
                IsActive = true
            };

            var createdUser = await userService.CreateAsync(userDto);
            
            // Create user profile
            var userProfileDto = new GraphQLAgentApp.Models.Dtos.UserProfileDto
            {
                UserId = createdUser.Id,
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateTime.UtcNow
            };
            
            var createdProfile = await userProfileRepository.CreateAsync(userProfileDto);
            
            // Get the complete user with profile
            var completeUser = await userService.GetByIdAsync(createdUser.Id);
            return mappingService.Map<UserGraphQLModel>(completeUser);
        }
    }
}