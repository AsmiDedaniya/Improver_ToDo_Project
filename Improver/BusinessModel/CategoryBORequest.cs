using Improver.Models;

namespace Improver.BusinessModel
{
    public class CategoryBORequest
    {
        public int? Categoryid { get; set; }

        public string Categoryname { get; set; }

        public bool? Isdeleted { get; set; }

        public int Userid { get; set; }

        public DateTime? Lastmodifieddate { get; set; }

        public static Category CategoryCreate(CategoryBORequest objBoRequest)
        {
            Category objCategory = new Category();

            //objUser.Userid = objBoRequest.Userid;
            objCategory.Categoryid = objBoRequest.Categoryid.HasValue ? objBoRequest.Categoryid.Value : 0 ;
            objCategory.Categoryname = objBoRequest.Categoryname;
            objCategory.Createddate = DateTime.Now;
            objCategory.Isdeleted = objBoRequest.Isdeleted.HasValue ? objBoRequest.Isdeleted.Value : false;
            objCategory.Lastmodifieddate = objBoRequest.Lastmodifieddate.HasValue ? objBoRequest.Lastmodifieddate.Value : DateTime.Now;
            objCategory.Userid = objBoRequest.Userid;

            return objCategory;
        }

    }
}
