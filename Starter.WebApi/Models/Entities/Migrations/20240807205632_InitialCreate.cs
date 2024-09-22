using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.WebApi.Models.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserCredentials_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PersonalPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProfessionalPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressSupplement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserCredentialsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserProfile_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "UserProfile_UserCredentialsId_fkey",
                        column: x => x.UserCredentialsId,
                        principalTable: "UserCredentials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserCredentialsId",
                table: "UserProfile",
                column: "UserCredentialsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserCredentials");
        }
    }
}
