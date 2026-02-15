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
    public class TaskController : ControllerBase
    {
        public readonly ITaskService _taskService;
        private APIResponse _apiResponse;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
            _apiResponse = new();
        }

        [HttpPost]
        [Route("Create")]

        public async Task<ActionResult<APIResponse>> Create([FromBody] TaskBORequest objBoRequest)
        {
            objBoRequest.Createdbyid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newtask = await _taskService.Create(objBoRequest);

            if (newtask.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newtask.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newtask;

            return Ok(_apiResponse);

        }

        [HttpPut]
        [Route("Update")]

        public async Task<ActionResult<APIResponse>> Update([FromBody] TaskBORequest objBoRequest)
        {
            objBoRequest.Createdbyid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newtask = await _taskService.Update(objBoRequest);

            if (newtask.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newtask.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newtask;

            return Ok(_apiResponse);

        }

        [HttpGet("{TaskId}")]

        public async Task<ActionResult<APIResponse>> GetById(int TaskId)
        {
            int Createdbyid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newtask = await _taskService.GetById(TaskId, Createdbyid);

            if (newtask.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newtask.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newtask;

            return Ok(_apiResponse);

        }

        [HttpGet]
        [Route("GetList")]

        public async Task<ActionResult<APIResponse>> GetList(DateOnly Taskdate)
        {
            int Creaetdbyid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newTask = await _taskService.GetList(Creaetdbyid, Taskdate);

            if (newTask.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newTask.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newTask;

            return Ok(_apiResponse);

        }

        [HttpDelete("{TaskId}")]

        public async Task<ActionResult<APIResponse>> Delete(int TaskId)
        {
            int Createdbyid = Convert.ToInt32(HttpContext.Items["UserId"]);


            var newTask = await _taskService.Delete(TaskId, Createdbyid);

            if (newTask.IsSuccess == false)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.Forbidden;
                _apiResponse.Errors.Add(newTask.Message);

                return Unauthorized(_apiResponse);
            }

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.data = newTask;

            return Ok(_apiResponse);

        }
    }
}
