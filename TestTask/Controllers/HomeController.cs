using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static TestTask.CustomExceptions;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TestTaskDbContext _testTaskDbContext;
        private ApiResponse ApiResponse { get; set; }

        public HomeController(TestTaskDbContext testTaskDbContext)
        {
            _testTaskDbContext = testTaskDbContext;
            ApiResponse = new ApiResponse();
        }

        [HttpPost("createTask")]
        public IActionResult CreateTask([FromQuery] string? title, [FromQuery] string? description)
        {
            try
            {
                // Validate if title and description are not empty
                ValidateRequiredInput(title, "Title is required");
                ValidateRequiredInput(description, "Description is required");
                // Validate if there is already task with same title
                ValidateTitleDublication(_testTaskDbContext.Tasks, title);

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

                // here we create Api response
                this.ApiResponse.Message = "Added task";
                this.ApiResponse.Data = newTask;
 
                return Ok(this.ApiResponse);
            }
            //custom exceptions
            catch (CustomBadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            //other exceptions
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("getAllTTasks")]
        public IActionResult GetTasks()
        {
            try
            {
                // Find if there are any tasks created
                FindTasks(_testTaskDbContext.Tasks);

                this.ApiResponse.Message = "List of all Tasks";
                this.ApiResponse.Data = _testTaskDbContext.Tasks;

                return Ok(this.ApiResponse);
            }
            //custom exceptions
            catch (CustomBadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            //other exceptions
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getTaskById")]
        public IActionResult GetTaskById([FromQuery] int id)
        {
            try
            {
                FindTasks(_testTaskDbContext.Tasks);
                // Find if there is task by id input value
                var taskWithSpecifiedId = FindTaskById(_testTaskDbContext.Tasks, id);

                this.ApiResponse.Message = "Founded Task";
                this.ApiResponse.Data = taskWithSpecifiedId;

                return Ok(this.ApiResponse);
            }
            //custom exceptions
            catch (CustomBadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            //other exceptions
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateTaskById")]
        public IActionResult UpdateTask([FromQuery] int id, [FromQuery] string? title, [FromQuery] string? description)
        {
            try
            {
                FindTasks(_testTaskDbContext.Tasks);
                var taskToBeUpdated = _testTaskDbContext.Tasks.FirstOrDefault(t => t.Id == id);
                ValidateRequiredInput(title, "Title is required");
                ValidateRequiredInput(description, "Description is required");
                ValidateTitleDublication(_testTaskDbContext.Tasks, title);

                taskToBeUpdated.Title = title;
                taskToBeUpdated.Description = description;

                _testTaskDbContext.SaveChanges();
                this.ApiResponse.Message = "Updated Task";
                this.ApiResponse.Data = taskToBeUpdated;

                return Ok(this.ApiResponse);
            }
            //custom exceptions
            catch (CustomBadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            //other exceptions
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteTaskById")]
        public IActionResult DeleteTask([FromQuery] int id)
        {
            try
            {
                FindTasks(_testTaskDbContext.Tasks);
                var deletedTask = FindTaskById(_testTaskDbContext.Tasks, id);

                _testTaskDbContext.Tasks.Remove(deletedTask);
                _testTaskDbContext.SaveChanges();

                this.ApiResponse.Message = "This task has been deleted";
                this.ApiResponse.Data = deletedTask;

                return Ok(this.ApiResponse);
            }
            //custom exceptions
            catch (CustomBadRequestException ex) 
            {
                return BadRequest(ex.Message);
            }
            //other exceptions
            catch (Exception ex) 
            {
                  return BadRequest(ex.Message);
            }
        }

        // 
        public static void ValidateRequiredInput(string? value, string errorMessage)
        {
            if (value == null)
            {
                throw new CustomBadRequestException(errorMessage);
            }
        }

        public static void ValidateTitleDublication(DbSet<CustomTask> taskList, string title)
        {
            if (taskList.Any(t => t.Title == title))
            {
                throw new CustomBadRequestException("A task with the same title already exists");
            }
        }
        public static CustomTask FindTaskById(DbSet<CustomTask> taskList, int id)
        {
            var task = taskList.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new CustomBadRequestException("There is no task with the specified Id");
            }
            return task;
        }

        public static void FindTasks(DbSet<CustomTask> taskList)
        {
            if (!taskList.Any())
            {
                throw new CustomBadRequestException("There are no tasks created");
            }
        }
    }
}