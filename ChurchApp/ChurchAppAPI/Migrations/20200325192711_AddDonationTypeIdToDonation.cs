using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchAppAPI.Migrations
{
    public partial class AddDonationTypeIdToDonation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeID",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "DonationTypeID",
                table: "Donations",
                newName: "DonationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DonationTypeID",
                table: "Donations",
                newName: "IX_Donations_DonationTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "DonationTypeId",
                table: "Donations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeId",
                table: "Donations",
                column: "DonationTypeId",
                principalTable: "DonationTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeId",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "DonationTypeId",
                table: "Donations",
                newName: "DonationTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_DonationTypeId",
                table: "Donations",
                newName: "IX_Donations_DonationTypeID");

            migrationBuilder.AlterColumn<int>(
                name: "DonationTypeID",
                table: "Donations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeID",
                table: "Donations",
                column: "DonationTypeID",
                principalTable: "DonationTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
