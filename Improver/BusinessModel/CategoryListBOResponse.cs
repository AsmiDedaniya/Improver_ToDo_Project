using Improver.BusinessModel;

public class CategoryListBOResponse : BaseResponse
{
    public List<CategoryBOResponse> Categories { get; set; }
}
