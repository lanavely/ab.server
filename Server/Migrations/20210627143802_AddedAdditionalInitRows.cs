using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AddedAdditionalInitRows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateRegistration",
                value: new DateTime(2021, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateLastActivity", "DateRegistration" },
                values: new object[,]
                {
                    { 5, new DateTime(2021, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2021, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateRegistration",
                value: new DateTime(2021, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
