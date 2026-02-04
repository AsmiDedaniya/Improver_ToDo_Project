using CollegeApp.Models;
using Improver.BusinessModel;
using Improver.BusinessserviceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;

namespace Improver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        private APIResponse _apiResponse;
        public UserController(IUserService userService)
        {
            _userService = userService;
            _apiResponse = new();
        }

        [HttpPost]
        [Route("SignUp")]

        public async Task<ActionResult<APIResponse>> SignUP([FromBody] UserBORequest objBoRequest)
        {
            var newUser = await _userService.SignUPUser(objBoRequest);
            _apiResponse.data = newUser;
           if(newUser.IsSuccess == false)
            {
                _apiResponse.Staus = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden ;
            }
            else
            {
                _apiResponse.Staus = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
            }
                
            return Ok(_apiResponse);
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] UserBORequest objBoRequest)
        {
            var LoginUser = await _userService.Login(objBoRequest);
            _apiResponse.data = LoginUser;
            if (LoginUser.IsSuccess == false)
            {
                _apiResponse.Staus = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
            }
            else
            {
                _apiResponse.Staus = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
            }

            return Ok(_apiResponse);
        }
    }
}
