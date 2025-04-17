// Import the model namespace so we can use the Student class
using StudentManagerApi.Models;
using System.Xml.Linq;

namespace StudentManagerApi.Services
{
    // This service acts as a basic data handler (like a temporary database)
    public class StudentService
    {
        // In-memory list to simulate a database
        private readonly List<Student> _students = new();
        private List<Student> updated = new();

        // Counter to simulate auto-incrementing IDs
        private int _nextId = 1;

        // Return all students in the list
        public List<Student> GetAll()
        {
            return _students;
        }

        // Generate fake data for a specified number of students
        public List<Student> GenerateFakeData(int count)
        {
            updated.Clear(); // Clear the list before generating new data
            for (int i = 0; i < count; i++)
            {
                int _cohort = new Random().Next(1, 13);
                var student = new Student
                {
                    Id = Student._NextId++,// random id #
                    FirstName = StudentManagerSystem.GenerateRandomName(5, 5),
                    LastName = StudentManagerSystem.GenerateRandomName(5, 5), // random name between 3 and 8 characters
                    Cohort = _cohort, // random cohort between 1 and 12
                    Age = _cohort + 6 + new Random().Next(-1, 2), // random age between +1/-1 of cohort + 6
                    Grades = StudentManagerSystem.GenerateRandomGrades()
                };
                updated.Add(student);
                _students.Add(student); // Add the generated student to the list
            }
            return updated;
        }
        // Find a student by their ID
        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        // Add a new student to the list
        public Student Add(Student student)
        {
            student.Id = _nextId++;   // Assign a unique ID
            _students.Add(student);   // Add to the list
            return student;           // Return the newly added student
        }

        // Update a student’s data by ID
        public bool Update(int id, Student updated)
        {
            var index = _students.FindIndex(s => s.Id == id);
            if (index == -1)
                return false; // Student not found

            updated.Id = id; // Preserve the original ID
            _students[index] = updated; // Replace with updated data
            return true;
        }

        // Delete a student by ID
        public bool Delete(int id)
        {
            var student = GetById(id);
            if (student == null)
                return false;

            _students.Remove(student);
            return true;
        }


    }
}
