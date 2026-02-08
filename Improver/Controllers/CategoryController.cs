using CollegeApp.Models;
using Improver.BusinessModel;
using Improver.BusinessServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Improver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;
        private APIResponse _apiResponse;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _apiResponse = new();
        }

        [HttpPost]
        [Route("Create")]

        public async Task<ActionResult<APIResponse>> Create([FromBody] CategoryBORequest objBoRequest)
        {
            objBoRequest.Userid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newCategory = await _categoryService.Create(objBoRequest);

            if (newCategory.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newCategory.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newCategory;

            return Ok(_apiResponse);

        }

        [HttpPut]
        [Route("Update")]

        public async Task<ActionResult<APIResponse>> Update([FromBody] CategoryBORequest objBoRequest)
        {
            objBoRequest.Userid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newCategory = await _categoryService.Update(objBoRequest);

            if (newCategory.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newCategory.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newCategory;

            return Ok(_apiResponse);

        }

        [HttpGet("{CategoryId}")]

        public async Task<ActionResult<APIResponse>> GetById(int CategoryId)
        {
            int Userid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newCategory = await _categoryService.GetById(CategoryId,Userid);

            if (newCategory.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newCategory.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newCategory;

            return Ok(_apiResponse);

        }

        [HttpGet]
        [Route("GetList")]

        public async Task<ActionResult<APIResponse>> GetList()
        {
            int Userid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newCategory = await _categoryService.GetList(Userid);

            if (newCategory.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newCategory.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newCategory;

            return Ok(_apiResponse);

        }

        [HttpDelete("{CategoryId}")]

        public async Task<ActionResult<APIResponse>> Delete(int CategoryId)
        {
            int Userid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newCategory = await _categoryService.Delete(CategoryId, Userid);

            if (newCategory.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newCategory.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newCategory;

            return Ok(_apiResponse);

        }

    }
}
