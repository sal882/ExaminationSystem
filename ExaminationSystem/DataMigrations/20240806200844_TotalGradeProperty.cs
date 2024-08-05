using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationSystem.DataMigrations
{
    /// <inheritdoc />
    public partial class TotalGradeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalGrade",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGrade",
                table: "Exams");
        }
    }
}
