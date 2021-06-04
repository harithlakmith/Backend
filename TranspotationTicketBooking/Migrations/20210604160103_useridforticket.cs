using Microsoft.EntityFrameworkCore.Migrations;

namespace TranspotationTicketBooking.Migrations
{
    public partial class useridforticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusInfo_Users_UserId",
                table: "BusInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Passenger_Users_UserId",
                table: "Passenger");

            migrationBuilder.DropIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger");

            migrationBuilder.DropIndex(
                name: "IX_BusInfo_UserId",
                table: "BusInfo");

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
                name: "UserId",
                table: "Passenger");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BusInfo");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d9c69cbd-5f34-4d40-be05-74441008cd88", "76942597-d7ed-4836-baf8-e7ecfd334da7", "Passenger", "PASSENGER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb9e9678-3cb8-4d61-9542-1c1444025392", "0bbc6f3c-4064-4e3d-975c-b6735cf3a1ae", "BusController", "BUSCONTROLLER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6bf7f621-93a8-4c49-aa6d-fa8f52d9e277", "1c77a837-dd28-489b-8f03-75a52c8663df", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6bf7f621-93a8-4c49-aa6d-fa8f52d9e277");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "cb9e9678-3cb8-4d61-9542-1c1444025392");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d9c69cbd-5f34-4d40-be05-74441008cd88");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ticket");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Passenger",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BusInfo",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_UserId",
                table: "Passenger",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusInfo_UserId",
                table: "BusInfo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusInfo_Users_UserId",
                table: "BusInfo",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passenger_Users_UserId",
                table: "Passenger",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
