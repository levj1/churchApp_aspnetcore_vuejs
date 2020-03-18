using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchAppAPI.Migrations
{
    public partial class addAddressType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddressType_TypeID",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressType",
                table: "AddressType");

            migrationBuilder.RenameTable(
                name: "AddressType",
                newName: "AddresseTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddresseTypes",
                table: "AddresseTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddresseTypes_TypeID",
                table: "Addresses",
                column: "TypeID",
                principalTable: "AddresseTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddresseTypes_TypeID",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddresseTypes",
                table: "AddresseTypes");

            migrationBuilder.RenameTable(
                name: "AddresseTypes",
                newName: "AddressType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressType",
                table: "AddressType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddressType_TypeID",
                table: "Addresses",
                column: "TypeID",
                principalTable: "AddressType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
