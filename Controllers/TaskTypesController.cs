using ACOS_be.Business;
using Microsoft.AspNetCore.Mvc;

namespace ACOS_be.Controllers
{
    [Route("api/types")]
    public class TaskTypesController : Controller
    {
        private TaskTypeService taskTypeService;

        public TaskTypesController(TaskTypeService taskTypeService)
        {
            this.taskTypeService = taskTypeService;
        }

        [HttpGet]
        public IActionResult GetAllTypes()
        {
            return Ok(taskTypeService.FindAll());
        }
    }
}