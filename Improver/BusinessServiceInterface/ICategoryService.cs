using Improver.BusinessModel;

namespace Improver.BusinessServiceInterface
{
    public interface ICategoryService
    {
        Task<CategoryBOResponse> Create(CategoryBORequest objBoRequest);

        Task<CategoryBOResponse> Update(CategoryBORequest objBoRequest);

        Task<CategoryBOResponse> GetById(int CategoryId,int UserId);

        Task<CategoryListBOResponse> GetList(int UserId);

        Task<CategoryBOResponse> Delete(int CategoryId, int UserId);
    }
}
