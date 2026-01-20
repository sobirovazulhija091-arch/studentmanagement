using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class Fixmistakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeValue",
                table: "Grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeValue",
                table: "Grades");
        }
    }
}
