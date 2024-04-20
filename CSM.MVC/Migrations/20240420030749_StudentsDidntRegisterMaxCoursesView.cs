using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.MVC.Migrations
{
    /// <inheritdoc />
    public partial class StudentsDidntRegisterMaxCoursesView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW StudentsDidntRegisterMaxCourses AS
                SELECT 1 as Id, COUNT(*) AS StudentsCount
                    FROM Students s
                    WHERE NOT EXISTS (
                        SELECT 1
                        FROM Registrations r
                        WHERE r.StudentId = s.Id
                        GROUP BY r.StudentId
                        HAVING COUNT(r.CourseId) >= 5
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        drop view StudentsDidntRegisterMaxCourses;
        ");
        }
    }
}
