using CarWorkshop.Infrastructure.Extension;

using CarWorkshop.Application.Extensions;
using CarWorkshop.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            builder.Services.AddInfastructure(builder.Configuration);
            builder.Services.AddApplication();

            var app = builder.Build();

            var scoped = app.Services.CreateScope();

            var seeder = scoped.ServiceProvider.GetRequiredService<CarWorkshopSeeder>();

            await seeder.Seed();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}