
public class RegistrationViewModel
{
    public IEnumerable<Student>? Students { get; set; } 
    public IEnumerable<Course>? Courses { get; set; }
    public int SelectedStudentId { get; set; } = 0;
    public int SelectedCourseId { get; set; } = 0;

    public int StudentsDidntRegisterMaxCoursesCount { get; set; }
    public int NumberOfCourseStillHaveSpace { get; set; }



    public RegistrationViewModel(IEnumerable<Student> students, IEnumerable<Course> courses)
    {
        Students = students;
        Courses = courses;
    }
    public RegistrationViewModel()
    {
    }
}

