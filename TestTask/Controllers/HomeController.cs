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
        public IActionResult CreateTask([FromQuery] string title, [FromQuery] string description)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            return Ok(_testTaskDbContext.Tasks);
        }

        [HttpGet("getAllTasks")]
        public IActionResult GetTasks()
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }

            return Ok(_testTaskDbContext.Tasks);
        }

        [HttpGet("getTaskById")]
        public IActionResult GetTaskById([FromQuery] int id)
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }

            var existingTask = _testTaskDbContext.Tasks.FirstOrDefault(t => t.Id == id);

            return Ok(existingTask);
        }

        [HttpPut("updateTaskById")]
        public IActionResult UpdateTask([FromQuery] int id, [FromQuery] string title, [FromQuery] string description)
        {
            if (!_testTaskDbContext.Tasks.Any())
            {
                return BadRequest("There are no tasks created");
            }

            var existingTask = _testTaskDbContext.Tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound("Task not found with the specified ID.");
            }

            existingTask.Title = title;
            existingTask.Description = description;

            _testTaskDbContext.SaveChanges();

            return Ok(existingTask);
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

            return Ok("task with Id " + deletedTask.Id + "has been removed");
        }
    }
}