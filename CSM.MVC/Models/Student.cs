using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50)]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gender is required")]
    [StringLength(10)]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Birth is required")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; } 

    [Required(ErrorMessage = "Address 1 is required")]
    [StringLength(100)]
    public string Address1 { get; set; } = string.Empty;

    [StringLength(100)]
    public string Address2 { get; set; } = string.Empty;

    [StringLength(100)]
    public string Address3 { get; set; }  = string.Empty;

    public List<Course> Registrations = new List<Course>();

}

