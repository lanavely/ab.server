using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateRegistration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateLastActivity = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateLastActivity", "DateRegistration" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2021, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2021, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
