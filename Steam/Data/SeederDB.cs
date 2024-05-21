using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Steam.Constants;
using Steam.Data.Entities.Identity;


namespace Steam.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;

                //Отримую посилання на наш контекст
                var context = service.GetRequiredService<AppEFContext>();
                context.Database.Migrate();

            }


        }
    }
}
