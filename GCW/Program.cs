using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GCW.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection"); ;

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString)); ;

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>(); ;

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
        Console.WriteLine("cREATED USERS");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

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
app.UseAuthentication(); ;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();