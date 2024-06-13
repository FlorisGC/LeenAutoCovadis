using LeenAutoCovadis.shared.Constants;
using LeenAutoCovadis.shared.Interfaces;

namespace LeenAutoCovadis.Api.Data;

public class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    public ICurrentUserContext.CurrentUser User => GetCurrentUser();

    public bool IsAuthenticated => httpContextAccessor.HttpContext!.User.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string roleName)
    {
        return User.Roles.Contains(roleName);
    }

    private ICurrentUserContext.CurrentUser GetCurrentUser()
    {
        var claims = httpContextAccessor.HttpContext!.User;

        var id = claims.FindFirst(Claims.Id)!.Value;
        var name = claims.FindFirst(Claims.Name)!.Value;
        var email = claims.FindFirst(Claims.Email)!.Value;
        var roles = claims.FindAll(Claims.Role).Select(r => r.Value).ToList();

        return new ICurrentUserContext.CurrentUser(int.Parse(id), name, email, roles);
    }
}