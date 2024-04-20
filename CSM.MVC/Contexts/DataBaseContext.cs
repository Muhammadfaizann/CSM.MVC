using CSM.MVC.Models.Dto;
using Microsoft.EntityFrameworkCore;

public class DataBaseContext : DbContext
{

    public DbSet<CourseDto> Courses { get; set; }
    public DbSet<StudentDto> Students { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    public DbSet<CoursesWithSpaceModel> GetCoursesWithSpace { get; set; }
    public DbSet<StudentsDidntRegisterMaxCoursesModel> StudentsDidntRegisterMaxCourses { get; set; }


    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CourseDto>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

        modelBuilder.Entity<StudentDto>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
    }
}

