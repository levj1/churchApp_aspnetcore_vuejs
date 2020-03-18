using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchAppAPI.Migrations
{
    public partial class addAddressTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddresseTypes_TypeID",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_TypeID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressTypeId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressTypeId",
                table: "Addresses",
                column: "AddressTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddresseTypes_AddressTypeId",
                table: "Addresses",
                column: "AddressTypeId",
                principalTable: "AddresseTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AddresseTypes_AddressTypeId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AddressTypeId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressTypeId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TypeID",
                table: "Addresses",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AddresseTypes_TypeID",
                table: "Addresses",
                column: "TypeID",
                principalTable: "AddresseTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
