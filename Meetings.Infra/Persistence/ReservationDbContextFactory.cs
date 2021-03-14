using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReservationService.Infra.Persistence
{
    class ReservationDbContextFactory: IDesignTimeDbContextFactory<ReservationDbContext>
    {
        public ReservationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReservationDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ReservationDb;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new ReservationDbContext(optionsBuilder.Options);
        }
    }
}

