using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace register.infra.data.Migrations
{
    public partial class CustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Birthdate", "DeleteDate", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("1ed5e347-bd10-411f-89fc-fe7a13149087"), new DateTime(2000, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "John", 1, "Stout" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Birthdate", "DeleteDate", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("1ed5e347-bd10-411f-89fc-fe7a13149088"), new DateTime(2005, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mary", 2, "Dunkel" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Birthdate", "DeleteDate", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("1ed5e347-bd10-411f-89fc-fe7a13149089"), new DateTime(2000, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jane", 1, "Pilsen" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
