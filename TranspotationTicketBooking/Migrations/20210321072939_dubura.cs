using Microsoft.EntityFrameworkCore.Migrations;

namespace TranspotationTicketBooking.Migrations
{
    public partial class dubura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Tp",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c4343fa-e1ee-445b-90f2-7b55f4be2c3e", "66082839-5ad7-4ad2-8c86-b7b7c8e5d2fc", "Passenger", "PASSENGER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8ce8944-8633-4f11-a779-014196c66bf8", "5d00e388-be00-4ff5-aeda-c9504ba10c7f", "BusController", "BUSCONTROLLER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c84fbead-c1b5-485c-9042-33ebedbc79b1", "2824074b-3de6-40f3-b98e-05fe853c0513", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5c4343fa-e1ee-445b-90f2-7b55f4be2c3e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a8ce8944-8633-4f11-a779-014196c66bf8");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c84fbead-c1b5-485c-9042-33ebedbc79b1");

            migrationBuilder.DropColumn(
                name: "Tp",
                table: "Users");

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
    }
}
