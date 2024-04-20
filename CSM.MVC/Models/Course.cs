using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Course Code is required")]
    [StringLength(10)]
    [Display(Name = "Course Code")]
    public string CourseCode { get; set; } = "";

    [Required(ErrorMessage = "Course Name is required")]
    [StringLength(100)]
    [Display(Name = "Course Name")]
    public string CourseName { get; set; } = "";

    [Required(ErrorMessage = "Teacher Name is required")]
    [StringLength(100)]
    [Display(Name = "Teacher Name")]
    public string TeacherName { get; set; } = "";

    [Required(ErrorMessage = "Start Date is required")]
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } 

    [Required(ErrorMessage = "End Date is required")]
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public List<Student> Registrations = new List<Student>();

}
