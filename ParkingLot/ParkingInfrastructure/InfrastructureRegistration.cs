using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ParkingInfrastructure;

public static class InfrastructureRegistration
{
    public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ParkingDbContext>((sp, bld) =>
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            bld.UseMySql(connectionString, serverVersion);
        });
    }
}