using LeenAutoCovadis.shared.Constants;
using LeenAutoCovadis.shared.Interfaces;

namespace LeenAutoCovadis.blazor.Services;

public class CurrentUserContext : ICurrentUserContext
{
    public ICurrentUserContext.CurrentUser User { get; set; }

    public bool IsAuthenticated { get; set; }

    public CurrentUserContext(LeenAutoCovadisAuthenticationStateProvider authenticationStateProvider)
    {
        authenticationStateProvider.AuthenticationStateChanged += (state) =>
        {
            var authState = state.Result;
            IsAuthenticated = authState.User.Identity?.IsAuthenticated ?? false;
            if (IsAuthenticated == true)
            {
                var claims = authState.User;
                var id = claims.FindFirst(Claims.Id)!.Value;
                var name = claims.FindFirst(Claims.Name)!.Value;
                var email = claims.FindFirst(Claims.Email)!.Value;
                var roles = claims.FindAll(Claims.Role).Select(r => r.Value).ToList();
                User = new ICurrentUserContext.CurrentUser(int.Parse(id), name, email, roles);
            }
        };
    }

    public bool IsInRole(string roleName)
    {
        return User.Roles.Contains(roleName);
    }
}
