using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PayrollSystem.Data.Common;

namespace PayrollSystem.Data
{
    public class ContextFactory:IDesignTimeDbContextFactory<DbsContext>
    {
        public DbsContext CreateDbContext(string []args) 
        {
            IConfigurationRoot configuration=new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())  // 
                .AddJsonFile("appsettings.json")  // Load main appsettings file
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true).Build();
            var connectionString = configuration.GetConnectionString("ConnectionLink");
            var optionsBuilder = new DbContextOptionsBuilder<DbsContext>();
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("PayrollSystem"));
            return new DbsContext(optionsBuilder.Options);
        }
    }
}
