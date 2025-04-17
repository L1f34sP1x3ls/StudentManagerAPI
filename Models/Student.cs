// Declare the namespace for the model class
using System.Text;

namespace StudentManagerApi.Models
{
    public enum Course
    {
        English,
        Math,
        SocialStudies,
        History,
        Science,
        PE
    }
    // This is a plain C# class (POCO) that represents a student
    public class Student
    {
        // Unique identifier for the student
        public static int _NextId = 0; // static variable to keep track of the last assigned ID
        public int Id { get; set; } // serialized id #
        public string FirstName { get; set; } // two sets of random characters between 3-8 lenght each
        public string LastName { get; set; } // two sets of random characters between 3-8 lenght each
        public int Cohort { get; set; } // set random number 1-12
        public int Age { get; set; } // set random number range (18-12 +/-1), 18 +/- 1
        public Dictionary<Course, int> Grades { get; set; } // English, Math, Social Studies, History, Science, PE each assigned a grade between 0-100

        public Student()
        {

        }
    }
    public static class StudentManagerSystem
    {
        public static string GenerateRandomName(int min, int max)
        {
            Random random = new Random();
            int length = new Random().Next(min, max + 1);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
        public static Dictionary<Course, int> GenerateRandomGrades()
        {
            Random random = new Random();
            Dictionary<Course, int> grades = new Dictionary<Course, int>();
            foreach (Course course in Enum.GetValues(typeof(Course)))
            {
                grades[course] = random.Next(60, 101); // random grade between 0 and 100
            }
            return grades;
        }
    }
}
