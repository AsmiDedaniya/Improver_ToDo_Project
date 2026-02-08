using Azure;
using Improver.BusinessModel;
using Improver.BusinessServiceInterface;
using Improver.Models;
using Microsoft.EntityFrameworkCore;

namespace Improver.BusinessServices
{
    public class CategoryService : ICategoryService
    {
        public readonly AppDbContext _dbContext;


        public CategoryService(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<CategoryBOResponse> Create(CategoryBORequest objBORequest)
        {
            CategoryBOResponse newCategory = new CategoryBOResponse();
            try
            {
                CategoryValidator validator = new CategoryValidator();
                string validationMessage = validator.Categorychecker(objBORequest);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = validationMessage;
                    //throw new ArgumentException(validationMessage);
                    return newCategory;
                }
             

                var categoryObj = CategoryBORequest.CategoryCreate(objBORequest);
                var categoryExist = _dbContext.Categories.Where(c => c.Categoryname == categoryObj.Categoryname && c.Userid == categoryObj.Userid && c.Isdeleted == false).FirstOrDefault();
                if (categoryExist != null)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = $"category with the same name already exists: {categoryObj.Categoryname}";

                    return newCategory;
                }
                _dbContext.Categories.Add(categoryObj);
                await _dbContext.SaveChangesAsync();

                newCategory = CategoryBOResponse.CategoryCreate(categoryObj);
                newCategory.IsSuccess = true;
                return newCategory;

            }
            catch (Exception ex)
            {
                newCategory.IsSuccess = false;
                newCategory.Message = $"user creation faliled : {ex.Message}";

                return newCategory;
            }
            }

        public async Task<CategoryBOResponse> Update(CategoryBORequest objBORequest)
        {
            CategoryBOResponse newCategory = new CategoryBOResponse();
            try
            {
                CategoryValidator validator = new CategoryValidator();
                string validationMessage = validator.Categorychecker(objBORequest);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = validationMessage;
                    return newCategory;
                }

                var categoryObj = CategoryBORequest.CategoryCreate(objBORequest);

                // 🔹 Find existing category (belongs to user & not deleted)
                var existingCategory = await _dbContext.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c =>
                        c.Categoryid == categoryObj.Categoryid &&
                        c.Userid == categoryObj.Userid &&
                        c.Isdeleted == false);

                if (existingCategory == null)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = "Category not found";
                    return newCategory;
                }

                // 🔹 Duplicate name check (exclude current category)
                bool duplicateExists = await _dbContext.Categories.AnyAsync(c =>
                    c.Categoryname == categoryObj.Categoryname &&
                    c.Userid == categoryObj.Userid &&
                    c.Isdeleted == false &&
                    c.Categoryid != categoryObj.Categoryid);

                if (duplicateExists)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = $"Category with the same name already exists: {categoryObj.Categoryname}";
                    return newCategory;
                }

                _dbContext.Categories.Update(categoryObj);
                await _dbContext.SaveChangesAsync();

                newCategory = CategoryBOResponse.CategoryCreate(categoryObj);
                newCategory.IsSuccess = true;
                newCategory.Message = "Category updated successfully";

                return newCategory;
            }
            catch (Exception ex)
            {
                newCategory.IsSuccess = false;
                newCategory.Message = $"Category update failed: {ex.Message}";
                return newCategory;
            }
        }

        public async Task<CategoryBOResponse> GetById(int CategoryId,int UserId)
        {
            CategoryBOResponse newCategory = new CategoryBOResponse();
            try
            {
                if (CategoryId <= 0)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = $"Category with the categoryid does not exists: {CategoryId}";
                    return newCategory;
                }

                var categoryData = _dbContext.Categories.Where(c => c.Categoryid == CategoryId && c.Isdeleted == false && c.Userid == UserId).FirstOrDefault();
                if(categoryData == null)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = $"Category with the categoryid does not exists: {CategoryId}";
                    return newCategory;
                }


                newCategory = CategoryBOResponse.CategoryCreate(categoryData);
                newCategory.IsSuccess = true;
                newCategory.Message = "Category Fetch successfully";

                return newCategory;
            }
            catch (Exception ex)
            {
                newCategory.IsSuccess = false;
                newCategory.Message = $"Category fetch failed: {ex.Message}";
                return newCategory;
            }
        }

        public async Task<CategoryListBOResponse> GetList( int UserId)
        {
            CategoryListBOResponse newCategory = new CategoryListBOResponse();
            try
            {
              

                var categoryData = await _dbContext.Categories.Where(c => c.Isdeleted == false && c.Userid == UserId).ToListAsync();

                if (categoryData == null)
                {
                    newCategory.IsSuccess = true;
                    newCategory.Message = "Create your New Categories";
                }

                newCategory.Categories = new List<CategoryBOResponse>();
                foreach (var category in categoryData)
                {
                    // Convert Category entity to CategoryBOResponse
                    CategoryBOResponse categoryResponse =
                        CategoryBOResponse.CategoryCreate(category);

                    // Add converted object to list
                    newCategory.Categories.Add(categoryResponse);
                }
                newCategory.IsSuccess = true;
                newCategory.Message = "Category Fetch successfully";

                return newCategory;
            }
            catch (Exception ex)
            {
                newCategory.IsSuccess = false;
                newCategory.Message = $"Category fetch failed: {ex.Message}";
                return newCategory;
            }
        }

        public async Task<CategoryBOResponse> Delete(int CategoryId, int UserId)
        {
            CategoryBOResponse newCategory = new CategoryBOResponse();
            try
            {
                if (CategoryId <= 0)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = $"Category with the categoryid does not exists: {CategoryId}";
                    return newCategory;
                }

                var categoryData = await  _dbContext.Categories.Where(c => c.Categoryid == CategoryId && c.Isdeleted == false && c.Userid == UserId).FirstOrDefaultAsync();
                if (categoryData == null)
                {
                    newCategory.IsSuccess = false;
                    newCategory.Message = $"Category with the categoryid does not exists: {CategoryId}";
                    return newCategory;
                }

                // 🔹 Soft delete
                categoryData.Isdeleted = true;
                categoryData.Lastmodifieddate = DateTime.Now;

                _dbContext.Categories.Update(categoryData);
                await _dbContext.SaveChangesAsync();

                newCategory.IsSuccess = true;
                newCategory.Message = "Category deleted successfully";

                return newCategory;
            }
            catch (Exception ex)
            {
                newCategory.IsSuccess = false;
                newCategory.Message = $"Category deleted failed: {ex.Message}";
                return newCategory;
            }
        }

    }
}
