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
         
           if(newUser.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden ;
                _apiResponse.Errors.Add(newUser.Message);

                return Unauthorized(_apiResponse);
            }
            
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newUser;

            return Ok(_apiResponse);
            
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] UserBORequest objBoRequest)
        {
            var LoginUser = await _userService.Login(objBoRequest);
            
            if (LoginUser.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(LoginUser.Message);

                return Unauthorized(_apiResponse);
            }
           
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = LoginUser;

            return Ok(_apiResponse);
        }
    }
}
