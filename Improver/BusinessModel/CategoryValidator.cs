namespace Improver.BusinessModel
{
    public class CategoryValidator
    {
        public string Categorychecker(CategoryBORequest objBoRequest)
        {
            string validationmsg = "";

            if (objBoRequest == null)
            {
                return "Category request object is null";
            }

            if (string.IsNullOrWhiteSpace(objBoRequest.Categoryname))
            {
                validationmsg += "CategoryName is required. ";
            }
            

            return validationmsg;
        }
    }
}
