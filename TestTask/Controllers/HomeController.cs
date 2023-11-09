using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TestTaskDbContext _testTaskDbContext;
        public HomeController(TestTaskDbContext testTaskDbContext)
        {
            _testTaskDbContext = testTaskDbContext;
        }

        [HttpPost("createTask")]
        public IActionResult CreateTask([FromQuery] string? title, [FromQuery] string? description)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(title == null)
            {
                return BadRequest("Title is required");
            }
            if (description == null)
            {
                return BadRequest("Description is required");
            }

            if (_testTaskDbContext.Tasks.Any(t => t.Title == title))
            {
                return BadRequest("A task with the same title already exists");
            }

            // Create a new task with the input data
            var newTask = new CustomTask
            {
                Title = title,
                Description = description
            };
            
            // Add the new task to the database context
            _testTaskDbContext.Tasks.Add(newTask);

            // Save changes to the database
            _testTaskDbContext.SaveChanges();

            var response = new
            {
                Message = "Added task",
                Data = newTask
            };
            return Ok(response);
        }

        [HttpGet("getAllTasks")]
        public IActionResult GetTasks()
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }

            var response = new
            {
                Message = "List of all Tasks",
                Data = _testTaskDbContext.Tasks
            };
            return Ok(response);
        }

        [HttpGet("getTaskById")]
        public IActionResult GetTaskById([FromQuery] int id)
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }

            var foundedTask = _testTaskDbContext.Tasks.FirstOrDefault(t => t.Id == id);

            var response = new
            {
                Message = "Founded Task",
                Data = foundedTask
            };
            return Ok(response);
        }

        [HttpPut("updateTaskById")]
        public IActionResult UpdateTask([FromQuery] int id, [FromQuery] string? title, [FromQuery] string? description)
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }

            var previousTask = _testTaskDbContext.Tasks.FirstOrDefault(t => t.Id == id);

            if (previousTask== null)
            {
                return NotFound("Task not found with the specified ID.");
            }

            if (title == null)
            {
                return BadRequest("Title is required");
            }
            if (description == null)
            {
                return BadRequest("Description is required");
            }


            if (_testTaskDbContext.Tasks.Any(t => t.Title == title))
            {
                return BadRequest("A task with the same title already exists");
            }

            var updatedTask = previousTask;
            updatedTask.Title = title;
            updatedTask.Description = description;

            _testTaskDbContext.SaveChanges();

            var response = new
            {   
                PreviousMessage = "Task before Update",
                PreviousData = previousTask,
                Message = "Updated Tas Datk",
                Data = updatedTask
            };
            return Ok(response);
        }

        [HttpDelete("deleteTaskById")]
        public IActionResult DeleteTask([FromQuery] int id)
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }
            // Find the task with the specified ID
            var deletedTask = _testTaskDbContext.Tasks.FirstOrDefault(t => t.Id == id);

            if (deletedTask == null)
            {
                return NotFound("Task not found with the specified ID.");
            }

            _testTaskDbContext.Tasks.Remove(deletedTask);
            _testTaskDbContext.SaveChanges();

            var response = new
            {
                Message = "This task has been deleted",
                Data = deletedTask
            };
            return Ok(response);
        }
    }
}