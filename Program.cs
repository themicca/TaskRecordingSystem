using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TaskRecordingSystem.Components;
using TaskRecordingSystem.Services;

namespace TaskRecordingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlite("Data Source=app.db"));

            builder.Services.AddQuickGridEntityFrameworkAdapter();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var keyPath = Path.Combine(AppContext.BaseDirectory, "keys");

            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keyPath))
                .SetApplicationName("TaskRecordingSystem");

            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<ProtectedLocalStorage>();

            builder.Services.AddScoped(sp =>
            {
                var nav = sp.GetRequiredService<NavigationManager>();
                return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseMigrationsEndPoint();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Attachments")),
                RequestPath = "/Attachments"
            });

            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //DataCleaner.Clear(db);
                DataSeeder.Seed(db);
            }

            app.MapControllers();

            app.Run();
        }
    }
}
