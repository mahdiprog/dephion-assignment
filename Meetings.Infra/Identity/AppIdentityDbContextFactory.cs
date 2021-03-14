using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Meetings.Infra.Identity
{
    class AppIdentityDbContextFactory: IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ReservationDb;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new AppIdentityDbContext(optionsBuilder.Options);
        }
    }
}

