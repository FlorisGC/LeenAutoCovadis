
using LeenAutoCovadis.Api.Data;
using LeenAutoCovadis.Api.Services;
using LeenAutoCovadis.shared.Requests;
using LeenAutoCovadis.shared.Responses;

namespace LeenAutoCovadis.Api.Services;

public class AuthService(CovadisContext dbContext, TokenService tokenService)
{
    public AuthResponse? Login(LoginRequest request)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null || user.Password != request.Password)
        {
            return null;
        }

        return new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = tokenService.CreateToken(user),
        };
    }
}
