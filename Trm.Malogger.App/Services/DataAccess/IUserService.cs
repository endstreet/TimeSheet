using razor.Components.Models;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public interface IUserService
    {
        Task<User?> Activate(string otp);
        //Task CreateRoleAsync(Role newRole);
        Task<User> CreateUserAsync(User newUser);
        Task CreateUserProjectAsync(UserProject newUserProject);
        Task CreateUserRoleAsync(UserRole newUserRole);
        Task<PagedResult<User>> GetPagedUsersAsync(int page = 1);
        Task<Role> GetRoleAsync(int id);
        Task<List<Role>> GetRolesAsync();
        //Task<User> GetUserAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> GetUserByOtpAsync(string otp);
        Task<UserProject> GetUserProjectAsync(int id);
        Task<List<UserProject>> GetUserProjectsAsync();
        Task<UserRole> GetUserRoleAsync(int id);
        Task<List<User>> GetUsersAsync();
        Task<List<Project>> GetUsersProjectsAsync(int userId);
        Task<List<Role>> GetUsersRolesAsync(int userId);
        Task<User> OnboardUser(User newUser);
        //Task RemoveRoleAsync(int id);
        Task RemoveUserAsync(int id);
        Task RemoveUserProjectAsync(int id);
        Task RemoveUserProjectAsync(int projectId, int userId);
        Task RemoveUserRoleAsync(int Id);
        Task RemoveUserRoleAsync(int roleId, int userId);
        //Task UpdateRoleAsync(int Id, Role updatedRole);
        Task UpdateUserAsync(int id, User updatedUser);
        Task UpdateUserProjectAsync(int Id, UserProject updatedUserProject);
        //Task UpdateUserRoleAsync(int Id, UserRole updatedUserRole);
    }
}