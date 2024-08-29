using Microsoft.AspNetCore.Authorization;

namespace CleanTableTennisApp.WebUI.Permission;

public class HasPermissionsHandler : AuthorizationHandler<PermissionDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionDto permissionDto)
    {
        if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == permissionDto.Issuer))
            return Task.CompletedTask;

        var permissions = context.User.Claims
            .Where(s => s.Type == "permissions" && s.Issuer == permissionDto.Issuer)
            .Select(s => s.Value)
            .ToList();

        if (permissions.Any(s => s.Contains(permissionDto.Permission)) || IsAdminPermission(permissions))
        {
            context.Succeed(permissionDto);
        }

        return Task.CompletedTask;
    }

    private static bool IsAdminPermission(List<string> permissions) => permissions.Any(s => s.Contains(Permissions.All.Admin));
}