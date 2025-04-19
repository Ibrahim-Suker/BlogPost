using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrasitructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "tech" },
                    { 2, null, "test" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedAt", "Image", "Title" },
                values: new object[,]
                {
                    { 1, 1, "post content", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "x.png", "title1" },
                    { 2, 2, "post content", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "x.png", "title1" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "PostId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "comment1", 1, "reem" },
                    { 2, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "comment1", 1, "reem" },
                    { 3, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "comment1", 1, "reem" },
                    { 4, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "comment1", 2, "reem" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
