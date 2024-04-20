using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.MVC.Migrations
{
    /// <inheritdoc />
    public partial class GetStudentsByCourseIdProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetStudentsByCourseId
                            @CourseId INT
                        AS
                        BEGIN
                            SELECT s.*
                            FROM Students s
                            INNER JOIN Registrations r ON s.Id = r.StudentId
                            WHERE r.CourseId = @CourseId;
                        END
                        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Drop PROCEDURE GetStudentsByCourseId");
        }
    }
}
