using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Compensation.Api
{
    public class CompensationDbContextFactory : IDesignTimeDbContextFactory<CompensationDbContext>
    {
        public CompensationDbContext CreateDbContext(string[] args)
        {
            var optionBuider = new DbContextOptionsBuilder<CompensationDbContext>();

            var connStr = ConfigurationHelper.GetCurrentSettings("ConnectionStrings:DefaultConnection");

            optionBuider.UseSqlServer(connStr);

            return new CompensationDbContext(optionBuider.Options);


        }
    }
}
