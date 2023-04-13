using Clean.Architecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.Infrastructure;

public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) =>
      services.AddDbContext<PersonDbContext>(options =>
          //options.UseSqlite(connectionString)); // will be created in web project root
          options.UseSqlServer(connectionString)); // will be created in web project root
}
