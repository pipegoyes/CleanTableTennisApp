using Microsoft.AspNetCore.Authorization;

namespace CleanTableTennisApp.WebUI.Permission;

public class PermissionDto : IAuthorizationRequirement
{
    public string Issuer { get; }
    public string Permission { get; }

    public PermissionDto(string permission, string issuer)
    {
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
    }
}