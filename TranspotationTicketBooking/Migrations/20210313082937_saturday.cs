using Microsoft.EntityFrameworkCore.Migrations;

namespace TranspotationTicketBooking.Migrations
{
    public partial class saturday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "95c0e24e-8963-4286-abab-fcec520a09a4");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a1829897-31bd-4175-955a-adebc5f7a637");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "baee82d8-bf3c-498c-b73c-033b349d6f4b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c7b6ccf7-60ea-4c2b-95b0-6c6f9aa6a81c");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Passenger",
                newName: "LastName");

            migrationBuilder.AlterColumn<int>(
                name: "Tp",
                table: "Passenger",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Passenger",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Passenger");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Passenger",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Tp",
                table: "Passenger",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "baee82d8-bf3c-498c-b73c-033b349d6f4b", "6aa93504-8c0e-46fb-89a4-f05ad93c2784", "Visitor", "VISITOR" },
                    { "a1829897-31bd-4175-955a-adebc5f7a637", "ebb999ce-29c1-47da-a1b8-9524c4809832", "Passenger", "PASSENGER" },
                    { "95c0e24e-8963-4286-abab-fcec520a09a4", "d1c034ab-83d7-4bb4-95a4-e6d4e6ecf4b6", "BusController", "BUSCONTROLLER" },
                    { "c7b6ccf7-60ea-4c2b-95b0-6c6f9aa6a81c", "770c6333-8b7d-4941-9693-918b2fd0ebee", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
