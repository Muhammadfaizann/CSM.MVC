using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RegistrationController : Controller
{
    private readonly DataBaseContext _context;
    private readonly IMapper _mapper;



    public RegistrationController(DataBaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var viewModel = new RegistrationViewModel
        {
            Students = _mapper.Map<IEnumerable<Student>>(_context.Students.ToList()),
            Courses = _mapper.Map<IEnumerable<Course>>(_context.Courses.ToList()),
        };

        var result = _context
        .StudentsDidntRegisterMaxCourses
        .ToList();

        var result2 = _context
        .GetCoursesWithSpace
        .ToList();

        viewModel.StudentsDidntRegisterMaxCoursesCount = result[0].StudentsCount;
        viewModel.NumberOfCourseStillHaveSpace = result2[0].CoursesCount;
        return View(viewModel); 
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel viewModel) 
    {
        if(viewModel.SelectedStudentId == 0 || viewModel.SelectedCourseId == 0)
        {
            ViewBag.PopupMessage = "Select both course and student to register";
            return View();
        }
        // Check if the student is already registered for the course
        var existingRegistration = await _context.Registrations
            .Where(r => r.StudentId == viewModel.SelectedStudentId && r.CourseId == viewModel.SelectedCourseId)
            .FirstOrDefaultAsync();

        if (existingRegistration != null)
        {
            ViewBag.PopupMessage = "Student is already registered for this course";
            return View();
        }

        // Check if the student has already registered for 5 courses
        var studentRegistrationsCount = await _context.Registrations
            .CountAsync(r => r.StudentId == viewModel.SelectedStudentId);

        if (studentRegistrationsCount >= 5)
        {
            ViewBag.PopupMessage = "Student has already registered for the maximum allowed courses";
            return View();
        }

        // Check if the course has available space
        var courseRegistrationsCount = await _context.Registrations
            .CountAsync(r => r.CourseId == viewModel.SelectedCourseId);

        if (courseRegistrationsCount >= 5)
        {
            ViewBag.PopupMessage = "Course is already full";
            return View();
        }

        // Create new registration
        var registration = new Registration
        {
            StudentId = viewModel.SelectedStudentId,
            CourseId = viewModel.SelectedCourseId,
        };

        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();

        ViewBag.PopupMessage = "Student successfully registered for the course";
        return View();

    }
}
