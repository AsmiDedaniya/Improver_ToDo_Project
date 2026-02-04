using Improver.BusinessModel;
using Microsoft.AspNetCore.Mvc;

namespace Improver.BusinessserviceInterface
{
    public interface IUserService
    {
        Task<UserBOResponse> SignUPUser(UserBORequest objBoRequest);

        Task<string> TokenGenerator(int userId);

        Task<UserBOResponse> Login(UserBORequest objBoRequest);
    }
}
