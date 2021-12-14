using EFCore.DbConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_EF.Migrations
{
    public static class MigrationExtention
    {
        public static void Migrate(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<UserContext>();

                context.Database.Migrate();
            }
        }
    }
}
