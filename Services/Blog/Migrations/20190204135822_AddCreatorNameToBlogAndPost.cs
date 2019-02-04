using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddCreatorNameToBlogAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("0139bd0e-44de-4a75-9146-8841137787e5"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("9494a38c-0a0b-4fd9-84de-f7f62a76706a"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("ed284137-a8e7-4246-b92c-ffd81d73ccf8"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("0a376ca5-33bf-48ce-a7af-d601fc45160e"));

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Blogs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "Created", "CreatorId", "CreatorName", "Title" },
                values: new object[] { new Guid("d8c97f6b-eba1-40f5-b1c9-c456d41a1446"), "Content-1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ae7b3b9c-5eb9-4642-a215-e2942243d55e"), "Creator 1", "Title-1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "Created", "CreatorId", "CreatorName", "Title" },
                values: new object[] { new Guid("7241bc8b-b4c6-4ffc-8754-d192904b93fe"), "Content-2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e13c029c-090d-4366-9dda-48876ddddce1"), "Creator 2", "Title-2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "Created", "CreatorId", "CreatorName", "Title" },
                values: new object[] { new Guid("aba681c5-5414-465a-b366-143c2c2ebc00"), "Content-3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cdb86c22-4ce5-4070-960e-251ef0c1935e"), "Creator 3", "Title-3" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CreatorId", "CreatorName" },
                values: new object[] { new Guid("95fc888d-0e0c-4bf6-b106-1de2f56572fd"), new Guid("d8c97f6b-eba1-40f5-b1c9-c456d41a1446"), "Content", new Guid("62c95ccf-89ab-4832-acb9-c5e09cda1e68"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("7241bc8b-b4c6-4ffc-8754-d192904b93fe"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("aba681c5-5414-465a-b366-143c2c2ebc00"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("95fc888d-0e0c-4bf6-b106-1de2f56572fd"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("d8c97f6b-eba1-40f5-b1c9-c456d41a1446"));

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Blogs");

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "Created", "CreatorId", "Title" },
                values: new object[] { new Guid("0a376ca5-33bf-48ce-a7af-d601fc45160e"), "Content-1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b2576551-7c88-49e4-ab58-1cffe2cbd17a"), "Title-1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "Created", "CreatorId", "Title" },
                values: new object[] { new Guid("9494a38c-0a0b-4fd9-84de-f7f62a76706a"), "Content-2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6b73ef1-f626-4475-960b-0a2ff1a17b7f"), "Title-2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "Created", "CreatorId", "Title" },
                values: new object[] { new Guid("0139bd0e-44de-4a75-9146-8841137787e5"), "Content-3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4108fd4e-306e-4936-89ec-06a7b356a895"), "Title-3" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CreatorId" },
                values: new object[] { new Guid("ed284137-a8e7-4246-b92c-ffd81d73ccf8"), new Guid("0a376ca5-33bf-48ce-a7af-d601fc45160e"), "Content", new Guid("fa6e457e-15e0-4fb1-a450-c60a7eaead6b") });
        }
    }
}
