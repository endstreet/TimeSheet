using MongoDB.Driver;
using Trm.MaLogger.Data.Models;

namespace Trm.MaLogger.Data.Context
{
    public class MongoContext
    {
        public IMongoCollection<TimeEntry> timeEntrys;
        public IMongoCollection<Project> projects;
        public IMongoCollection<Client> clients;
        public IMongoCollection<Role> roles;
        public IMongoCollection<User> users;
        public IMongoCollection<UserRole> userRoles;
        public IMongoCollection<UserProject> userProjects;
        public IMongoCollection<Report> reports;

        public MongoContext(IMongoDatabase db)
        {
            timeEntrys = db.GetCollection<TimeEntry>("TimeEntry");
            projects = db.GetCollection<Project>("Project");
            clients = db.GetCollection<Client>("Client");
            roles = db.GetCollection<Role>("Role");
            users = db.GetCollection<User>("User");
            userRoles = db.GetCollection<UserRole>("UserRole");
            userProjects = db.GetCollection<UserProject>("UserProject");
            reports = db.GetCollection<Report>("Report");
        }

    }
}
