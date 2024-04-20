using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.MVC.Migrations
{
    /// <inheritdoc />
    public partial class GetCoursesByStudentIdProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoursesByStudentId
                            @StudentId INT
                        AS
                        BEGIN
                            SELECT c.*
                            FROM Courses c
                            INNER JOIN Registrations r ON c.Id = r.CourseId
                            WHERE r.StudentId = @StudentId;
                        END
                        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Drop PROCEDURE GetCoursesByStudentId
                            
                        ");
        }
    }
}
