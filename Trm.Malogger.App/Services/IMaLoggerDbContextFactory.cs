using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trm.MaLogger.MsData.Context;

namespace Trm.MaLogger.App.Services
{
    public class IMaLoggerDbContextFactory
    {
        public MaLoggerDbContext dbContext;
        public IMaLoggerDbContextFactory(DbContextOptions<MaLoggerDbContext> options)
        {
            dbContext = new MaLoggerDbContext(options);
        }
    }
}
