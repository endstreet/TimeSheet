using Microsoft.EntityFrameworkCore;
using razor.Components.Models;
using Trm.MaLogger.MsData.Context;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public class UserService : IUserService
    {
        public UserService(MaLoggerDbContext context, StaticDataService sd)
        {
            _context = context;
            _sd = sd;
        }
        readonly MaLoggerDbContext _context;
        readonly StaticDataService _sd;

        #region users
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<PagedResult<User>> GetPagedUsersAsync(int page = 1)
        {
            PagedResult<User> users = new()
            {
                count = _context.Users.Count(),
                Result = await _context.Users.Skip(16 * (page - 1)).Take(16).ToListAsync()
            };

            return users;
        }

        //public async Task<User> GetUserAsync(int id)
        //{
        //    return await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        //}

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = _context.Users.Where(x => x.Email == email.ToLower());
            if (user.Any()) return await user.FirstAsync();
            return null;
        }

        public async Task<User> GetUserByOtpAsync(string otp)
        {
            return await _context.Users.Where(x => x.OTP == otp).FirstAsync();
        }
        public async Task<User?> Activate(string otp)
        {
            User user = await _context.Users.Where(x => x.OTP == otp).FirstAsync();
            if (user == null) return null;
            user.OTP = null;
            await UpdateUserAsync(user.Id, user);
            return user;
        }
        public async Task<User> OnboardUser(User newUser)
        {
            newUser = await CreateUserAsync(newUser);
            //if (newUser.Id == null) throw new Exception("OnboardUser: User creation failed!");
            //Get the Admin Role
            var admin = (await _context.Roles.Where(x => x.Name == "Admin").FirstAsync()) ?? throw new Exception("OnboardUser: No Admin Role!");
            //Create Admin role for this user if no admin exists
            if (!_context.UserRoles.Where(x => x.RoleId == admin.Id).Any())
            {
                await CreateUserRoleAsync(new UserRole() { RoleId = admin.Id, UserId = newUser.Id });
            }
            //Get the default role
            Role? defaultRole = await _context.Roles.Where(x => x.Name == "User").FirstOrDefaultAsync() ?? throw new Exception("OnboardUser: No Default Role!");
            //Add default Role
            UserRole newUserRole = new() { RoleId = defaultRole.Id, UserId = newUser.Id };
            await CreateUserRoleAsync(newUserRole);
            return newUser;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            newUser.CreatedOn = DateTime.Now;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }
        public async Task UpdateUserAsync(int id, User updatedUser)
        {
            //if (string.IsNullOrEmpty(id)) throw new Exception("UpdateUserAsync : Id is null");
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync(true);
        }

        public async Task RemoveUserAsync(int id)
        {
            //Cascade delete 
            var rolesToDelete = _context.UserRoles.Where(x => x.UserId == id).ToList();
            _context.UserRoles.RemoveRange(rolesToDelete);
            var projectsToDelete = _context.UserProjects.Where(x => x.UserId == id).ToList();
            _context.UserProjects.RemoveRange(projectsToDelete);
            User user = await _context.Users.Where(x => x.Id == id).FirstAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(true);

        }
        #endregion
        #region roles
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.Where(_ => true).ToListAsync();
        }
        public async Task<Role> GetRoleAsync(int Id)
        {
            return await _context.Roles.Where(x => x.Id == Id).FirstAsync();
        }

        //public async Task CreateRoleAsync(Role newRole)
        //{
        //    _context.Roles.Add(newRole);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task UpdateRoleAsync(int Id, Role updatedRole)
        //{
        //    await _context.Roles.ReplaceOneAsync(x => x.Id == id, updatedRole);
        //}

        //public async Task RemoveRoleAsync(int Id)
        //{
        //    await _context.Roles.DeleteOneAsync(x => x.Id == id);
        //}
        #endregion
        #region userrole
        public async Task<List<Role>> GetUsersRolesAsync(int userId)
        {
            List<Role> roles = new();
            var links = await _context.UserRoles.Where(r => r.UserId == userId).ToListAsync();
            foreach (var link in links)
            {
                var role = await GetRoleAsync(link.RoleId);
                if (role == null) continue;
                if (roles.Where(r => r.Id == link.RoleId).Any()) continue;
                roles.Add(await GetRoleAsync(link.RoleId));
            }
            return roles;
        }

        public async Task<UserRole> GetUserRoleAsync(int Id)
        {
            return await _context.UserRoles.Where(x => x.Id == Id).FirstAsync();
        }

        public async Task CreateUserRoleAsync(UserRole newUserRole)
        {
            _context.UserRoles.Add(newUserRole);
            await _context.SaveChangesAsync();
        }
        //public async Task UpdateUserRoleAsync(int Id, UserRole updatedUserRole)
        //{
        //    _context.UserRoles.Update(updatedUserRole);
        //    await _context.SaveChangesAsync();
        //}

        public async Task RemoveUserRoleAsync(int roleId, int userId)
        {
            var userRole = _context.UserRoles.Where(x => x.RoleId == roleId && x.UserId == userId).First();
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync(true);
        }
        public async Task RemoveUserRoleAsync(int Id)
        {
            var userRole = _context.UserRoles.Where(x => x.Id == Id).First();
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync(true);
        }
        #endregion
        #region userproject
        public async Task<List<UserProject>> GetUserProjectsAsync()
        {
            return await _context.UserProjects.Where(_ => true).ToListAsync();
        }
        public async Task<UserProject> GetUserProjectAsync(int id)
        {
            return await _context.UserProjects.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<List<UserProject>> GetUserProjectsAsync(int userId)
        {
            return await _context.UserProjects.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<List<UserProjectView>> GetUserProjectViewAsync(int userId)
        {
            //Ensure cache exists
            _sd.Projects ??= await _context.Projects.ToListAsync();
            _sd.Clients ??= await _context.Clients.ToListAsync();
            //Join User frienly column values
            var result = (from p in _sd.Projects
                          join c in _sd.Clients on p.ClientId equals c.Id
                          select new UserProjectView
                          {
                              ProjectId = p.Id,
                              ProjectName = p.Name,
                              ClientName = c.Name,
                          }).ToList();
            //Append UserProject data
            List<UserProject> userprojects = await GetUserProjectsAsync(userId);
            foreach (UserProjectView upv in result)
            {
                var activeLink = userprojects.Where(u => u.ProjectId == upv.ProjectId);
                if (activeLink.Any())
                {
                    upv.IsProjectManager = activeLink.First().IsProjectManager;
                    upv.IsProjectMember = !upv.IsProjectManager;
                }
            }
            return result;
        }

        public async Task SetUserProjectsAsync(List<UserProjectView> selection, int userId)
        {

            var userprojects = await GetUserProjectsAsync(userId);
            //Remove unselected entries
            selection.RemoveAll(x => !x.IsProjectManager && !x.IsProjectMember);
            //Delete entries that no loger exist in selection
            foreach (var oldLink in userprojects)
            {
                if (!selection.Where(x => x.ProjectId == oldLink.ProjectId).Any())
                {
                    await RemoveUserProjectAsync(oldLink.ProjectId, userId);
                }
            }
            foreach (UserProjectView selected in selection.Where(x => x.IsProjectManager || x.IsProjectMember))
            {
                if (userprojects.Where(x => x.ProjectId == selected.ProjectId).Any())
                {
                    //Existing link - update
                    var updateLink = userprojects.Where(x => x.ProjectId == selected.ProjectId && x.UserId == userId).First();
                    updateLink.IsProjectManager = selected.IsProjectManager;
                    await UpdateUserProjectAsync(updateLink.Id, updateLink);
                }
                else
                {
                    //New link - Create new entry
                    UserProject newLink = new()
                    {
                        ProjectId = selected.ProjectId,
                        UserId = userId,
                        IsProjectManager = selected.IsProjectManager
                    };
                    await CreateUserProjectAsync(newLink);
                }

            }

        }
        public async Task<List<Project>> GetUsersProjectsAsync(int userId)
        {
            List<Project> projects = new();
            var links = await _context.UserProjects.Where(x => x.UserId == userId).ToListAsync();
            foreach (var link in links)
            {
                projects.Add(_sd.Projects.First(p => p.Id == link.ProjectId));
            }
            return projects;
        }

        public async Task CreateUserProjectAsync(UserProject newUserProject)
        {
            _context.UserProjects.Add(newUserProject);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserProjectAsync(int Id, UserProject updatedUserProject)
        {
            _context.UserProjects.Update(updatedUserProject);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserProjectAsync(int Id)
        {
            var userProject = await _context.UserProjects.Where(x => x.Id == Id).FirstAsync();
            _context.UserProjects.Remove(userProject);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserProjectAsync(int projectId, int userId)
        {
            var userProject = await _context.UserProjects.Where(x => x.ProjectId == projectId && x.UserId == userId).FirstAsync();
            _context.UserProjects.Remove(userProject);
            await _context.SaveChangesAsync(true);
        }
        #endregion
    }
}
