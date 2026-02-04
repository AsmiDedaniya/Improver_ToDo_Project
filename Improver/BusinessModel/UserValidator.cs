using Improver.BusinessModel;

namespace Improver.BusinessModel
{
    public class UserValidator
    {
        public string UserSignupchecker(UserBORequest objBoRequest)
        {
            string validationmsg = "";

            if (objBoRequest == null)
            {
                return "User request object is null";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Email))
            {
                validationmsg += "Email is required. ";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Firstname))
            {
                validationmsg += "First name is required. ";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Lastname))
            {
                validationmsg += "Last name is required. ";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Password))
            {
                validationmsg += "Password is required. ";
            }

            return validationmsg;
        }

        public string UserLoginchecker(UserBORequest objBoRequest)
        {
            string validationmsg = "";

            if (objBoRequest == null)
            {
                return "User request object is null";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Email))
            {
                validationmsg += "Email is required. ";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Password))
            {
                validationmsg += "Password is required. ";
            }

            return validationmsg;
        }
    }
}
