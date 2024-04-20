using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSM.MVC.Migrations
{
    /// <inheritdoc />
    public partial class GetCoursesWithSpaceView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW GetCoursesWithSpace AS 
                SELECT 1 as Id, COUNT(*) AS CoursesCount
                FROM (
                    SELECT c.Id
                    FROM Courses c
                    LEFT JOIN Registrations r ON c.Id = r.CourseId
                    GROUP BY c.Id
                    HAVING COUNT(r.StudentId) < 5
                ) AS Subquery;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        drop view GetCoursesWithSpace;
        ");
        }
    }
}
