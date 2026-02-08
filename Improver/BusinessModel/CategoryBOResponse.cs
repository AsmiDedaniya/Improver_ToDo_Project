using Improver.Models;

namespace Improver.BusinessModel
{
    public class CategoryBOResponse : BaseResponse
    {
        public int Categoryid { get; set; }

        public string Categoryname { get; set; }

        public int Userid { get; set; }

        public bool? Isdeleted { get; set; }

        public DateTime Createddate { get; set; }

        public DateTime Lastmodifieddate { get; set; }


        public static CategoryBOResponse CategoryCreate(Category objCategory)
        {
            CategoryBOResponse objCategoryBORes = new CategoryBOResponse();

            objCategoryBORes.Categoryid = objCategory.Categoryid;
            objCategoryBORes.Categoryname = objCategory.Categoryname;
            objCategoryBORes.Createddate = objCategory.Createddate;
            objCategoryBORes.Isdeleted = objCategory.Isdeleted;
            objCategoryBORes.Lastmodifieddate = objCategory.Lastmodifieddate;
            objCategoryBORes.Userid = objCategory.Userid;

            return objCategoryBORes;
        }
    }
}
