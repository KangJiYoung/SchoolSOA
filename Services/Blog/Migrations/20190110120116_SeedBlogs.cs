using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Migrations
{
    public partial class SeedBlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[] { new Guid("e9f1ad0b-e281-44db-b4fc-c3c2a23919ff"), "Content-1", new Guid("7762e7eb-d979-499c-882d-bbe31e2bfc15"), "Title-1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[] { new Guid("d8d603ae-b486-4d6a-92ab-5f7bb87c6495"), "Content-2", new Guid("6b072a48-9223-4e78-8269-5563015129ea"), "Title-2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatorId", "Title" },
                values: new object[] { new Guid("3f4cf919-884c-418b-bea3-bd400ace37fe"), "Content-3", new Guid("91b66212-7a01-4c13-bfe6-ba196d414fe0"), "Title-3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("3f4cf919-884c-418b-bea3-bd400ace37fe"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("d8d603ae-b486-4d6a-92ab-5f7bb87c6495"));

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: new Guid("e9f1ad0b-e281-44db-b4fc-c3c2a23919ff"));
        }
    }
}
