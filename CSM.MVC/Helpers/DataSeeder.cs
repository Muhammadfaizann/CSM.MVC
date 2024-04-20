using System;
using System.Collections.Generic;
using System.Linq;
using CSM.MVC.Models.Dto;

namespace CSM.MVC.Helpers
{
    public class DataSeeder
    {
        private readonly DataBaseContext _context;

        public DataSeeder(DataBaseContext context)
        {
            _context = context;
        }

        public void SeedData(int numStudents, int numCourses)
        {
            // Generate random students
            var random = new Random();
            var genders = new List<string> { "Male", "Female" };
            var addresses = new List<string> { "123 Main St", "456 Elm St", "789 Oak St" };
            var students = Enumerable.Range(1, numStudents).Select(i => new StudentDto
            {
                FirstName = $"Student{i}",
                Surname = $"Surname{i}",
                Gender = genders[random.Next(genders.Count)],
                DOB = DateTime.Today.AddDays(-random.Next(365 * 18, 365 * 30)), // Random DOB between 18 and 30 years ago
                Address1 = addresses[random.Next(addresses.Count)],
                Address2 = addresses[random.Next(addresses.Count)],
                Address3 = addresses[random.Next(addresses.Count)]
            }).ToList();

            // Generate random courses
            var courseCodes = new List<string> { "C001", "C002", "C003", "C004", "C005" };
            var courseNames = new List<string> { "Mathematics", "Science", "History", "English", "Art" };
            var teachers = new List<string> { "Teacher1", "Teacher2", "Teacher3", "Teacher4", "Teacher5" };
            var courses = Enumerable.Range(1, numCourses).Select(i => new CourseDto
            {
                CourseCode = courseCodes[random.Next(courseCodes.Count)],
                CourseName = courseNames[random.Next(courseNames.Count)],
                TeacherName = teachers[random.Next(teachers.Count)],
                StartDate = DateTime.Today.AddDays(random.Next(1, 30)), // Random start date within the next 30 days
                EndDate = DateTime.Today.AddDays(random.Next(31, 90)) // Random end date 31-90 days after start date
            }).ToList();

            // Add students and courses to database
            _context.Students.AddRange(students);
            _context.Courses.AddRange(courses);
            _context.SaveChanges();
        }
    }
}