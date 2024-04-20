using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class CourseController : Controller
{
    private readonly DataBaseContext _context;
    private readonly IMapper _mapper;

    public CourseController(DataBaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var courses = _mapper.Map<IEnumerable<Course>>(await _context.Courses.ToListAsync());
        return View(courses);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    public async Task<IActionResult> Details(int id)
    {
        var course = _mapper.Map<Course>(await _context.Courses.FindAsync(id));
        
        if (course == null)
        {
            return NotFound();
        }
        var students = _context.Students
       .FromSqlRaw("EXEC GetStudentsByCourseId @CourseId", new SqlParameter("@CourseId", id))
       .ToList();
        course.Registrations = _mapper.Map<List<Student>>(students);
        return View(course);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
