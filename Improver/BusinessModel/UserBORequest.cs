using Improver.Models;

namespace Improver.BusinessModel
{
    public class UserBORequest
    {
        public int Userid { get; set; }

        public string Firstname { get; set; } 

        public string Lastname { get; set; } 

        public string Password { get; set; } 

        public string Email { get; set; } 

        public DateTime Createddate { get; set; }

        public DateTime Lastmodifieddate { get; set; }

        public string? UserimageUrl { get; set; }

        public bool? Isdeleted { get; set; }

        public static User UserCreate(UserBORequest objBoRequest)
        {
            User objUser = new User();

            //objUser.Userid = objBoRequest.Userid;
            objUser.Firstname = objBoRequest.Firstname;
            objUser.Lastname = objBoRequest.Lastname;
            objUser.Email = objBoRequest.Email;
            objUser.Password = BCrypt.Net.BCrypt.HashPassword(objBoRequest.Password);
            objUser.Createddate = DateTime.Now;
            objUser.Isdeleted = objBoRequest.Isdeleted;
            objUser.Lastmodifieddate = DateTime.Now;
            objUser.UserimageUrl = objBoRequest.UserimageUrl;

            return objUser;
        }
    }
}
