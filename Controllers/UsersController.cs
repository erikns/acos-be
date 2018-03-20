using ACOS_be.Business;
using ACOS_be.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACOS_be.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private UserService userService;
        private TaskService taskService;

        public UsersController(UserService userService,
            TaskService taskService)
        {
            this.userService = userService;
            this.taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = userService.Find(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateNewUser([FromBody] UserModel user)
        {
            var createdUser = userService.Create(user);
            if (createdUser != null)
            {
                return Ok(createdUser);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (userService.Exists(id))
            {
                if (userService.Delete(id))
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}/tasks")]
        public IActionResult GetTasksForUser(int userId)
        {
            var user = userService.Find(userId);
            if (user == null) return NotFound();
            var tasks = taskService.FindByUserEmail(user.Email);
            return Ok(tasks);
        }
    }
}