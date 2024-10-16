using Microsoft.EntityFrameworkCore;
using Repositories;

namespace e_commerce.Infastructe.Extensions
{
    public static class ApplicationExtansion
    {
        public static void ConfigureAndCheckMigrations(this IApplicationBuilder app)
        {
            DataContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DataContext>();
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR")
                .AddSupportedCultures("tr-TR")
                .SetDefaultCulture("tr-TR");

                options.AddSupportedCultures("en-US")
                .AddSupportedCultures("en-US")
                .SetDefaultCulture("en-US");

                options.AddSupportedCultures("en-GB")
                .AddSupportedCultures("en-GB")
                .SetDefaultCulture("en-GB");
            });
        }
    }
}
