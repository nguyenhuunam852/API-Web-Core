using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface IAuthorizeJwt
    {
        string generateJwtToken(UserModel user);
    }
}
