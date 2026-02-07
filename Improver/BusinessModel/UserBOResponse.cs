using Improver.Models;

namespace Improver.BusinessModel
{
    public class UserBOResponse :BaseResponse
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

        public string token { get; set; }

        public static UserBOResponse UserCreate(User objUser)
        {
            UserBOResponse objUserBORes = new UserBOResponse();

            objUserBORes.Userid = objUser.Userid.Value;
            objUserBORes.Firstname = objUser.Firstname;
            objUserBORes.Lastname = objUser.Lastname;
            objUserBORes.Email = objUser.Email;
            objUserBORes.Password = objUser.Password;
            objUserBORes.Createddate = objUser.Createddate.Value;
            objUserBORes.Isdeleted = objUser.Isdeleted;
            objUserBORes.Lastmodifieddate = objUser.Lastmodifieddate.Value;
            objUserBORes.UserimageUrl = objUser.UserimageUrl;

            return objUserBORes;
        }
    }
}
