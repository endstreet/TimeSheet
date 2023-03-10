using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trm.MaLogger.MsData.Context;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Services
{
    /// <summary>
    /// CRUD service for all db entities
    /// </summary>
    public class MaLoggerService
    {
        MaLoggerDbContext _context;
        public MaLoggerService(MaLoggerDbContext context)
        {
            _context = context;

        }
        //#region clients
        //public async Task<List<Client>> GetClientsAsync()
        //{
        //    return await _context.clients.Find(_ => true).ToListAsync();
        //}
        //public async Task<Client> GetClientAsync(string id)
        //{
        //    return await _context.clients.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task CreateClientAsync(Client newClient)
        //{
        //    await _context.clients.InsertOneAsync(newClient);
        //}
        //public async Task UpdateClientAsync(string id, Client updatedClient)
        //{
        //    await _context.clients.ReplaceOneAsync(x => x.Id == id, updatedClient);
        //}

        //public async Task RemoveClientAsync(string id)
        //{
        //    await _context.clients.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
        //#region projects
        //public async Task<List<Project>> GetProjectsAsync()
        //{
        //    return await _context.projects.Find(_ => true).ToListAsync();
        //}
        //public async Task<Project> GetProjectAsync(string id)
        //{
        //    return await _context.projects.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task CreateProjectAsync(Project newProject)
        //{
        //    await _context.projects.InsertOneAsync(newProject);
        //}
        //public async Task UpdateProjectAsync(string id, Project updatedProject)
        //{
        //    await _context.projects.ReplaceOneAsync(x => x.Id == id, updatedProject);
        //}

        //public async Task RemoveProjectAsync(string id)
        //{
        //    await _context.projects.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
        //#region roles
        //public async Task<List<Role>> GetRolesAsync()
        //{
        //    return await _context.Roles.ToListAsync();
        //}
        //public async Task<Role> GetRoleAsync(int id)
        //{
        //    return await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync()??new Role();
        //}

        //public async Task CreateRoleAsync(Role newRole)
        //{
        //    await _context.Roles.InsertOneAsync(newRole);
        //}
        //public async Task UpdateRoleAsync(string id, Role updatedRole)
        //{
        //    await _context.Roles.ReplaceOneAsync(x => x.Id == id, updatedRole);
        //}

        //public async Task RemoveRoleAsync(string id)
        //{
        //    await _context.Roles.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
        //#region users
        //public async Task<List<User>> GetUsersAsync()
        //{
        //    return await _context.users.Find(_ => true).ToListAsync();
        //}
        //public async Task<User> GetUserAsync(string id)
        //{
        //    return await _context.users.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task CreateUserAsync(User newUser)
        //{
        //    await _context.users.InsertOneAsync(newUser);
        //}
        //public async Task UpdateUserAsync(string id, User updatedUser)
        //{
        //    await _context.users.ReplaceOneAsync(x => x.Id == id, updatedUser);
        //}

        //public async Task RemoveUserAsync(string id)
        //{
        //    await _context.users.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
        //#region userrole
        //public async Task<List<UserRole>> GetUserRolesAsync()
        //{
        //    return await _context.userRoles.Find(_ => true).ToListAsync();
        //}
        //public async Task<UserRole> GetUserRoleAsync(string id)
        //{
        //    return await _context.userRoles.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task CreateUserRoleAsync(UserRole newUserRole)
        //{
        //    await _context.userRoles.InsertOneAsync(newUserRole);
        //}
        //public async Task UpdateUserRoleAsync(string id, UserRole updatedUserRole)
        //{
        //    await _context.userRoles.ReplaceOneAsync(x => x.Id == id, updatedUserRole);
        //}

        //public async Task RemoveUserRoleAsync(string id)
        //{
        //    await _context.userRoles.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
        //#region userproject
        //public async Task<List<UserProject>> GetUserProjectsAsync()
        //{
        //    return await _context.userProjects.Find(_ => true).ToListAsync();
        //}
        //public async Task<UserProject> GetUserProjectAsync(string id)
        //{
        //    return await _context.userProjects.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task CreateUserProjectAsync(UserProject newUserProject)
        //{
        //    await _context.userProjects.InsertOneAsync(newUserProject);
        //}
        //public async Task UpdateUserProjectAsync(string id, UserProject updatedUserProject)
        //{
        //    await _context.userProjects.ReplaceOneAsync(x => x.Id == id, updatedUserProject);
        //}

        //public async Task RemoveUserProjectAsync(string id)
        //{
        //    await _context.userProjects.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
        //#region timeentry
        //public async Task<List<TimeEntry>> GetTimeEntrysAsync()
        //{
        //    return await _context.timeEntrys.Find(_ => true).ToListAsync();
        //}
        //public async Task<TimeEntry> GetTimeEntryAsync(string id)
        //{
        //    return await _context.timeEntrys.Find(x => x.Id == id).FirstOrDefaultAsync();
        //}
        //public async Task CreateTimeEntryAsync(TimeEntry newTimeEntry)
        //{
        //    await _context.timeEntrys.InsertOneAsync(newTimeEntry);
        //}
        //public async Task UpdateTimeEntryAsync(string id, TimeEntry updatedTimeEntry)
        //{
        //    await _context.timeEntrys.ReplaceOneAsync(x => x.Id == id, updatedTimeEntry);
        //}
        //public async Task RemoveTimeEntryAsync(string id)
        //{
        //    await _context.timeEntrys.DeleteOneAsync(x => x.Id == id);
        //}
        //#endregion
    }
}