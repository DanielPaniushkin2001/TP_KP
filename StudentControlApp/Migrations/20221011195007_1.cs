using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentControlApp.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studentdatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studentdatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studentdatas_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Studentdatas_IdentityUser_Userid",
                        column: x => x.Userid,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeacherDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherDatas_IdentityUser_Userid",
                        column: x => x.Userid,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    TeacherDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Disciplines_TeacherDatas_TeacherDataId",
                        column: x => x.TeacherDataId,
                        principalTable: "TeacherDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    teacherID = table.Column<int>(type: "int", nullable: false),
                    TeacherDataId = table.Column<int>(type: "int", nullable: true),
                    studentID = table.Column<int>(type: "int", nullable: false),
                    StudentdataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSubjects_Studentdatas_StudentdataId",
                        column: x => x.StudentdataId,
                        principalTable: "Studentdatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjects_TeacherDatas_TeacherDataId",
                        column: x => x.TeacherDataId,
                        principalTable: "TeacherDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_GroupId",
                table: "Disciplines",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_TeacherDataId",
                table: "Disciplines",
                column: "TeacherDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Studentdatas_GroupId",
                table: "Studentdatas",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Studentdatas_Userid",
                table: "Studentdatas",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_StudentdataId",
                table: "StudentSubjects",
                column: "StudentdataId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_TeacherDataId",
                table: "StudentSubjects",
                column: "TeacherDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDatas_Userid",
                table: "TeacherDatas",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "Studentdatas");

            migrationBuilder.DropTable(
                name: "TeacherDatas");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
