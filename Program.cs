using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Klient", "Trener" };

    foreach (var roleName in roleNames)
    {
        bool roleExists = await roleManager.RoleExistsAsync(roleName);

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    string trainerEmail = "trener@fitnessapp.pl";
    string trainerPassword = "Trener123!";

    var trainerUser = await userManager.FindByEmailAsync(trainerEmail);

    if (trainerUser == null)
    {
        trainerUser = new IdentityUser
        {
            UserName = trainerEmail,
            Email = trainerEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(trainerUser, trainerPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(trainerUser, "Trener");
        }
    }
    else
    {
        if (!await userManager.IsInRoleAsync(trainerUser, "Trener"))
        {
            await userManager.AddToRoleAsync(trainerUser, "Trener");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();