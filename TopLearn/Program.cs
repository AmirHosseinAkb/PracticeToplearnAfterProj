using Microsoft.EntityFrameworkCore;
using TopLearn.Data.Context;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.Services;
using TopLearn.Core.Convertors;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
#region DbContext
    builder.Services.AddDbContext<TopLearnContext>(context =>
        context.UseSqlServer(builder.Configuration.GetConnectionString("TopLearnConnection"))
    );
#endregion
#region IOC

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IViewRenderService,RenderViewToString>();

#endregion
#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(5);
    });

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
