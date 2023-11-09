using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private static List<Task> tasks = new List<Task>();

        [HttpPost("createTask")]
        public IActionResult CreateTask([FromQuery] string title, [FromQuery] string description)
        {   
            // Create a new task with the input data
            var newTask = new Task
            {
                Id = GenerateUiniqueId(),
                Title = title,
                Description = description,
            };
            tasks.Add(newTask);

            return Ok(newTask);
        }

        private int GenerateUiniqueId()
        {
            return tasks.Count + 1;
        }

        [HttpGet("getAllTasks")]
        public IActionResult GetTasks()
        {
            if (tasks.Count == 0)
            {
                return BadRequest("There are no tasks created");
            }

            return Ok(tasks);
        }

        [HttpGet("getTaskById")]
        public IActionResult GetTaskById([FromQuery] int id)
        {
            if (tasks.Count == 0)
            {
                return BadRequest("There are no tasks created");
            }

            var existingTask = tasks.FirstOrDefault(t => t.Id == id); = tasks.FirstOrDefault(t => t.Id == id);

            return Ok(task);
        }

        [HttpPut("updateTaskById")]
        public IActionResult UpdateTask([FromQuery] int id, [FromQuery] string title, [FromQuery] string description)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound("Task not found with the specified ID.");
            }

            existingTask.Title = title;
            existingTask.Description = description;

            return Ok(existingTask);
        }

        [HttpDelete("deleteTaskById")]
        public IActionResult DeleteTask([FromQuery] int id)
        {
            // Find the task with the specified ID
            var existingTask = tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound("Task not found with the specified ID.");
            }

            tasks.Remove(existingTask);

            return Ok("task with Id " + existingTask.Id + "has been removed");
        }
    }
}