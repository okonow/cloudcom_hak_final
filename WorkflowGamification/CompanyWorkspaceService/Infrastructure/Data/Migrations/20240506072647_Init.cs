using System;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:complexity", "easy,normal,difficult");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<string>(type: "text", nullable: false),
                    DepartmentDescription = table.Column<string>(type: "text", nullable: true),
                    DirectorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentEmployeesId = table.Column<Guid[]>(type: "uuid[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    JobMetadata_Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    JobMetadata_Complexity = table.Column<Complexity>(type: "complexity", nullable: false),
                    JobMetadata_CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobMetadata_WorkerId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompletedEasyJobsCount = table.Column<int>(type: "integer", nullable: false),
                    CompletedNormalJobsCount = table.Column<int>(type: "integer", nullable: false),
                    CompletedDifficultJobsCount = table.Column<int>(type: "integer", nullable: false),
                    AverageTimeForCompletingJob = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobAnswer",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AttachedFile_Name = table.Column<string>(type: "text", nullable: true),
                    AttachedFile_Extension = table.Column<string>(type: "text", nullable: true),
                    AttachedFile_Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnswer", x => new { x.JobId, x.Id });
                    table.ForeignKey(
                        name: "FK_JobAnswer_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "JobAnswer");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
