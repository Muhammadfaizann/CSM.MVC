using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
{
    private readonly DataBaseContext _context;
    private readonly IMapper _mapper;

    public StudentController(DataBaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var students = _mapper.Map<IEnumerable<Student>>(_context.Students.ToList());
        return View(students);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    public async Task<IActionResult> Details(int id)
    {
        var student = _mapper.Map<Student>(await _context.Students.FindAsync(id));
        if (student == null)
        {
            return NotFound();
        }
        var registeredCourses = _context.Courses
            .FromSqlRaw("EXEC GetCoursesByStudentId @StudentId", new SqlParameter("@StudentId", id))
            .ToList();
        student.Registrations = _mapper.Map<List<Course>>(registeredCourses);
        return View(student);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
