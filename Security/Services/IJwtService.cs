using Domain.Entities;

namespace Security.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
