using Microsoft.EntityFrameworkCore.Migrations;

namespace TranspotationTicketBooking.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1bc2c583-d566-4d5f-9fe7-f9f66107b688");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8018dcfd-623e-4910-a616-d8465a424153");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a4b09852-1ebe-42d1-af08-ba1cb9b008c6");

            migrationBuilder.AddColumn<string>(
                name: "BusNo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d515c75-ea84-402c-9f99-d69a10fa6e18", "6c4fb945-2b25-49d4-9d58-87482fc544bc", "Passenger", "PASSENGER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35b7a21a-e9bb-4c4b-9e15-1a4efaa7a5f6", "bc320a50-c09b-400c-904d-71e95c06576e", "BusController", "BUSCONTROLLER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99266620-5f56-4654-bf3b-a17aff424bc3", "fb6ba838-5546-4617-94db-9d6581ef1753", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1d515c75-ea84-402c-9f99-d69a10fa6e18");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "35b7a21a-e9bb-4c4b-9e15-1a4efaa7a5f6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "99266620-5f56-4654-bf3b-a17aff424bc3");

            migrationBuilder.DropColumn(
                name: "BusNo",
                table: "Users");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a4b09852-1ebe-42d1-af08-ba1cb9b008c6", "bd3e9608-7e10-4822-a72d-ce56947f56bf", "Passenger", "PASSENGER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1bc2c583-d566-4d5f-9fe7-f9f66107b688", "853dd597-92aa-4e08-a104-450c4c8330db", "BusController", "BUSCONTROLLER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8018dcfd-623e-4910-a616-d8465a424153", "249b1e22-f1b6-4374-951d-c4a9488d3c5a", "Administrator", "ADMINISTRATOR" });
        }
    }
}
