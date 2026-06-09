using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Security;

namespace WebApplication1.Data;

public static class IdentitySeedData
{
    public static async Task EnsureRolesAndClaimsAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await EnsureRoleAsync(roleManager, AppRoles.Klient);
        await EnsureRoleAsync(
            roleManager,
            AppRoles.Trener,
            AppPermissions.ManageGymStructure,
            AppPermissions.AssignUserPlans,
            AppPermissions.ManageFitnessClasses,
            AppPermissions.ManageStore);
    }

    private static async Task EnsureRoleAsync(
        RoleManager<IdentityRole> roleManager,
        string roleName,
        params string[] permissions)
    {
        var role = await roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            role = new IdentityRole(roleName);
            var createResult = await roleManager.CreateAsync(role);

            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Nie udalo sie utworzyc roli {roleName}: {string.Join(", ", createResult.Errors.Select(error => error.Description))}");
            }
        }

        var existingClaims = await roleManager.GetClaimsAsync(role);

        foreach (var permission in permissions)
        {
            if (existingClaims.Any(claim =>
                    claim.Type == AppClaimTypes.Permission &&
                    claim.Value == permission))
            {
                continue;
            }

            var claimResult = await roleManager.AddClaimAsync(
                role,
                new Claim(AppClaimTypes.Permission, permission));

            if (!claimResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Nie udalo sie dodac uprawnienia {permission} do roli {roleName}: {string.Join(", ", claimResult.Errors.Select(error => error.Description))}");
            }
        }
    }
}
