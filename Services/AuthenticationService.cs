using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using TaskRecordingSystem.Models;

namespace TaskRecordingSystem.Services
{
    public class AuthenticationService
    {
        private readonly ProtectedLocalStorage _localStorage;
        public AppUser? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public AuthenticationService(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SetUserAsync(AppUser user, bool remember)
        {
            CurrentUser = user;
            if (remember)
            {
                await _localStorage.SetAsync("auth_user_id", user.Id);
            }
        }

        public async Task TryAutoLoginAsync(IDbContextFactory<AppDbContext> factory)
        {
            var result = await _localStorage.GetAsync<int>("auth_user_id");
            if (result.Success)
            {
                using var context = factory.CreateDbContext();
                var user = await context.Users.FindAsync(result.Value);
                if (user != null)
                    CurrentUser = user;
            }
        }

        public async Task LogoutAsync()
        {
            CurrentUser = null;
            await _localStorage.DeleteAsync("auth_user_id");
        }
    }
}
