using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class AddCreatedToBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("3bee4b2b-6426-4838-9ed0-b7a4d67feedc"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("c4752618-8374-42c8-9c40-6bff8ffbb027"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("cff9d13f-ce97-40cd-a51d-1df08706b8cd"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("785e43e4-3400-49ff-a249-7a2c96d75890"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Blogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Blogs");

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[,]
                {
                    { new Guid("3bee4b2b-6426-4838-9ed0-b7a4d67feedc"), "Content-1", new Guid("a2d0f792-530e-47a6-a22d-5f5ea7f6ebe0"), "Title-1" },
                    { new Guid("c4752618-8374-42c8-9c40-6bff8ffbb027"), "Content-2", new Guid("6a6ac338-7a25-4049-a7c7-9de2b922ae80"), "Title-2" },
                    { new Guid("cff9d13f-ce97-40cd-a51d-1df08706b8cd"), "Content-3", new Guid("f796f035-d9da-4c87-8c9b-6a75074b46d1"), "Title-3" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BlogId", "Content", "CreatorId" },
                values: new object[] { new Guid("785e43e4-3400-49ff-a249-7a2c96d75890"), new Guid("e9f1ad0b-e281-44db-b4fc-c3c2a23919ff"), "Content", new Guid("ee825f96-9522-4c6e-ae5c-383ad3f68668") });
        }
    }
}
