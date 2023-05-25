using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSchool.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editmaterialentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                table: "Materials",
                newName: "Path");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubjectId",
                table: "Materials",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Subjects_SubjectId",
                table: "Materials",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Subjects_SubjectId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_SubjectId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Materials",
                newName: "path");
        }
    }
}
