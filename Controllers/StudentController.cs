// Import necessary namespaces
using Microsoft.AspNetCore.Mvc;             // For MVC controller features like [ApiController] and routing
using StudentManagerApi.Models;                   // Our Student model
using StudentManagerApi.Services;                 // The StudentService we created for logic/data management

namespace StudentManagerApi.Controllers
{
    // This attribute marks the class as a Web API controller
    [ApiController]

    // This sets the route to "api/students". [controller] will be replaced with the controller name minus "Controller"
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // Private field to hold the injected StudentService
        private readonly StudentService _service;

        // Constructor that accepts a StudentService instance via dependency injection
        public StudentsController(StudentService service)
        {
            _service = service;
        }

        // HTTP GET: api/students
        // Returns a list of all students
        [HttpGet]
        public ActionResult<List<Student>> GetAll()
        {
            return _service.GetAll();  // Delegate the logic to the service
        }

        // HTTP POST: api/students/generate
        // Generates a specified number of fake students
        [HttpPost]
        [Route("Generate")]
        public ActionResult<List<Student>> Generate(int count = 10)
        {
            var students = _service.GenerateFakeData(count); // Generate fake data
            return CreatedAtAction(nameof(GetAll), students); // Return 201 Created with the list of generated students
        }

        // HTTP GET: api/students/{id}
        // Returns a single student by ID
        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            var student = _service.GetById(id); // Try to get the student by ID
            if (student == null)
                return NotFound();  // Return 404 if the student doesn't exist

            return student;  // Return the student if found
        }

        // HTTP POST: api/students
        // Creates a new student
        [HttpPost]
        public ActionResult<Student> Add(Student student)
        {
            var created = _service.Add(student);  // Add student and return the created one
            // Return 201 Created response with the URI of the newly created resource
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // HTTP PUT: api/students/{id}
        // Updates an existing student (full replacement)
        [HttpPut("{id}")]
        public IActionResult Update(int id, Student updated)
        {
            // If update fails (e.g., student not found), return 404
            if (!_service.Update(id, updated))
                return NotFound();

            return NoContent(); // Return 204 No Content on successful update
        }

        // HTTP DELETE: api/students/{id}
        // Deletes a student by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // If deletion fails (student not found), return 404
            if (!_service.Delete(id))
                return NotFound();

            return NoContent(); // Return 204 No Content on successful deletion
        }

    }
}
