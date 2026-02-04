using Improver.BusinessServices;
using Improver.BusinessserviceInterface;
using Microsoft.AspNetCore.Mvc;
using Improver.BusinessModel;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Improver.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Azure;
using Microsoft.Extensions.ObjectPool;

namespace Improver.BusinessServices
{
    public class UserService : IUserService
    {
        public readonly AppDbContext _dbContext;

        private readonly IConfiguration _configuration;

        public UserService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _dbContext = appDbContext;
            _configuration = configuration;
        }
        public async Task<UserBOResponse> SignUPUser(UserBORequest objBORequest)
        {
            UserBOResponse newuser = new UserBOResponse();
            try
            {
                UserValidator validator = new UserValidator();
                string validationMessage = validator.UserSignupchecker(objBORequest);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    newuser.IsSuccess = false;
                    newuser.Message = validationMessage;
                    //throw new ArgumentException(validationMessage);
                    return newuser;
                }

                var userObj = UserBORequest.UserCreate(objBORequest);

                //check email alredy exist in db 
                var alredyUser = _dbContext.Users.Where(u=> u.Email == userObj.Email && u.Isdeleted==false).FirstOrDefault();

                if (alredyUser!=null)
                {
                    newuser.IsSuccess = false;
                    newuser.Message = $"User with the same email already exists: {userObj.Email}";
                    //  throw new Exception($"user with same Email alredy exist:{userObj.Email}");
                    return newuser;
                }

                _dbContext.Users.Add(userObj);
                await _dbContext.SaveChangesAsync();

                newuser = UserBOResponse.UserCreate(userObj);

                //create jwt token 
                if (newuser != null && newuser.Userid != null)
                {
                    //var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));

                    //var tokenHandler = new JwtSecurityTokenHandler();

                    //var tokenDescriptor = new SecurityTokenDescriptor()
                    //{
                    //    //add claims
                    //    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    //    {
                    //        new Claim(ClaimTypes.NameIdentifier, newuser.Userid.ToString()),
                    //          new Claim(ClaimTypes.Email, newuser.Email)
                    //    }),
                    //      Expires = DateTime.Now.AddHours(8),
                    //    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    //};
                    //  var token = tokenHandler.CreateToken(tokenDescriptor);
                    //newuser.token = tokenHandler.WriteToken(token);
                    newuser.token = await this.TokenGenerator(newuser.Userid);

                }
                newuser.IsSuccess = true;
                return newuser;

            }
            catch (Exception ex)
            {
                newuser.IsSuccess = false;
                newuser.Message = $"user creation faliled : {ex.Message}";

                return newuser;
            }


        }

        public async Task<string> TokenGenerator(int userId)
        {

            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecret"));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                //add claims
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                }),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }

        public async Task<UserBOResponse> Login(UserBORequest objBoRequest)
        {
            UserBOResponse LoginUserData = new UserBOResponse();
            try
            {
             
                UserValidator validator = new UserValidator();
                string validationMessage = validator.UserLoginchecker(objBoRequest);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    //throw new ArgumentException(validationMessage);
                    LoginUserData.IsSuccess = false;
                    LoginUserData.Message = validationMessage;
                    return LoginUserData;
                }
                var userObj = UserBORequest.UserCreate(objBoRequest);
                //check if user present with email
                var userData = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userObj.Email && u.Isdeleted == false);
                if (userData == null)
                {
                    LoginUserData.IsSuccess = false;
                    LoginUserData.Message = "Invalid  user Login";
                    return LoginUserData;
                }
                else
                {
                    bool isvalidPassword = BCrypt.Net.BCrypt.Verify(objBoRequest.Password,userData.Password);

                    if (!isvalidPassword)
                    {
                        LoginUserData.IsSuccess = false;
                        LoginUserData.Message = "Invalid Password";
                        return LoginUserData;
                    }
                    LoginUserData = UserBOResponse.UserCreate(userData);

                    LoginUserData.token = await this.TokenGenerator(userData.Userid);

                }
                LoginUserData.IsSuccess = true;
                
                return LoginUserData;



            }
            catch (Exception ex)
            {
                LoginUserData.IsSuccess = false;
                LoginUserData.Message = $"User Login failed: {ex.Message}";
                return LoginUserData;

            }
        }
    }

}
