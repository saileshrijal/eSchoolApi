using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSchool.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subjectissue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubject_Grades_GradeId",
                table: "GradeSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubject_Subjects_SubjectId",
                table: "GradeSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeSubject",
                table: "GradeSubject");

            migrationBuilder.RenameTable(
                name: "GradeSubject",
                newName: "GradeSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubject_SubjectId",
                table: "GradeSubjects",
                newName: "IX_GradeSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubject_GradeId",
                table: "GradeSubjects",
                newName: "IX_GradeSubjects_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeSubjects",
                table: "GradeSubjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubjects_Grades_GradeId",
                table: "GradeSubjects",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubjects_Subjects_SubjectId",
                table: "GradeSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubjects_Grades_GradeId",
                table: "GradeSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeSubjects_Subjects_SubjectId",
                table: "GradeSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeSubjects",
                table: "GradeSubjects");

            migrationBuilder.RenameTable(
                name: "GradeSubjects",
                newName: "GradeSubject");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubjects_SubjectId",
                table: "GradeSubject",
                newName: "IX_GradeSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_GradeSubjects_GradeId",
                table: "GradeSubject",
                newName: "IX_GradeSubject_GradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeSubject",
                table: "GradeSubject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubject_Grades_GradeId",
                table: "GradeSubject",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeSubject_Subjects_SubjectId",
                table: "GradeSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
